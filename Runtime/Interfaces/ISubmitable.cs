using System;

namespace ActionCode.UI
{
    /// <summary>
    /// Interface used on objects able to be submitted. 
    /// </summary>
    public interface ISubmitable
    {
        /// <summary>
        /// Event fired when this object is submitted.
        /// </summary>
        event Action OnSubmitted;
    }
}