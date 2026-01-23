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
        [Tooltip("Cooldown time of the dodge.")]
        [SerializeField] private float _dashCooldown = 2;

        [Tooltip("Time to dodge.")]
        [SerializeField] private float _dashDuration = 0.1f;

        [Tooltip("The distance that the player will cover after dodging.")]
        [SerializeField] private float _dashDistance = 2.5f;

        #region Properties
        public float Speed => _speed;
        public float SpeedMultiplier => _speedMultiplier;
        public float DashCooldown => _dashCooldown;
        public float DashDuration => _dashDuration;
        public float DashDistance => _dashDistance;
        #endregion
    }
}