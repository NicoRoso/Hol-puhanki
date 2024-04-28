using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private PlayerInput _playerInput;
    [Space]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private CharacterController _characterController;
    private Vector2 _moveInput;
    private Vector3 moveDirectionRelativeToCamera;
    [Space]
    [Header("Actions")]
    public static Action<Vector2> OnMoved;
    public static Action<Vector3> OnDashed;

    [SerializeField] private AudioClip[] _footstepsClip;

    [Header("AudioActions")]
    public static Action<AudioClip[], float, float> OnMovedSounded;
    public static Action<AudioClip, float, float> OnSounded;

    [SerializeField] private GameObject _pause;

    [Header("Gravity")]
    [SerializeField] private float _gravity = 9.81f;
    private Vector3 _fallVelocity = Vector3.zero;

    private void Awake()
    {
        if (_pause != null)
        {
            _pause.SetActive(true);
        }

        _characterController = GetComponent<CharacterController>();
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Movement.Walk.performed += ctx => Move(ctx.ReadValue<Vector2>());
        _playerInput.Movement.Walk.canceled += ctx => StopMoving();
        _playerInput.Dash.Dash.performed += ctx => PerformedDash();
    }
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Move(Vector2 moveInput)
    {
        if (Time.timeScale != 0)
        {
            _moveInput = moveInput;
            OnMoved?.Invoke(_moveInput);
        }
    }

    private void StopMoving()
    {
        _moveInput = Vector2.zero;
        OnMoved?.Invoke(_moveInput);
    }

    private void PerformedDash()
    {
        if (Time.timeScale != 0)
        {
            Vector3 dashDirection = moveDirectionRelativeToCamera;
            OnDashed?.Invoke(dashDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;

        if (moveDirection != Vector3.zero)
        {
            float cameraRotationAngle = Camera.main.transform.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, cameraRotationAngle, 0f) * Quaternion.LookRotation(moveDirection);
            moveDirectionRelativeToCamera = Quaternion.Euler(0f, cameraRotationAngle, 0f) * moveDirection;
            _characterController.Move(moveDirectionRelativeToCamera * _moveSpeed * Time.fixedDeltaTime);
        }

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _fallVelocity.y -= _gravity * Time.deltaTime;
        _characterController.Move(_fallVelocity * Time.deltaTime);
    }

    public void PlaySound(AudioClip clip)
    {
        OnSounded?.Invoke(clip, 1f, 1f);
    }

    public void PlaySoundWithRandom()
    {
        OnMovedSounded?.Invoke(_footstepsClip, 1f, 1f);
    }
}
