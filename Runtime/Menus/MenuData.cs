using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Data container for menus.
    /// </summary>
    [CreateAssetMenu(fileName = "MenuData", menuName = "ActionCode/UI/Menu Data", order = 110)]
    public sealed class MenuData : ScriptableObject
    {
        /// <summary>
        /// The audio played when selected.
        /// </summary>
        [field: SerializeField, Tooltip("The audio played when selected.")]
        public AudioClip Selection { get; private set; }

        /// <summary>
        /// The audio played when submitted.
        /// </summary>
        [field: SerializeField, Tooltip("The audio played when submitted.")]
        public AudioClip Submition { get; private set; }
    }
}