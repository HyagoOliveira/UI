using System;
using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// A popup with a Confirmation button.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ConfirmationPopup : Popup
    {
        [SerializeField, Tooltip("The button used to confirm this Popup.")]
        private Button confirmButton;
        [SerializeField, Tooltip("If enable, it will close the Popup when the Confirm Button is submitted.")]
        private bool closeOnConfirm = true;

        public event Action OnConfirmed;

        protected override void Reset()
        {
            base.Reset();
            var buttons = GetComponentsInChildren<Button>();
            if (buttons.Length > 1)
            {
                confirmButton = buttons[0];
                closeButton = buttons[1];
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            confirmButton.onClick.AddListener(HandleConfirmButtonClick);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            confirmButton.onClick.RemoveListener(HandleConfirmButtonClick);
        }

        private void HandleConfirmButtonClick()
        {
            OnConfirmed?.Invoke();
            if (closeOnConfirm) Close();
        }
    }
}