using System;

namespace ActionCode.UI
{
    /// <summary>
    /// Interface used on objects able to be selected. 
    /// </summary>
    public interface ISelectable
    {
        /// <summary>
        /// Event fired when this object is selected.
        /// </summary>
        event Action OnSelected;
    }
}