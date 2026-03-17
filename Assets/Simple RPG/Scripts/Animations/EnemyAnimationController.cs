using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : BaseAnimationController
{
    private EnemyAI _enemy;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private int _currentState;
    private int _idleDirection;

    private static readonly int Idle = Animator.StringToHash("Idle_right");
    private static readonly int Walk = Animator.StringToHash("Walk_right");

    private void Start()
    {
        GetComponents();
        _idleDirection = Idle;
    }

    private void Update()
    {
        var state = GetState();

        if (state == _currentState) return;

        _animator.CrossFade(state, 0);

        _currentState = state;
    }

    protected override void GetComponents()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<EnemyAI>();
    }

    protected override int GetState()
    {
        if (Time.time < lockedTill) return _currentState;

        _spriteRenderer.flipX = _enemy.IsFacingRight;

        if (_enemy.IsMoving)
            return Walk;

        return _idleDirection;
    }
}