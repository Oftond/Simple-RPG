using Player.Movement;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : BaseAnimationController
{
    [Inject] private PlayerMovementSystem _pms;

    private Animator _animator;
    private int _currentState;
    private int _idleDirection;

    protected override void GetComponents()
    {

    }

    protected override int GetState()
    {
        throw new System.NotImplementedException();
    }
}