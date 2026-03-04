using SimpleRPG.Services.Input;
using UnityEngine;
using Zenject;

public class WeaponAnimationController : BaseAnimationController
{
    [Inject] private PlayerInputService _pis;

    [SerializeField] private WeaponsSettings _weaponSettings;

    private Animator _animator;
    private Camera _camera;

    protected override void GetComponents()
    {
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main;
    }

    protected override int GetState()
    {
        return 0;
    }
}