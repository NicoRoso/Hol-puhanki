using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardStateManager : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;
    EnemyHealth enemyHealth;


    WizardBaseState currentState;
    Transform currentTarget;
    private void Start()
    {
        navMeshAgent =GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        navMeshAgent.destination = currentTarget.position;
    }
    public void SwitchState(WizardBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }
    public void SetSpeed(float speed)
    {
        navMeshAgent.speed = speed;
    }
    public void SetTarget(Transform target)
    {
        currentTarget = target;
    }
    public float GetRemainingDistance()
    {
        return navMeshAgent.remainingDistance;
    }
}
