using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateManager : MonoBehaviour
{
    public BossBaseState currentState;
    public BossSummonState bossSummon = new BossSummonState();
    public BossCurveFireState bossCurveFire = new BossCurveFireState();
    public BossRoundFireState bossRoundFire = new BossRoundFireState();
    public BossTpFireState bossTpFire = new BossTpFireState();
    public BossWeaknessState bossWeak = new BossWeaknessState();
    public BossDeathState bossDeath = new BossDeathState();
    public BossIdleState bossIdle = new BossIdleState();
    Action rotationStop;
    Transform player;

    [SerializeField] public Animator _animator;


    [SerializeField] float _rotationSpeed;

    [SerializeField] public GameObject _fireballPrefab;
    [SerializeField] float _curveFireballRotationSpeed;
    [SerializeField] float _curveFireballFlyAwaySpeed;
    [SerializeField] int _curveAttackWavesAmount;
    [SerializeField] float _curveAttackWavesSpawnDelay;
    [SerializeField] float _straightFireballSpeed;
    [SerializeField] int _roundAttackWavesAmount;
    [SerializeField] float _roundAttackWavesSpawnDelay;
    [SerializeField] int _amountOfTps;
    [SerializeField] float _afterTpsDelay;

    [SerializeField] Transform _centerPoint;

    [SerializeField] List<Transform> _tpPoints;
    [SerializeField] List<BossSpawner> _spawnPoints;
    [SerializeField] List<GameObject> _possibleMinions;
    public void SwitchState(BossBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    private void Start()
    {
        player = GameObject.FindObjectOfType<TestPlayerExistance>().transform;
        SwitchState(bossCurveFire);
    }
    private void Update()
    {
        RotateToTarget();
    }

    // curve attack
    public void SummonCurveFireball(Vector3 pos, float rotSpeed, float flyAwaySpeed)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        fireball.GetComponent<FireballStateManager>().OnSpawn(new FireballCurveAttackState(), ref rotationStop, transform, rotSpeed, flyAwaySpeed);
    }
    void SummonCurveWave(float rotSpeed,float flyAwaySpeed)
    {
        SummonCurveFireball(transform.position + new Vector3(1, 1.5f, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(-1, 1.5f, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 1.5f, 1), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 1.5f, -1), rotSpeed, flyAwaySpeed);
    }
    IEnumerator CurveWavesSpawnCycle(int waves, float breakTime, float rotSpeed, float flyAwaySpeed)
    {
        int wavesPassed = 0;
        while(wavesPassed < waves)
        {
            SummonCurveWave(rotSpeed,flyAwaySpeed);
            wavesPassed++;
            yield return new WaitForSeconds(breakTime);
        }
        yield return new WaitForSeconds(breakTime);
        rotationStop?.Invoke();
        yield return new WaitForSeconds(0.8f);
        _animator.SetTrigger("centerAttackEndDown");
        yield break;
    }
    void StartCurveAttack()
    {
        StartCoroutine(CurveWavesSpawnCycle(_curveAttackWavesAmount,_curveAttackWavesSpawnDelay,_curveFireballRotationSpeed,_curveFireballFlyAwaySpeed));
    }
    public void IncreaseRotateSpeed()
    {
        _curveFireballRotationSpeed += Time.deltaTime * 2;
    }

    //
    // staright attacks
    void SummonStraightFireball(Vector3 pos, FireballBaseState state)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        Vector3 direction = fireball.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        fireball.GetComponent<FireballStateManager>().OnSpawn(state, transform, direction,_straightFireballSpeed);
    }

    //round attack
    void SummonRoundWave()
    {
        SummonStraightFireball(transform.position + transform.forward * 0.1f + new Vector3(0,1.5f,0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position - transform.forward * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + transform.right * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position - transform.right * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + (transform.forward + transform.right).normalized*0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position - (transform.forward + transform.right).normalized * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + (-transform.forward + transform.right).normalized * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + (transform.forward - transform.right).normalized * 0.1f + new Vector3(0, 1.5f, 0), new FireballRoundAttackState());
    }
    IEnumerator RoundWavesSpawnCycle(int waves, float breakTime)
    {
        int wavesPassed = 0;
        while (wavesPassed < waves)
        {
            SummonRoundWave();
            wavesPassed++;
            yield return new WaitForSeconds(breakTime);
        }
        yield break;
    }
    public void StartRoundAttack()
    {
        StartCoroutine(RoundWavesSpawnCycle(_roundAttackWavesAmount,_roundAttackWavesSpawnDelay));
    }
    //
    List<Transform> GetRandomTpPointsFromList(int amount)
    {
        List<Transform> tpPoints = new List<Transform> ();
        foreach(Transform t in _tpPoints)
        {
            tpPoints.Add(t);
        }
        List<Transform> randPoints = new List<Transform>();
        for(int i = 0; i < amount; i++)
        {
            int randNumber = UnityEngine.Random.Range(0, tpPoints.Count);
            randPoints.Add(tpPoints[randNumber]);
            tpPoints.Remove(tpPoints[randNumber]);
            if (tpPoints.Count == 0) break;
        }
        return randPoints;
    }
    IEnumerator TpFireCycle(int amountOfTps)
    {
        int TpsDone = 0;
        while (TpsDone < amountOfTps)
        {
            TpToPointAndFire(GetRandomTpPointsFromList(amountOfTps)[TpsDone]);
            TpsDone++;
            yield return new WaitForSeconds(_afterTpsDelay);
        }
        yield return new WaitForSeconds(_afterTpsDelay);
        transform.position = _centerPoint.position;
        transform.LookAt(player);
        Quaternion newRot = transform.rotation;
        newRot.z = 0;
        newRot.x = 0;
        transform.rotation = newRot;
        yield break;
    }
    void TpToPointAndFire(Transform point)
    {
        transform.position = point.position;
        transform.LookAt(player);
        _animator.SetTrigger("frontAttack");
        Quaternion newRot = transform.rotation;
        newRot.x = 0;
        newRot.z = 0;
        transform.rotation = newRot;
    }
    void TrippleShot()
    {
        SummonStraightFireball(transform.forward + transform.position, new FireballStraightAttackState());
        SummonStraightFireball(transform.forward + transform.right * 0.3f + transform.position, new FireballStraightAttackState());
        SummonStraightFireball(transform.forward - transform.right * 0.3f + transform.position, new FireballStraightAttackState());
    }
    public void StartTpAttack()
    {
        StartCoroutine(TpFireCycle(_amountOfTps));
    }
    //


    public void SpawnMinions()
    {
        foreach(BossSpawner bossSpawner in _spawnPoints)
        {
            if(UnityEngine.Random.Range(0,2) == 0)
            {
                bossSpawner.SpawnEnemy(_possibleMinions[UnityEngine.Random.Range(0, _possibleMinions.Count)]);
            }
        }
    }
    public void RotateToTarget()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(direction).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

}