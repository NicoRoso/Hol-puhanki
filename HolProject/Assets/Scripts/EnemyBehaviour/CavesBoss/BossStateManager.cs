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
    public Action changeAttack;
    Transform player;

    [SerializeField] public Animator _animator;


    [SerializeField] float _rotationSpeed;
    [SerializeField] int maxAttacksInARow;
    [SerializeField] public float _barrierVisualFadeTime;
    [SerializeField] int weakStateMaxTime;

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
    [SerializeField] GameObject _barrierVisual;
    [SerializeField] ParticleSystem _tpParticle;

    public bool bossfightStarted = false;
    [HideInInspector]
    public bool isKillable = false;
    BossHealth bossHealth;
    AudioManager audioManager;
    int attacksInARow;
    public void SwitchState(BossBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
        //Debug.Log(currentState);
    }
    private void Start()
    {
        bossHealth = GetComponent<BossHealth>();
        audioManager = GetComponent<AudioManager>();
        attacksInARow = 0;
        player = GameObject.FindObjectOfType<PlayerStatSys>().transform;
        SwitchBarrierState(false);
        SwitchState(bossIdle);
    }
    private void Update()
    {
        currentState.UpdateState(this);
        if(currentState != bossDeath) RotateToTarget();
    }

    // curve attack
    public void SummonCurveFireball(Vector3 pos, float rotSpeed, float flyAwaySpeed)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        fireball.GetComponent<FireballStateManager>().OnSpawn(new FireballCurveAttackState(), ref rotationStop, transform, rotSpeed, flyAwaySpeed);
    }
    void SummonCurveWave(float rotSpeed,float flyAwaySpeed)
    {
        audioManager.Play("throw" + UnityEngine.Random.Range(2, 7));
        SummonCurveFireball(transform.position + new Vector3(1, 1.5f, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(-1, 1.5f, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 1.5f, 1), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 1.5f, -1), rotSpeed, flyAwaySpeed);
    }
    IEnumerator CurveWavesSpawnCycle(int waves, float breakTime, float rotSpeed, float flyAwaySpeed)
    {
        int wavesPassed = 0;
        audioManager.Play("curveAttack");
        while(wavesPassed < waves)
        {
            SummonCurveWave(rotSpeed,flyAwaySpeed);
            wavesPassed++;
            yield return new WaitForSeconds(breakTime);
        }
        yield return new WaitForSeconds(breakTime);
        rotationStop?.Invoke();
        yield return new WaitForSeconds(0.8f);
        audioManager.Play("down");
        _animator.SetTrigger("centerAttackEndDown");
        
        StartCoroutine(WaitforSecondsBeforeNexstState(2));
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
        audioManager.Play("throw" + UnityEngine.Random.Range(2, 7));
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
        audioManager.Play("roundAttack");
        while (wavesPassed < waves)
        {
            
            SummonRoundWave();
            wavesPassed++;
            yield return new WaitForSeconds(breakTime);
        }
        _animator.SetTrigger("centerAttackEnd");
        StartCoroutine(WaitforSecondsBeforeNexstState(1));
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
            SetTpParticles(true);
            yield return new WaitForSeconds(0.5f);
            TpToPointAndFire(GetRandomTpPointsFromList(amountOfTps)[TpsDone]);
            SetTpParticles(false);
            TpsDone++;
            yield return new WaitForSeconds(_afterTpsDelay);
        }
        yield return new WaitForSeconds(_afterTpsDelay);
        SetTpParticles(true);
        yield return new WaitForSeconds(0.5f);
        audioManager.Play("tpSound");
        transform.position = _centerPoint.position;
        transform.LookAt(player);
        Quaternion newRot = transform.rotation;
        newRot.z = 0;
        newRot.x = 0;
        transform.rotation = newRot;
        SetTpParticles(false);
        StartCoroutine(WaitforSecondsBeforeNexstState(1));
        yield break;
    }
    void TpToPointAndFire(Transform point)
    {
        audioManager.Play("tpSound");
        transform.position = point.position;
        transform.LookAt(player);
        _animator.SetTrigger("frontAttack");
        audioManager.Play("forwardAttack");
        Quaternion newRot = transform.rotation;
        newRot.x = 0;
        newRot.z = 0;
        transform.rotation = newRot;
    }
    void TrippleShot()
    {
        audioManager.Play("throw" + UnityEngine.Random.Range(2, 7));
        SummonStraightFireball(transform.forward + transform.position + new Vector3(0,1.5f,0), new FireballStraightAttackState());
        SummonStraightFireball(transform.forward + transform.right * 0.3f + transform.position + new Vector3(0, 1.5f, 0), new FireballStraightAttackState());
        SummonStraightFireball(transform.forward - transform.right * 0.3f + transform.position + new Vector3(0, 1.5f, 0), new FireballStraightAttackState());
    }
    public void StartTpAttack()
    {
        StartCoroutine(TpFireCycle(_amountOfTps));
    }
    //

    public void SpawnMinions()
    {
        audioManager.Play("spawnAttack");
        foreach(BossSpawner bossSpawner in _spawnPoints)
        {
            if(UnityEngine.Random.Range(0,2) == 0)
            {
                bossSpawner.SpawnEnemy(_possibleMinions[UnityEngine.Random.Range(0, _possibleMinions.Count)]);
            }
        }
        StartCoroutine(WaitforSecondsBeforeNexstState(8));
    }

    public IEnumerator WaitforSecondsBeforeNexstState(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        TryMakeNextAttack();
        yield break;
    }
    public void RotateToTarget()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(direction).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }
    void StartCenterAttack()
    {
        if (currentState == bossCurveFire) StartCurveAttack();
        if (currentState == bossRoundFire) StartRoundAttack();
    }

    public void TryMakeNextAttack()
    {
        if(attacksInARow < maxAttacksInARow)
        {
            changeAttack?.Invoke();
            attacksInARow++;
        }
        else
        {
            attacksInARow = 0;
            SwitchState(bossWeak);
            StartCoroutine(WaitOnWeakState());
        }
    }
    IEnumerator WaitOnWeakState()
    {
        yield return new WaitForSeconds(weakStateMaxTime);
        if(currentState == bossWeak)
        {
            TryMakeNextAttack();
            SwitchBarrierState(true);
        }
        yield break;
    }
    public void SwitchBarrierState(bool hidden)
    {
        //_barrierVisual.enabled = hidden;
        SetBarriarParticles(hidden);
        isKillable = hidden;
    }
    void SetBarriarParticles(bool mode)
    {
        foreach(ParticleSystem particleSystem in _barrierVisual.GetComponentsInChildren<ParticleSystem>())
        {
            if(mode)
            {
                particleSystem.loop = true;
                particleSystem.time = 0;
                particleSystem.Play();
            }
            else
            {
                particleSystem.Stop();
            }
        }
    }
    void SetTpParticles(bool mode)
    {
        if (mode)
        {
            _tpParticle.time = 0;
            _tpParticle.Play();
        }
        else
        {
            _tpParticle.Stop();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<SwordAttack>(out SwordAttack sword) && !isKillable)
        {
            bossHealth.TakeDamage((int)GameObject.FindObjectOfType<PlayerStatSys>().Attack());
            audioManager.Play("hit" + UnityEngine.Random.Range(1, 4));
            if (bossHealth.GetHealth() <= 0)
            {
                audioManager.Play("death");
                _animator.SetTrigger("isDead");
            }
        }
    }


}