using Cinemachine.Utility;
using Player.Input;
using SimpleRPG.Services.Input;
using System.Collections;
using Unity.VisualScripting;
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
        private Camera _camera;

        private bool _isFacingRight;
        private bool _isDashing;
        private Vector2 _frameInput;
        private Vector2 _frameVelocity;
        #endregion

        #region Properties
        public Vector2 FrameInput => _frameInput;
        #endregion

        private void OnEnable()
        {
            _inputService.OnMovementAction += GetMoveInput;
            _inputService.OnDashToggle += OnDash;
        }

        private void Start()
        {
            _camera = Camera.main;
            _rb = GetComponent<Rigidbody2D>();
            _bc = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            if (!_isDashing)
                HandleDirection();
            ApplyMovement();
        }

        private void OnDisable()
        {
            _inputService.OnMovementAction -= GetMoveInput;
            _inputService.OnDashToggle -= OnDash;
        }

        #region Movement
        private void GetMoveInput(Vector2 vector) => _frameInput = vector;

        private void HandleDirection()
        {
            float speed = _movementSettings.Speed * (_inputService.IsSprint ? _movementSettings.SpeedMultiplier : 1);
            _frameVelocity = _frameInput * speed;

            CheckDirectionToFace(_camera.ScreenToWorldPoint(_inputService.CurrentMausePosition).x < transform.position.x);
        }

        private void ApplyMovement() => _rb.linearVelocity = _frameVelocity;

        private void OnDash()
        {
            if (_isDashing) return;

            Vector2 velocityToDash = Vector2.zero;
            float speed = _movementSettings.Speed * (_inputService.IsSprint ? _movementSettings.SpeedMultiplier : 1);

            if (_frameInput != Vector2.zero)
                velocityToDash = _frameInput * speed;
            else
                velocityToDash = _isFacingRight ? Vector2.right * (speed / 1) : Vector2.left * (speed / 1);
            
            StartCoroutine(DashCoroutine(velocityToDash));
        }

        private IEnumerator DashCoroutine(Vector2 velocity)
        {
            _isDashing = true;

            _frameVelocity = velocity * _movementSettings.DashSpeed;
            yield return new WaitForSeconds(_movementSettings.DashDuration);

            float elapsedTime = 0;
            Vector2 startingVelocity = _frameVelocity;

            while (elapsedTime < _movementSettings.DashSlowdown)
            {
                _frameVelocity = Vector2.Lerp(startingVelocity, Vector2.zero, elapsedTime / _movementSettings.DashSlowdown);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _frameVelocity = Vector2.zero;
            _isDashing = false;
        }

        private void Turn()
        {
            if (_isDashing) return;

            _isFacingRight = !_isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        private void CheckDirectionToFace(bool isMovingRight)
        {
            if (isMovingRight != _isFacingRight)
                Turn();
        }
        #endregion
    }
}