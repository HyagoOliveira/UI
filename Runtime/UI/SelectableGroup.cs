using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// Selectable Group component.
    /// </summary>
    public sealed class SelectableGroup : MonoBehaviour
    {
        [SerializeField, Tooltip("Selectable components.")]
        private Selectable[] selectables;

        private void Reset() =>
            selectables = GetComponentsInChildren<Selectable>(includeInactive: true);

        /// <summary>
        /// Set all <see cref="selectables"/> interaction using the given param.
        /// </summary>
        /// <param name="interactable">Whether is interactable.</param>
        public void SetInteraction(bool interactable)
        {
            foreach (var selectable in selectables)
            {
                selectable.interactable = interactable;
            }
        }
    }
}