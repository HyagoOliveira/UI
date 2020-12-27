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
    public sealed class DelayedButton : Button
    {
        [Min(0F), Tooltip("Time (in seconds) to trigger the button On Click action.")]
        public float delay = 0.2F;

        private bool isSubmiting;

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (isSubmiting) return;
            StartCoroutine(SubmitCoroutine(eventData));
        }

        public override void OnSubmit(BaseEventData eventData)
        {
            if (isSubmiting) return;
            StartCoroutine(SubmitCoroutine(eventData));
        }

        private System.Collections.IEnumerator SubmitCoroutine(BaseEventData eventData)
        {
            isSubmiting = true;
            yield return new WaitForSeconds(delay);
            base.OnSubmit(eventData);
            isSubmiting = false;
        }
    }
}