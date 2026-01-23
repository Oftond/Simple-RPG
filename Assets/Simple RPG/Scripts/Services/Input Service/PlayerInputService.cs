using GameControls;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleRPG.Services.Input
{
    public class PlayerInputService : MonoBehaviour, IInputService
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset _playerControls;

        [Header("Action Name Name")]
        [SerializeField] private string _actionMapName = "Player";

        [Header("Action Name References")]
        [SerializeField] private string _movementName = "Movement";
        [SerializeField] private string _mausePositionName = "Look";
        [SerializeField] private string _attackName = "Attack";
        [SerializeField] private string _sprintName = "Sprint";
        [SerializeField] private string _dashName = "Dash";
        [SerializeField] private string _openInventoryName = "Inventory";
        [SerializeField] private string _openSkillsName = "Skills";
        [SerializeField] private string _interactName = "Interact";
        [SerializeField] private string _openMenuName = "Menu";

        #region Actions
        private InputAction _movementAction;
        private InputAction _mousePositionAction;
        private InputAction _attackAction;
        private InputAction _sprintAction;
        private InputAction _dashAction;
        private InputAction _inventoryAction;
        private InputAction _skillsAction;
        private InputAction _interactAction;
        private InputAction _menuAction;
        #endregion

        #region Events
        public event Action<Vector2> OnMovementAction;
        public event Action<Vector2> OnMausePositionChanged;
        public event Action OnAttackToggle;
        public event Action OnSprintToggle;
        public event Action OnDashToggle;
        public event Action OnInventoryToggle;
        public event Action OnSkillsToggle;
        public event Action OnInteractToggle;
        public event Action OnMenuToggle;
        #endregion

        #region Properties
        public bool IsMove { get; private set; }
        public bool IsSprint { get; private set; }
        public Vector2 CurrentMausePosition { get; private set; }
        #endregion

        private void Awake()
        {
            var controls = _playerControls.FindActionMap(_actionMapName);

            _movementAction = controls.FindAction(_movementName);
            _mousePositionAction = controls.FindAction(_mausePositionName);
            _attackAction = controls.FindAction(_attackName);
            _sprintAction = controls.FindAction(_sprintName);
            _dashAction = controls.FindAction(_dashName);
            _inventoryAction = controls.FindAction(_openInventoryName);
            _skillsAction = controls.FindAction(_openSkillsName);
            _interactAction = controls.FindAction(_interactName);
            _menuAction = controls.FindAction(_openMenuName);

            ActionRegister();
        }

        private void ActionRegister()
        {
            _movementAction.performed += HandleMovementAction;
            _movementAction.canceled += HandleMovementActionEnd;

            _mousePositionAction.performed += HandleMousePositionAction;

            _attackAction.performed += HandleAttackAction;

            _sprintAction.performed += HandleSprintAction;
            _sprintAction.canceled += HandleSprintActionEnd;

            _dashAction.performed += HandleDashAction;

            _inventoryAction.performed += HandleInventoryAction;
            _skillsAction.performed += HandleSkillsAction;
            _interactAction.performed += HandleInteractAction;
            _menuAction.performed += HandleMenuAction;
        }

        private void HandleMenuAction(InputAction.CallbackContext obj)
        {
            OnMenuToggle?.Invoke();
        }

        private void HandleInteractAction(InputAction.CallbackContext obj)
        {
            OnInteractToggle?.Invoke();
        }

        private void HandleSkillsAction(InputAction.CallbackContext obj)
        {
            OnSkillsToggle?.Invoke();
        }

        private void HandleInventoryAction(InputAction.CallbackContext obj)
        {
            OnInventoryToggle?.Invoke();
        }

        private void HandleDashAction(InputAction.CallbackContext obj)
        {
            OnDashToggle?.Invoke();
        }

        private void HandleSprintActionEnd(InputAction.CallbackContext obj)
        {
            IsSprint = false;
        }

        private void HandleSprintAction(InputAction.CallbackContext obj)
        {
            IsSprint = true;
            OnSprintToggle?.Invoke();
        }

        private void HandleAttackAction(InputAction.CallbackContext obj)
        {
            OnAttackToggle?.Invoke();
        }

        private void HandleMousePositionAction(InputAction.CallbackContext obj)
        {
            CurrentMausePosition = obj.ReadValue<Vector2>();
            OnMausePositionChanged?.Invoke(CurrentMausePosition);
        }

        private void HandleMovementActionEnd(InputAction.CallbackContext obj)
        {
            IsMove = false;
            OnMovementAction?.Invoke(Vector2.zero);
        }

        private void HandleMovementAction(InputAction.CallbackContext obj)
        {
            IsMove = true;
            OnMovementAction?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnEnable() => _playerControls.Enable();

        private void OnDisable() => _playerControls.Disable();
    }
}