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

    [SerializeField] TrailRenderer _trailRenderer;

    public static Action<bool> OnDashed;

    [SerializeField] private AudioClip _dashClip;
    public static Action<AudioClip, float, float> OnDashedSounded;

    private void Awake()
    {
        _trailRenderer.enabled = false;
    }

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
        _trailRenderer.enabled = true;
        _lastDashTime = Time.time;
        _canDash = false;
        OnDashed(true);
        OnDashedSounded?.Invoke(_dashClip, 1f, 1f);

        float startTime = Time.time;
        while (Time.time < startTime + _dashTime)
        {
            GetComponent<CharacterController>().Move(moveDirection * _dashSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(_dashCooldown);

        _canDash = true;
        OnDashed(false);
        _trailRenderer.enabled = false;
    }
}
