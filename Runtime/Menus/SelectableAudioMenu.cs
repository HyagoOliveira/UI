using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Plays the <see cref="selection"/> audio when any menu UI is selected.
    /// <para>
    /// The menu UI must implement <see cref="ISelectable"/> interface,
    /// as <see cref="DelayedButton"/> does.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class SelectableAudioMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("The local AudioSource component.")]
        private AudioSource source;
        [SerializeField, Tooltip("The audio played when selected.")]
        private AudioClip selection;

        private ISelectable[] selectables;

        private void Reset() => source = GetComponent<AudioSource>();
        private void Awake() => FindSelectables();
        private void OnEnable() => BindSelectablesAfterDelay();
        private void OnDisable() => UnBindSelectables();

        private void FindSelectables() =>
            selectables = GetComponentsInChildren<ISelectable>(includeInactive: true);

        // The binding has a small delay to prevent the selection sound to be played soon after the game starts.
        private void BindSelectablesAfterDelay() => Invoke(nameof(BindSelectables), 0.01F);

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

        private void HandleSelected() => source.PlayOneShot(selection);
    }
}