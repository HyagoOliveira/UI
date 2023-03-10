using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Delayed button component.
    /// <para>It will trigger the button On Click action after the <see cref="delay"/>.</para>
    /// </summary>
    [AddComponentMenu("UI/Delayed Button")]
    public sealed class DelayedButton : Button, ISelectable, ISubmitable, IHighlightable
    {
        [SerializeField, Tooltip("Time (in seconds) to trigger the button On Click action."), Min(minDelay)]
        private float delay = 0.2F;

        public event Action OnSelected;
        public event Action OnSubmitted;
        public event Action<GameObject> OnHighlighted;

        /// <summary>
        /// Time (in seconds) to trigger the button On Click action.
        /// </summary>
        public float Delay
        {
            get => delay;
            set => delay = Mathf.Max(minDelay, value);
        }

        private bool isSubmiting;
        private const float minDelay = 0F;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            OnSelected?.Invoke();
        }

        public override void OnSubmit(BaseEventData eventData) => TrySubmit(eventData);

        public override void OnPointerClick(PointerEventData eventData)
        {
            var isLeftClick = eventData.button == PointerEventData.InputButton.Left;
            if (isLeftClick) TrySubmit(eventData);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnHighlighted?.Invoke(gameObject);
        }

        private void TrySubmit(BaseEventData eventData)
        {
            if (isSubmiting) return;
            StartCoroutine(SubmitCoroutine(eventData));
        }

        private IEnumerator SubmitCoroutine(BaseEventData eventData)
        {
            isSubmiting = true;
            OnSubmitted?.Invoke();

            yield return new WaitForSecondsRealtime(delay);
            base.OnSubmit(eventData);

            isSubmiting = false;
        }
    }
}