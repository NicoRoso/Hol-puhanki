using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BossStateManager : MonoBehaviour
{
    public BossBaseState currentState;
    public BossSummonState bossSummon = new BossSummonState();
    public BossCurveFireState bossCurveFire = new BossCurveFireState();
    public BossRoundFireState bossRoundFire = new BossRoundFireState();
    public BossTpFireState bossTpFire = new BossTpFireState();
    public BossWeaknessState bossWeak = new BossWeaknessState();
    public BossDeathState bossDeath = new BossDeathState();
    Action rotationStop;

    [SerializeField] public GameObject _fireballPrefab;
    [SerializeField] float _curveFireballRotationSpeed;
    [SerializeField] float _curveFireballFlyAwaySpeed;
    [SerializeField] int _curveAttackWavesAmount;
    [SerializeField] float _curveAttackWavesSpawnDelay;
    [SerializeField] float _straightFireballSpeed;
    [SerializeField] int _roundAttackWavesAmount;
    [SerializeField] float _roundAttackWavesSpawnDelay;
    public void SwitchState(BossBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    private void Start()
    {
        SwitchState(bossRoundFire);
    }


    // curve attack
    public void SummonCurveFireball(Vector3 pos, float rotSpeed, float flyAwaySpeed)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        fireball.GetComponent<FireballStateManager>().OnSpawn(new FireballCurveAttackState(), ref rotationStop, transform, rotSpeed, flyAwaySpeed);
    }
    void SummonCurveWave(float rotSpeed,float flyAwaySpeed)
    {
        SummonCurveFireball(transform.position + new Vector3(1, 0, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(-1, 0, 0), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 0, 1), rotSpeed, flyAwaySpeed);
        SummonCurveFireball(transform.position + new Vector3(0, 0, -1), rotSpeed, flyAwaySpeed);
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
        yield break;
    }
    public void StartCurveAttack()
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
        SummonStraightFireball(transform.position + new Vector3(0.5f,0,0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(-0.5f, 0, 0), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(0, 0, 0.5f), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(0, 0, -0.5f), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(0.25f, 0, 0.25f), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(0.25f, 0, -0.25f), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(-0.25f, 0, 0.25f), new FireballRoundAttackState());
        SummonStraightFireball(transform.position + new Vector3(-0.25f, 0, -0.25f), new FireballRoundAttackState());
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



}