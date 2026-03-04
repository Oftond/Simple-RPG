using Player.Movement;
using SimpleRPG.Services.Input;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    [Inject] private PlayerInputService _pis;
    [SerializeField] private PlayerMovementSettings _movementSettings;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private int _currentState;
    private int _idleDirection;
    private bool _isDashing;

    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Dash = Animator.StringToHash("Dash");

    private void OnEnable()
    {
        _pis.OnDashToggle += OnDash;
    }

    private void Start()
    {
        GetComponents();
        _idleDirection = Idle;
        _camera = Camera.main;
    }

    private void Update()
    {
        var state = GetState();

        if (state == _currentState) return;

        _animator.CrossFade(state, 0, 0);

        _currentState = state;
    }

    private void OnDisable()
    {
        _pis.OnDashToggle -= OnDash;
    }

    protected override void GetComponents()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override int GetState()
    {
        if (Time.time < lockedTill) return _currentState;

        if (_camera.ScreenToWorldPoint(_pis.CurrentMausePosition).x > transform.position.x)
            _spriteRenderer.flipX = false;
        else _spriteRenderer.flipX = true;

        if (_isDashing)
            return Dash;

        if (_pis.IsMove)
        {
            if (_pis.IsSprint) _animator.speed = 2;
            else _animator.speed = 1;
            return Walk;
        }

        if (_animator.speed > 1) _animator.speed = 1;

        return _idleDirection;
    }

    private void OnDash()
    {
        if (_isDashing) return;
        StartCoroutine(ActionCoroutine());
    }

    private IEnumerator ActionCoroutine()
    {
        _isDashing = true;
        yield return new WaitForSeconds(_movementSettings.DashDuration + _movementSettings.DashSlowdown);
        _isDashing = false;
    }
}