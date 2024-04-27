using UnityEngine;


[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string SPEED = "Speed";
    private const string ATTACK = "AttackIndex";
    private const string IsDashed = "IsDashed";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Movement.OnMoved += OnMove;
        PlayerAttack.attackAction += Attack;
        PlayerDash.OnDashed += Dash;
    }

    private void OnDisable()
    {
        Movement.OnMoved -= OnMove;
        PlayerAttack.attackAction -= Attack;
        PlayerDash.OnDashed -= Dash;
    }

    private void OnMove(Vector2 moveInput) 
    {
        float speed = moveInput.normalized.magnitude;
        _animator.SetFloat(SPEED, speed);
    }
    private void Attack(int attackIndex)
    {
        _animator.SetInteger(ATTACK, attackIndex);
    }

    private void Dash(bool value)
    {
        _animator.SetBool(IsDashed, value);
    }

}