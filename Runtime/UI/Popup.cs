using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Popup component for UI panels.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public sealed class Popup : CanvasViewer
    {
        [SerializeField, Tooltip("AudioSource to play when the Popup is opened.")]
        private AudioSource audioSource;
        [SerializeField, Tooltip("List of Selectable Groups to be disabled when the popup is opened and enabled when it's closed.")]
        private SelectableGroup[] interactionGroups;

        private Selectable[] selectables;
        private GameObject previousSelectedGameObject;

        protected override void Reset()
        {
            base.Reset();
            audioSource = GetComponent<AudioSource>();
        }

        private void Awake()
        {
            FindSelectables();
            if (!Visible) SetSelectableInteraction(interactable: false);
        }

        /// <summary>
        /// Opens this popup.
        /// </summary>
        public void Open()
        {
            Show();
            audioSource.Play();
            SetSelectableInteraction(interactable: true);
            SelectFirstSelectableGameObject();
            SetGroupsInteraction(interactable: false);
        }

        /// <summary>
        /// Opens this popup using the given delay.
        /// </summary>
        /// <param name="delay">The time (in seconds) to open.</param>
        public void Open(float delay) => Invoke(nameof(Open), delay);

        /// <summary>
        /// Closes this popup.
        /// </summary>
        public void Close()
        {
            Hide();
            SetSelectableInteraction(interactable: false);
            SetGroupsInteraction(interactable: true);
            SelectPreviousSelectableGameObject();
        }

        /// <summary>
        /// Closes this popup using the given delay.
        /// </summary>
        /// <param name="delay">The time (in seconds) to close.</param>
        public void Close(float delay) => Invoke(nameof(Close), delay);

        private void SelectFirstSelectableGameObject()
        {
            var currentEventSystem = EventSystem.current;
            var firstSelectable = selectables.Length > 0 ? selectables[0] : null;
            if (currentEventSystem == null || firstSelectable == null) return;

            previousSelectedGameObject = currentEventSystem.currentSelectedGameObject;
            currentEventSystem.SetSelectedGameObject(firstSelectable.gameObject);
        }

        private void SelectPreviousSelectableGameObject()
        {
            var currentEventSystem = EventSystem.current;
            if (currentEventSystem == null) return;

            currentEventSystem.SetSelectedGameObject(previousSelectedGameObject);
        }

        private void SetSelectableInteraction(bool interactable)
        {
            foreach (var selectable in selectables)
            {
                selectable.interactable = interactable;
            }
        }

        private void SetGroupsInteraction(bool interactable)
        {
            foreach (var group in interactionGroups)
            {
                group.SetInteraction(interactable);
            }
        }

        private void FindSelectables() =>
            selectables = GetComponentsInChildren<Selectable>(includeInactive: true);
    }
}