using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// Popup component for UI panels.
    /// </summary>
    [RequireComponent(typeof(GraphicRaycaster))]
    public sealed class Popup : CanvasViewer
    {
        [SerializeField, Tooltip("The button used to close this Popup.")]
        private Button closeButton;

        protected override void Reset()
        {
            base.Reset();
            closeButton = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {
            if (closeButton) closeButton.onClick.AddListener(HandleCloseButtonClick);
        }

        private void OnDisable()
        {
            if (closeButton) closeButton.onClick.RemoveListener(HandleCloseButtonClick);
        }

        /// <summary>
        /// Opens this popup.
        /// </summary>
        public void Open() => Show();

        /// <summary>
        /// Opens this popup using the given delay.
        /// </summary>
        /// <param name="delay">The time (in seconds) to open.</param>
        public void Open(float delay) => Invoke(nameof(Open), delay);

        /// <summary>
        /// Closes this popup.
        /// </summary>
        public void Close() => Hide();

        /// <summary>
        /// Closes this popup using the given delay.
        /// </summary>
        /// <param name="delay">The time (in seconds) to close.</param>
        public void Close(float delay) => Invoke(nameof(Close), delay);

        private void HandleCloseButtonClick() => Close();
    }
}