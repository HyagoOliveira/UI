using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Button Toggle.
    /// <para>Swaps sprites based on its state</para>
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(RectTransform))]
    [AddComponentMenu("UI/ButtonToggle", 36)]
    public sealed class ButtonToggle : Selectable, IPointerClickHandler, ISubmitHandler
    {
        [Tooltip("Graphic Image the toggle should be working with.")]
        public Image graphic;
        [Tooltip("Sprite to swap Graphic Image when toggle is on.")]
        public Sprite onSprite;
        [Tooltip("Sprite to swap Graphic Image when toggle is off.")]
        public Sprite offSprite;

        /// <summary>
        /// Callback fired when button is toggled.
        /// </summary>
        [Header("Events")]
        public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

        /// <summary>
        /// Whether the button is toggle on.
        /// </summary>
        public bool IsOn { get; private set; } = true;

        private void Reset()
        {
            graphic = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            Toggle();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            Toggle();
        }

        /// <summary>
        /// Toggles the button.
        /// </summary>
        public void Toggle()
        {
            if (!IsActive() || !IsInteractable()) return;

            IsOn = !IsOn;
            if (IsOn)
            {
                if (onSprite) graphic.sprite = onSprite;
                else graphic.color -= Color.white * .5f;
            }
            else
            {
                if (offSprite) graphic.sprite = offSprite;
                else graphic.color = Color.white;
            }

            onValueChanged.Invoke(IsOn);
        }
    }
}