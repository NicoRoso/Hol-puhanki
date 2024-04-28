using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private int _countAttackClick;

    public static Action<int> attackAction;

    private bool isAttacking;

    private void Awake()
    {
        isAttacking = false;
        _countAttackClick = 0;
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Attack.Attack.performed += ctx => AttackFunction();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public void AttackFunction()
    {
        if (Time.timeScale != 0)
        {
            if (_countAttackClick < 3 && !isAttacking)
            {
                _countAttackClick++;
                isAttacking = true;
                attackAction?.Invoke(_countAttackClick);
            }
        }
    }

    public void FalseIsAttack()
    {
        isAttacking = false;
    }

    public void ResetAttackPhase()
    {
        _countAttackClick = 0;
        attackAction?.Invoke(_countAttackClick);
        isAttacking = false;
    }
}
