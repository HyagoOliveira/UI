using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Base component for menus.
    /// <para>It requires a <see cref="AudioSource"/> and <see cref="GraphicRaycaster"/> component.</para>
    /// </summary>
    //[DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class BaseMenu : CanvasViewer
    {
        [SerializeField, Tooltip("The local Audio Source component.")]
        private AudioSource audioSource;
        [SerializeField, Tooltip("The local GraphicRaycaster component.")]
        private GraphicRaycaster raycaster;

        [Header("Buttons")]
        [Tooltip("Audio clip played when any button is submitted.")]
        public AudioClip submit;
        [Tooltip("Audio clip played when any button is selected.")]
        public AudioClip selection;
        [SerializeField, Tooltip("All buttons from this menu.")]
        protected Button[] buttons;

        /// <summary>
        /// Whether the local GraphicRaycaster is enabled.
        /// </summary>
        public bool EnableRaycaster
        {
            get => raycaster.enabled;
            set => raycaster.enabled = value;
        }

        protected override void Reset()
        {
            base.Reset();
            audioSource = GetComponent<AudioSource>();
            raycaster = GetComponent<GraphicRaycaster>();
            buttons = GetComponentsInChildren<Button>(includeInactive: true);
        }

        protected virtual void OnEnable() => SetupEventTriggers();

        /// <summary>
        /// Plays the given audio clip.
        /// </summary>
        /// <param name="clip">The clip being played.</param>
        public void PlayAudio(AudioClip clip) => audioSource.PlayOneShot(clip);

        /// <summary>
        /// Plays the <see cref="submit"/> button audio clip.
        /// </summary>
        public void PlaySubmitAudio() => PlayAudio(submit);

        /// <summary>
        /// Plays the <see cref="selection"/> button audio clip.
        /// </summary>
        public void PlaySelectionAudio() => PlayAudio(selection);

        /// <summary>
        /// Shows the menu.
        /// </summary>
        public override void Show()
        {
            base.Show();
            EnableRaycaster = true;
        }

        /// <summary>
        /// Hides the menu.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
            EnableRaycaster = false;
        }

        /// <summary>
        /// Selects the first button.
        /// </summary>
        public void SelectFirstButton()
        {
            var canSelect = buttons.Length > 0 && buttons[0];
            if (canSelect) SelectGameObject(buttons[0].gameObject);
        }

        /// <summary>
        /// Selects the given GameObject and deselects the previous one.
        /// </summary>
        /// <param name="gameObject">GameObject to select.</param>
        public static void SelectGameObject(GameObject gameObject)
        {
            var eventSystem = EventSystem.current;
            var canSelect = eventSystem && !eventSystem.alreadySelecting;
            if (canSelect) eventSystem.SetSelectedGameObject(gameObject);
        }

        internal virtual void SubmitEvent(MenuEventTrigger eventTrigger) => PlaySubmitAudio();

        internal virtual void SelectEvent(MenuEventTrigger eventTrigger)
        {
            SelectGameObject(eventTrigger.gameObject);
            PlaySelectionAudio();
        }

        private void SetupEventTriggers()
        {
            foreach (var button in buttons)
            {
                var trigger = button.GetComponent<MenuEventTrigger>() ??
                    button.gameObject.AddComponent<MenuEventTrigger>();
                trigger.menu = this;
            }
        }
    }
}