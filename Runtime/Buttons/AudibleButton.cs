using UnityEngine;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Adds Submit and Selection audios for a button.
    /// </summary>
    [AddComponentMenu("UI/Audible Button")]
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudibleButton : MonoBehaviour,
        ISubmitHandler, ISelectHandler, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField, Tooltip("The local Audio Source component.")]
        private AudioSource audioSource;

        [Header("Audio Clips")]
        [Tooltip("Audio clip played when the button is submitted.")]
        public AudioClip submit;
        [Tooltip("Audio clip played when the button is selected.")]
        public AudioClip selection;

        private void Reset()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays the <see cref="submit"/> audio clip.
        /// </summary>
        public void PlaySubmitClip()
        {
            audioSource.PlayOneShot(submit);
        }

        /// <summary>
        /// Plays the <see cref="selection"/> audio clip.
        /// </summary>
        public void PlaySelectionClip()
        {
            audioSource.PlayOneShot(selection);
        }

        public void OnSubmit(BaseEventData eventData) => PlaySubmitClip();

        public void OnSelect(BaseEventData eventData) => PlaySelectionClip();

        public void OnPointerEnter(PointerEventData eventData) => PlaySelectionClip();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            PlaySubmitClip();
        }
    }
}