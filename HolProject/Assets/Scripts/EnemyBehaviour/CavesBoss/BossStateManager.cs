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
    [SerializeField] public float _curveFireballRotationSpeed;
    [SerializeField] public float _curveFireballFlyAwaySpeed;
    [SerializeField] public int _curveAttackWavesAmount;
    [SerializeField] public float _curveAttackWavesSpawnDelay;
    public void SwitchState(BossBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    private void Start()
    {
        SwitchState(bossCurveFire);
    }
    public void SummonCurveFireball(Vector3 pos, float rotSpeed, float flyAwaySpeed)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        fireball.GetComponent<FireballStateManager>().OnSpawn(new FireballCurveAttackState(), ref rotationStop, transform, rotSpeed, flyAwaySpeed);
    }
    public void SummonStraightFireball(Vector3 pos, FireballBaseState state, Vector3 dir)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        fireball.GetComponent<FireballStateManager>().OnSpawn(state, transform, dir);
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
    public void StartCurveAttack(int waves, float breakTime, float rotSpeed, float flyAwaySpeed)
    {
        StartCoroutine(CurveWavesSpawnCycle(waves,breakTime,rotSpeed,flyAwaySpeed));
    }
}