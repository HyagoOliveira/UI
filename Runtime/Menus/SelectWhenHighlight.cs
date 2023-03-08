using UnityEngine;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Selects an implementation of <see cref="IHighlightable"/> 
    /// (like <see cref="DelayedButton"/>) when any Pointer (like the mouse) highlights it.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SelectWhenHighlight : MonoBehaviour
    {
        private IHighlightable[] highlightables;

        private void Awake() => FindHighlightables();
        private void OnEnable() => BindHighlightables();
        private void OnDisable() => UnBindHighlightables();

        private void FindHighlightables() =>
            highlightables = GetComponentsInChildren<IHighlightable>(includeInactive: true);

        private void BindHighlightables()
        {
            foreach (var highlightable in highlightables)
            {
                highlightable.OnHighlighted += HandleHighlighted;
            }
        }

        private void UnBindHighlightables()
        {
            foreach (var highlightable in highlightables)
            {
                highlightable.OnHighlighted -= HandleHighlighted;
            }
        }

        private void HandleHighlighted(GameObject gameObject)
        {
            var eventSystem = EventSystem.current;
            if (eventSystem.alreadySelecting) return;

            eventSystem.SetSelectedGameObject(gameObject);
        }
    }
}