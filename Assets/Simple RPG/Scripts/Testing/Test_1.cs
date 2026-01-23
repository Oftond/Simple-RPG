using SimpleRPG.Services.Input;
using UnityEngine;

public class Test_1 : MonoBehaviour
{
    [SerializeField] private PlayerInputService _playerInputService;

    private void OnEnable()
    {
        _playerInputService.OnMovementAction += Move;
    }

    private void OnDisable()
    {
        _playerInputService.OnMovementAction -= Move;
    }

    private void Move(Vector2 vector)
    {
        print(vector);
    }
}