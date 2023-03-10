using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Base component for menus.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class AbstractMenu : CanvasViewer
    {
        [SerializeField, Tooltip("The local GraphicRaycaster component.")]
        protected GraphicRaycaster raycaster;
        [SerializeField, Tooltip("This GameObject will be select by default when the Menu is Shown.")]
        protected GameObject firstGameObject;
        [SerializeField, Tooltip("If enabled, the EventSystem will be disabled when this menu is Hidden.")]
        private bool disableEventSystemOnHide = true;

        private EventSystem eventSystem;

        /// <summary>
        /// Whether the Menu is visible.
        /// </summary>
        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                raycaster.enabled = value;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            raycaster = GetComponent<GraphicRaycaster>();
            TryFindFirstGameObject();
        }

        protected virtual void Awake()
        {
            eventSystem = EventSystem.current;
            if (Visible) Show();
        }

        protected virtual void OnEnable() => BindButtonsEvents();
        protected virtual void OnDisable() => UnBindButtonsEvents();

        public override void Show()
        {
            base.Show();
            if (eventSystem) eventSystem.enabled = true;
            TrySelectFirstGameObject();
        }

        public override void Hide()
        {
            base.Hide();
            CheckDisableEventSystem();
        }

        protected abstract void BindButtonsEvents();
        protected abstract void UnBindButtonsEvents();

        private void TryFindFirstGameObject()
        {
            var firstSelectable = GetComponentInChildren<Selectable>();
            if (firstSelectable) firstGameObject = firstSelectable.gameObject;
        }

        private void TrySelectFirstGameObject()
        {
            if (firstGameObject == null || eventSystem == null) return;

            var isAlreadySelected = eventSystem.currentSelectedGameObject == firstGameObject;
            if (isAlreadySelected) return;

            // Disable a possible SelectableAudioMenu component to don't
            // play any sound when the menu selects its first GameObject.
            var selectableAudio = GetComponent<SelectableAudioMenu>();
            if (selectableAudio) selectableAudio.enabled = false;

            eventSystem.SetSelectedGameObject(firstGameObject);

            if (selectableAudio) selectableAudio.enabled = true;
        }

        [ContextMenu("Create a Traditional Menu")]
        private void CreateTraditionalMenu()
        {
            gameObject.AddComponent<HighlightableMenu>();
            gameObject.AddComponent<SelectableAudioMenu>();
            gameObject.AddComponent<SubmitableAudioMenu>();
        }

        private void CheckDisableEventSystem()
        {
            if (disableEventSystemOnHide && eventSystem)
                eventSystem.enabled = false;
        }
    }
}