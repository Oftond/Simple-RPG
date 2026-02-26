using UnityEngine;

namespace Player.Movement
{
    [CreateAssetMenu(menuName = "Simple RPG/Player/Movement Settings")]
    public class PlayerMovementSettings : ScriptableObject
    {
        [Header("Movement Stats")]
        [Tooltip("Character movement speed.")]
        [SerializeField] private float _speed = 1;

        [Tooltip("Character's running speed.")]
        [SerializeField] private float _speedMultiplier = 1.2f;

        [Header("Dash Stats")]
        [Tooltip("Dash speed.")]
        [SerializeField] private float _dashSpeed = 2;

        [Tooltip("Cooldown time of the dodge.")]
        [SerializeField] private float _dashCooldown = 1;

        [Tooltip("Time to dodge.")]
        [SerializeField] private float _dashDuration = 0.3f;

        [Tooltip("Deceleration time after a jerk (damping). Affects the speed of the dash.")]
        [SerializeField] private float _dashSlowdown = 0.3f;

        #region Properties
        public float Speed => _speed;
        public float SpeedMultiplier => _speedMultiplier;

        public float DashSpeed => _dashSpeed;
        public float DashCooldown => _dashCooldown;
        public float DashDuration => _dashDuration;
        public float DashSlowdown => _dashSlowdown;
        #endregion
    }
}