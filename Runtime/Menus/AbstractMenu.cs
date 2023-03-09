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

        protected virtual void Start()
        {
            if (Visible) TrySelectFirstGameObject();
        }

        protected virtual void OnEnable() => BindButtonsEvents();
        protected virtual void OnDisable() => UnBindButtonsEvents();

        public override void Show()
        {
            base.Show();
            TrySelectFirstGameObject();
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
            if (firstGameObject == null) return;

            var eventSystem = EventSystem.current;
            if (eventSystem == null) return;

            eventSystem.SetSelectedGameObject(firstGameObject);
        }
    }
}