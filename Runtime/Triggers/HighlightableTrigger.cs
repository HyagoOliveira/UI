using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Basic implementation of <see cref="IHighlightable"/>.
    /// <para>
    /// Add this component in any UI GameObject to get <see cref="OnHighlighted"/>
    /// notification when a Pointer enters it.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HighlightableTrigger : MonoBehaviour, IPointerEnterHandler, IHighlightable
    {
        public event Action<GameObject> OnHighlighted;

        public void OnPointerEnter(PointerEventData _) => OnHighlighted?.Invoke(gameObject);
    }
}