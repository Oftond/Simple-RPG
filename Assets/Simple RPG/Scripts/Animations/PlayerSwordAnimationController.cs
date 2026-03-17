using GameControls;
using SimpleRPG.Services.Input;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerSwordAnimationController : BaseAnimationController
{
    [Inject] protected PlayerInputService _pis;
    [SerializeField] private WeaponsSettings _weaponSettings;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected Camera _camera;

    protected bool _isAttacking;

    protected static readonly int SwordRightSlash = Animator.StringToHash("SwordRightHit");
    protected static readonly int SwordLeftSlash = Animator.StringToHash("SwordLeftHit");
    protected static readonly int SwordUpSlash = Animator.StringToHash("SwordUpHit");
    protected static readonly int SwordDownSlash = Animator.StringToHash("SwordDownHit");

    protected void OnEnable()
    {
        _pis.OnAttackToggle += OnAttack;
    }

    protected void Awake()
    {
        _camera = Camera.main;
        GetComponents();
    }

    private void OnDisable()
    {
        _pis.OnAttackToggle -= OnAttack;
    }

    protected override void GetComponents()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override int GetState()
    {
        if (_animator == null || _camera == null) return SwordRightSlash;

        Vector3 cursorWorldPos = _camera.ScreenToWorldPoint(_pis.CurrentMausePosition);
        Vector2 direction = (cursorWorldPos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360f;

        int animationHash = angle switch
        {
            >= 45f and < 135f => SwordUpSlash,
            >= 135f and < 225f => SwordLeftSlash,
            >= 225f and < 315f => SwordDownSlash,
            _ => SwordRightSlash
        };

        return animationHash;
    }

    protected void OnAttack()
    {
        if (_isAttacking) return;
        StartCoroutine(AttackCoroutine());
    }

    protected IEnumerator AttackCoroutine()
    {
        _isAttacking = true;
        _animator.Play(GetState(), 0, 0);
        yield return new WaitForSeconds(_weaponSettings.Weapons[0].AnimationDuration);
        _isAttacking = false;
    }
}