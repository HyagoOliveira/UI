using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// Group Selectable components.
    /// </summary>
    public sealed class SelectableGroup : MonoBehaviour
    {
        [Tooltip("Selectable components.")]
        public Selectable[] selectables;

        private void Reset()
        {
            selectables = GetComponentsInChildren<Selectable>(includeInactive: true);
        }

        /// <summary>
        /// Set all <see cref="selectables"/> interaction using the given param.
        /// </summary>
        /// <param name="interactable"></param>
        public void SetInteraction(bool interactable)
        {
            foreach (var selectable in selectables)
            {
                selectable.interactable = interactable;
            }
        }
    }
}