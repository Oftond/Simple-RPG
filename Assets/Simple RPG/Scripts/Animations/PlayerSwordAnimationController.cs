using GameControls;
using SimpleRPG.Services.Input;
using UnityEngine;
using Zenject;

public class PlayerSwordAnimationController : BaseAnimationController
{
    [Inject] protected PlayerInputService _pis;
    [SerializeField] protected Transform _playerTransform;

    protected Animator _animator;
    protected SpriteRenderer _spriteRenderer;
    protected Camera _camera;

    protected static readonly int SwordSlash = Animator.StringToHash("SwordSlash");

    protected void OnEnable()
    {
        _pis.OnAttackToggle += OnAttack;
    }

    protected void Start()
    {
        _camera = Camera.main;
        GetComponents();
        HideSword();
    }

    private void OnDisable()
    {
        _pis.OnAttackToggle -= OnAttack;
    }

    protected void HideSword()
    {
        _spriteRenderer.enabled = false;
    }

    protected void ShowSword()
    {
        _spriteRenderer.enabled = true;
    }

    protected override void GetComponents()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override int GetState()
    {
        throw new System.NotImplementedException();
    }

    protected void OnAttack()
    {
        //var mouseWorldPos = _camera.ScreenToWorldPoint(_pis.CurrentMausePosition);

        //Vector2 direction = mouseWorldPos - _playerTransform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, angle);

        ShowSword();
        _animator.Play(SwordSlash, 0, 0);
    }
}