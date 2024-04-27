using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WizardStateManager : MonoBehaviour
{
    [SerializeField] public float _moveSpeed;
    [SerializeField] float _rotationSpeed;
    [SerializeField] public float _attackDistance;
    [SerializeField] GameObject _fireballPrefab;
    [SerializeField] float _fireballSpeed;

    NavMeshAgent navMeshAgent;
    [HideInInspector]
    public Animator animator;
    EnemyHealth enemyHealth;


    public WizardBaseState currentState;
    public WizardAttackState wizardAttack = new WizardAttackState();
    public WizardWalkState wizardWalk = new WizardWalkState();
    public WIzzardDeathState wizzardDeath = new WIzzardDeathState();

    Transform currentTarget;
    Transform player;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameObject.FindObjectOfType<TestPlayerExistance>().transform;
        SetTarget(player);
        SwitchState(wizardWalk);
    }
    private void Update()
    {
        navMeshAgent.destination = currentTarget.position;
        currentState.UpdateState(this);
        
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

    void SummonStraightFireball(Vector3 pos, FireballBaseState state)
    {
        GameObject fireball = Instantiate(_fireballPrefab, pos, Quaternion.identity, null);
        Vector3 direction = fireball.transform.position - transform.position;
        direction.y = 0;
        direction = direction.normalized;
        fireball.GetComponent<FireballStateManager>().OnSpawn(state, transform, direction, _fireballSpeed);
    }
    void SummonFireball()
    {
        SummonStraightFireball(transform.position + transform.forward * 0.1f + new Vector3(0, 1.5f, 0), new FireballStraightAttackState());
    }
    public void RotateToTarget()
    {
        Vector3 direction = (player.transform.position - transform.position);
        direction.y = 0f;
        Quaternion newRotation = Quaternion.LookRotation(direction).normalized;
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
    }
    void CheckDistance()
    {
        if(GetRemainingDistance() > _attackDistance)
        {
            animator.SetBool("isAttacking",false);
        }
    }
    void CheckAnimationVar()
    {
        if(!animator.GetBool("isAttacking"))
        {
            SwitchState(wizardWalk);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (true) // если оружие
        {
            //урон брать из оружия
            //playerstatsys.attack
            //animator.SetTrigger("isHit");
            if(enemyHealth.GetHealth() <= 0)
            {
                SwitchState(wizzardDeath);
            }
        }
    }
}
