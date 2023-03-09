using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Selectable Slider component.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SelectableSlider : Slider, ISelectable, IHighlightable
    {
        public event Action OnSelected;
        public event Action<GameObject> OnHighlighted;

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            OnSelected?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            OnHighlighted?.Invoke(gameObject);
        }
    }
}