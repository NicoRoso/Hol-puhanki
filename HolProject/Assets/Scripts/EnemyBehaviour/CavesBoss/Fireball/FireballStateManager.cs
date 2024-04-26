using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireballStateManager : MonoBehaviour
{
    [SerializeField] public  Transform _rotateCenter;
    [SerializeField] public float _rotateSpeed;
    public float flyAwaySpeed;
    public FireballBaseState currentState;
    public FireballCurveAttackState curveAttack = new FireballCurveAttackState();
    public FireballStraightAttackState straightAttack = new FireballStraightAttackState();

    bool shouldStop = false;
    private void Start()
    {
        SetState(curveAttack);
    }
    private void Update()
    {
        currentState?.UpdateState(this);
    }

    public void GoFurtherFromCenter()
    {
        transform.position = transform.position + ((transform.position - _rotateCenter.position).normalized)*Time.deltaTime*flyAwaySpeed;
    }
    public void IncreaseSizeToNormal(float time)
    {
        StartCoroutine(ScaleChanger(time));
    }
    IEnumerator ScaleChanger(float seconds)
    {
        float secondsPassed = 0;
        while(secondsPassed < seconds)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(1, 1, 1), secondsPassed / seconds);
            secondsPassed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield break;
    }
    public void SetState(FireballBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    IEnumerator RotationRulesChanger()
    {
        while(!shouldStop)
        {
            yield return null;
        }
        while(flyAwaySpeed > 0)
        {
            flyAwaySpeed -= Time.deltaTime*100;
            yield return null;
        }
        flyAwaySpeed = 0;
        while(_rotateSpeed > 0)
        {
            _rotateSpeed -= Time.deltaTime*200;
            yield return null;
        }
        _rotateSpeed = 0;
        yield return null;
        Vector3 currentPos = transform.position;
        transform.DOMove(currentPos + new Vector3(0, 1, 0), 0.5f).SetEase(Ease.InOutExpo);
        while (transform.position.y < currentPos.y + 1)
        {
            yield return null;
        }
        transform.DOMove(currentPos + new Vector3(0, -1.25f, 0), 0.4f).SetEase(Ease.InOutExpo);
        while (transform.position.y > currentPos.y - 1.2f)
        {
            yield return null;
        }
        //explosion collider and effect
        Destroy(gameObject);
        yield break;

    }
    public void StartCurveAttackControl()
    {
        StartCoroutine(RotationRulesChanger());
    }
    public void SetStop()
    {
        shouldStop = true;
    }

    public void OnSpawn(FireballBaseState state,Action stopRotation, float rotationSpeed, float flyAwaySpeed)
    {
        _rotateSpeed = rotationSpeed;
        this.flyAwaySpeed = flyAwaySpeed;
        SetState(state);
        stopRotation += SetStop;
    }
    public void OnSpawn(FireballBaseState state)
    {
        SetState(state);
    }
}
