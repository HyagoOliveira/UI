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