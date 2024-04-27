using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class SkeletonStateManager : MonoBehaviour
{
    [SerializeField] public float _walkSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] public float _attackDistance;
    [SerializeField] bool startWithIdle;

    public SkeletonBaseState currnetState;
    public SkeletonAttackState skeletonAttack = new SkeletonAttackState();
    public SkeletonIdleState skeletonIdle = new SkeletonIdleState();
    public SkeletonWalkState skeletonWalk = new SkeletonWalkState();

    NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;
    GameObject player;
    bool isRotating = false;
    Transform currentTarget;
    private void Start()
    {
        player = GameObject.FindObjectOfType<TestPlayerExistance>().gameObject;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        transform.LookAt(player.transform);
        SetTarget(player.transform);
        SetSpeed(0);
        if(startWithIdle) SwitchState(skeletonIdle);
        else SwitchState(skeletonWalk);
    }
    private void Update()
    {
        currnetState.UpdateState(this);
        navMeshAgent.destination = currentTarget.position;

    }
    private void FixedUpdate()
    {
        if (isRotating && (currnetState == skeletonAttack)) RotateToTarget();
    }
    public void SwitchState(SkeletonBaseState newState)
    {
        currnetState = newState;
        currnetState.EnterState(this);
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
    public void BrainActivation()
    {
        SwitchState(skeletonWalk);
    }

    public void RotateToTarget()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(direction).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }

    void StartStopRotation(int newValue)
    {
        isRotating = (newValue == 1);
    }
    void CheckIfTooFar()
    {
        if(GetRemainingDistance() > _attackDistance)
        {
            animator.SetBool("isAttacking",false);
        }
    }
    void CheckIfAnimation()
    {
        if (!animator.GetBool("isAttacking"))
        {
            SwitchState(skeletonWalk);
        }
    }
}
