using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// Plays the <see cref="MenuData.Submition"/> clip when any implementation of 
    /// <see cref="ISubmitable"/> (like <see cref="DelayedButton"/>) is submitted.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class SubmitableAudioMenu : MonoBehaviour
    {
        [SerializeField, Tooltip("The local AudioSource component.")]
        private AudioSource source;
        [SerializeField, Tooltip("The Data for this menu.")]
        private MenuData data;

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

        private void HandleSubmitted() => source.PlayOneShot(data.Submition);
    }
}