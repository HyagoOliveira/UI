using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Tries to select the <see cref="firstGameObject"/> 
    /// when the local <see cref="CanvasViewer"/> is shown.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class FirstGameObjectSelector : MonoBehaviour
    {
        [SerializeField, Tooltip("The local CanvasViewer component.")]
        private CanvasViewer viewer;
        [SerializeField, Tooltip("The GameObject that will be selected.")]
        private GameObject firstGameObject;

        private EventSystem eventSystem;

        private void Reset()
        {
            viewer = GetComponent<CanvasViewer>();
            TryFindFirstGameObject();
        }

        private void Awake() => eventSystem = EventSystem.current;
        private void OnEnable() => viewer.OnShow += HandleViewerShow;
        private void OnDisable() => viewer.OnShow -= HandleViewerShow;

        private void HandleViewerShow() => TrySelectFirstGameObject();

        private void TryFindFirstGameObject()
        {
            var firstSelectable = GetComponentInChildren<Selectable>();
            if (firstSelectable) firstGameObject = firstSelectable.gameObject;
        }

        private void TrySelectFirstGameObject()
        {
            if (eventSystem == null) return;

            var isAlreadySelected = eventSystem.currentSelectedGameObject == firstGameObject;
            if (isAlreadySelected) return;

            SelectFirstGameObject();
        }

        private void SelectFirstGameObject()
        {
            // Disable a possible SelectableAudioMenu component to don't play any sound when
            // selecting the firstGameObject as when the viewer is shown for the first time.
            var selectableAudio = GetComponent<SelectableAudioMenu>();
            if (selectableAudio) selectableAudio.enabled = false;

            eventSystem.SetSelectedGameObject(firstGameObject);

            if (selectableAudio) selectableAudio.enabled = true;
        }
    }
}