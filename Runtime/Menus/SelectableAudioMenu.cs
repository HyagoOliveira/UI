using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Plays the <see cref="MenuData.Selection"/> clip when any implementation of 
    /// <see cref="ISelectable"/> (like <see cref="DelayedButton"/>) is selected.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class SelectableAudioMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("The local AudioSource component.")]
        private AudioSource source;
        [SerializeField, Tooltip("The Data for this menu.")]
        private MenuData data;

        private ISelectable[] selectables;

        private void Reset() => source = GetComponent<AudioSource>();
        private void Awake() => FindSelectables();
        private void OnEnable() => BindSelectables();
        private void OnDisable() => UnBindSelectables();

        private void FindSelectables() =>
            selectables = GetComponentsInChildren<ISelectable>(includeInactive: true);

        private void BindSelectables()
        {
            foreach (var selectable in selectables)
            {
                selectable.OnSelected += HandleSelected;
            }
        }

        private void UnBindSelectables()
        {
            foreach (var selectable in selectables)
            {
                selectable.OnSelected -= HandleSelected;
            }
        }

        private void HandleSelected() => source.PlayOneShot(data.Selection);
    }
}