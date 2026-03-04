using GameControls;
using SimpleRPG.Services.Input;
using UnityEngine;
using Zenject;

public class PlayerSwordAnimationController : BaseAnimationController
{
    [Inject] protected PlayerInputService _pis;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected Camera _camera;

    protected static readonly int SwordSlash = Animator.StringToHash("SwordSlash");

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
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected override int GetState()
    {
        throw new System.NotImplementedException();
    }

    protected void OnAttack() => _animator.Play(SwordSlash, 0, 0);
}