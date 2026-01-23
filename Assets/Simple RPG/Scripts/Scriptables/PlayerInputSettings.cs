using UnityEngine;

namespace Player.Input
{
    [CreateAssetMenu(menuName = "Simple RPG/Player/Input Settings")]
    public class PlayerInputSettings : ScriptableObject
    {
        #region Fields
        [Header("Layers")]
        [Tooltip("The layer on which the player is located.")]
        [SerializeField] private LayerMask _playerLayer;

        [Header("Input")]
        [Tooltip("Converts all values to integer values. It is necessary for the correct operation of the gamepads.")]
        [SerializeField] private bool _snapInput = true;

        [Tooltip("Min. input for up/down movement. Prevents the sticks from drifting.")]
        [SerializeField, Range(0, 1)] private float _verticalDeadZoneTreshold = 0.1f;

        [Tooltip("Min. input for left/right movement. Prevents the sticks from drifting.")]
        [SerializeField, Range(0, 1)] private float _horizontalDeadZoneTreshold = 0.1f;
        #endregion

        #region Properties
        public LayerMask PlayerLayer => _playerLayer;
        public bool SnapInput => _snapInput;
        public float VerticalDeadZoneTreshold => _verticalDeadZoneTreshold;
        public float HorizontalDeadZoneTreshold => _horizontalDeadZoneTreshold;
        #endregion
    }
}