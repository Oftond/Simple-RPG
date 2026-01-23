using Cinemachine.Utility;
using Player.Input;
using SimpleRPG.Services.Input;
using UnityEngine;
using Zenject;

namespace Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class PlayerMovementSystem : MonoBehaviour
    {
        [Inject] private PlayerInputService _inputService;
        [SerializeField] private PlayerMovementSettings _movementSettings;
        [SerializeField] private PlayerInputSettings _inputSettings;

        #region Private Fields
        private Rigidbody2D _rb;
        private BoxCollider2D _bc;

        private Vector2 _frameInput;
        private Vector2 _frameVelocity;
        #endregion

        #region Properties
        public Vector2 FrameInput => _frameInput;
        #endregion

        private void OnEnable()
        {
            _inputService.OnMovementAction += GetMoveInput;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _bc = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            HandleDirection();
            ApplyMovement();
        }

        private void OnDisable()
        {
            _inputService.OnMovementAction -= GetMoveInput;
        }

        #region Movement
        private void GetMoveInput(Vector2 vector) => _frameInput = vector;
        private void HandleDirection()
        {
            float speed = _movementSettings.Speed * (_inputService.IsSprint ? _movementSettings.SpeedMultiplier : 1);
            _frameVelocity = _frameInput * speed;
        }
        private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;
        #endregion
    }
}