using UnityEngine;
using UnityEngine.UI;

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
        }

        protected virtual void Start()
        {
            if (Visible) Show();
        }

        protected virtual void OnEnable() => BindButtonsEvents();
        protected virtual void OnDisable() => UnBindButtonsEvents();

        protected abstract void BindButtonsEvents();
        protected abstract void UnBindButtonsEvents();

        [ContextMenu("Create a Traditional Menu")]
        private void CreateTraditionalMenu()
        {
            gameObject.AddComponent<HighlightableMenu>();
            gameObject.AddComponent<SelectableAudioMenu>();
            gameObject.AddComponent<SubmitableAudioMenu>();
            gameObject.AddComponent<FirstGameObjectSelector>();
        }
    }
}