using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Basic implementation of <see cref="ISelectHandler"/>.
    /// <para>
    /// Add this component in any UI GameObject to get <see cref="OnSelected"/>
    /// notification when the Event System selects it.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SelectableTrigger : MonoBehaviour, ISelectHandler, ISelectable
    {
        public event Action OnSelected;

        public void OnSelect(BaseEventData _) => OnSelected?.Invoke();
    }
}