using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Plays the <see cref="submition"/> audio when any menu UI is submitted.
    /// <para>
    /// The menu UI must implement <see cref="ISubmitable"/> interface,
    /// as <see cref="DelayedButton"/> does.
    /// </para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class SubmitableAudioMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("The local AudioSource component.")]
        private AudioSource source;
        [SerializeField, Tooltip("The audio played when submitted.")]
        private AudioClip submition;

        private ISubmitable[] submitables;

        private void Reset() => source = GetComponent<AudioSource>();
        private void Awake() => FindSubmitables();
        private void OnEnable() => BindSubmitables();
        private void OnDisable() => UnBindSubmitables();

        private void FindSubmitables() =>
            submitables = GetComponentsInChildren<ISubmitable>(includeInactive: true);

        private void BindSubmitables()
        {
            foreach (var submitable in submitables)
            {
                submitable.OnSubmitted += HandleSubmitted;
            }
        }

        private void UnBindSubmitables()
        {
            foreach (var submitable in submitables)
            {
                submitable.OnSubmitted -= HandleSubmitted;
            }
        }

        private void HandleSubmitted() => source.PlayOneShot(submition);
    }
}