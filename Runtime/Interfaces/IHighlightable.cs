using System;
using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Interface used on objects able to be highlighted.
    /// </summary>
    public interface IHighlightable
    {
        /// <summary>
        /// Event fired when this Game Object is highlighted.
        /// </summary>
        event Action<GameObject> OnHighlighted;
    }
}