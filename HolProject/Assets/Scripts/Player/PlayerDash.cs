using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashCooldown;
    private float _lastDashTime;
    private bool _canDash = true;

    public static Action<bool> OnDashed;

    private void OnEnable()
    {
        Movement.OnDashed += Dash;
    }

    private void OnDisable()
    {
        Movement.OnDashed -= Dash;
    }

    private void Dash(Vector3 moveDirection)
    {
        if (_canDash)
        {
            StartCoroutine(DashCoroutine(moveDirection));
        }
    }

    private IEnumerator DashCoroutine(Vector3 moveDirection)
    {
        _lastDashTime = Time.time;
        _canDash = false;
        OnDashed(true);

        float startTime = Time.time;
        while (Time.time < startTime + _dashTime)
        {
            GetComponent<CharacterController>().Move(moveDirection * _dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_dashCooldown);

        _canDash = true;
        OnDashed(false);
    }
}
