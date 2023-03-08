using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace ActionCode.UI
{
    /// <summary>
    /// Delayed button component.
    /// <para>It will trigger the button On Click action after the <see cref="delay"/>.</para>
    /// </summary>
    [AddComponentMenu("UI/Delayed Button")]
    public sealed class DelayedButton : Button
    {
        [SerializeField, Tooltip("Time (in seconds) to trigger the button On Click action."), Min(minDelay)]
        private float delay = 0.2F;

        private const float minDelay = 0F;

        /// <summary>
        /// Time (in seconds) to trigger the button On Click action.
        /// </summary>
        public float Delay
        {
            get => delay;
            set => delay = Mathf.Max(minDelay, value);
        }

        private bool isSubmiting;

        public override void OnSubmit(BaseEventData eventData) => TrySubmit(eventData);

        public override void OnPointerClick(PointerEventData eventData) => TrySubmit(eventData);

        private void TrySubmit(BaseEventData eventData)
        {
            if (isSubmiting) return;
            StartCoroutine(SubmitCoroutine(eventData));
        }

        private IEnumerator SubmitCoroutine(BaseEventData eventData)
        {
            isSubmiting = true;
            yield return new WaitForSeconds(delay);
            base.OnSubmit(eventData);
            isSubmiting = false;
        }
    }
}