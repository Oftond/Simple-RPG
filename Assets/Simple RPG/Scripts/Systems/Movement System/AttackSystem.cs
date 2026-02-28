using SimpleRPG.Services.Input;
using UnityEngine;
using Zenject;

public class AttackSystem : MonoBehaviour
{
    [Inject] private PlayerInputService _inputService;



    private void OnEnable()
    {
        _inputService.OnAttackToggle += OnAttack;
    }

    private void OnAttack()
    {

    }
}