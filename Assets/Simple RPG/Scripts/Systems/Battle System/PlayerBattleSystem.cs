using SimpleRPG.Services.Input;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerBattleSystem : MonoBehaviour
{
    [Inject] private PlayerInputService _pis;
    [SerializeField] private GameObject _weapon;
    [SerializeField] private WeaponsSettings _weaponSettings;

    private bool _isAttacking;

    private void OnEnable()
    {
        if (_weapon.activeSelf) _weapon.SetActive(false);
        _pis.OnAttackToggle += DoAttack;
    }

    private void OnDisable()
    {
        _pis.OnAttackToggle -= DoAttack;
    }

    private void DoAttack()
    {
        if (_isAttacking) return;
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _weapon.SetActive(true);
        _isAttacking = true;

        yield return new WaitForSeconds(_weaponSettings.Weapons[0].AnimationDuration);

        _isAttacking = false;
        _weapon.SetActive(false);
    }

    private void GetState()
    {

    }
}