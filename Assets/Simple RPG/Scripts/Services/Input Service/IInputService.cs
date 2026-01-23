using System;
using UnityEngine;

namespace GameControls
{
    public interface IInputService
    {
        event Action<Vector2> OnMovementAction;
        event Action<Vector2> OnMausePositionChanged;

        event Action OnAttackToggle;
        event Action OnSprintToggle;
        event Action OnDashToggle;
        event Action OnInventoryToggle;
        event Action OnSkillsToggle;
        event Action OnInteractToggle;
        event Action OnMenuToggle;

        Vector2 CurrentMausePosition { get; }
    }
}