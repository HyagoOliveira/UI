using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// Plays the local <see cref="AudioSource"/> when the local <see cref="Slider"/> value changes.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Slider))]
    [RequireComponent(typeof(AudioSource))]
    public sealed class AudibleSlider : MonoBehaviour
    {
        [SerializeField, Tooltip("The local Slider component.")]
        private Slider slider;
        [SerializeField, Tooltip("The local AudioSource component.")]
        private AudioSource source;
        [SerializeField, Tooltip("The Data for this menu.")]
        private MenuData data;

        private void Reset()
        {
            slider = GetComponent<Slider>();
            source = GetComponent<AudioSource>();

            source.spatialBlend = 0f;
        }

        private void OnEnable() => slider.onValueChanged.AddListener(HandleValueChanged);
        private void OnDisable() => slider.onValueChanged.RemoveListener(HandleValueChanged);

        private void HandleValueChanged(float value)
        {
            var canPlay = value > 0F && !source.isPlaying;
            if (!canPlay) return;

            if (data) source.PlayOneShot(data.Selection);
            else source.Play();
        }
    }
}