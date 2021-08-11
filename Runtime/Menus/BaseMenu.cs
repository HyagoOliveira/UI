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
        [SerializeField, Tooltip("The local GraphicRaycaster component.")]
        private GraphicRaycaster raycaster;

        [Header("Audio")]
        [Tooltip("Audio clip played when any button is submitted.")]
        public AudioClip submit;
        [Tooltip("Audio clip played when any button is selected.")]
        public AudioClip selection;
        [SerializeField, Tooltip("The local Audio Source component.")]
        private AudioSource audioSource;

        [Space]
        [SerializeField, Tooltip("All buttons from this menu.")]
        protected Button[] buttons;

        public MenuEventTrigger[] EventTriggers { get; private set; }

        public EventSystem CurrentEventSystem { get; private set; }

        /// <summary>
        /// Whether the Menu is visible.
        /// </summary>
        public override bool Visible
        {
            get => base.Visible;
            set
            {
                base.Visible = value;
                raycaster.enabled = value;
                audioSource.enabled = value;
            }
        }

        protected override void Reset()
        {
            base.Reset();
            audioSource = GetComponent<AudioSource>();
            raycaster = GetComponent<GraphicRaycaster>();
            buttons = GetComponentsInChildren<Button>(includeInactive: true);
        }

        private void Awake()
        {
            CurrentEventSystem = EventSystem.current;
            SetupEventTriggers();
        }

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
        /// Selects the first button.
        /// </summary>
        public void SelectFirstButton() => SelectButton(0);

        /// <summary>
        /// Selects a button referenced by the given index.
        /// <para>It checks if the button exists first.</para>
        /// </summary>
        /// <param name="index">The button index.</param>
        public void SelectButton(int index)
        {
            var canSelect = HasButton(index);
            if (canSelect) SelectGameObject(buttons[index].gameObject);
        }

        public bool IsButtonSelected(int index)
        {
            var hasButton = HasButton(index);
            return
                hasButton &&
                GetCurrentSelectedGameObject()?.GetInstanceID() ==
                buttons[index].gameObject.GetInstanceID();
        }

        public GameObject GetCurrentSelectedGameObject() =>
            CurrentEventSystem.currentSelectedGameObject;

        public bool HasButton(int index) =>
            index < buttons.Length && buttons[index];

        /// <summary>
        /// Selects the given GameObject and deselects the previous one.
        /// </summary>
        /// <param name="gameObject">GameObject to select.</param>
        public void SelectGameObject(GameObject gameObject)
        {
            var canSelect = !CurrentEventSystem.alreadySelecting;
            if (canSelect) CurrentEventSystem.SetSelectedGameObject(gameObject);
        }

        /// <summary>
        /// Submits the given Event Trigger.
        /// </summary>
        /// <param name="eventTrigger">Event Trigger to submit.</param>
        public virtual void SubmitEvent(MenuEventTrigger eventTrigger) => PlaySubmitAudio();

        /// <summary>
        /// Selects the given Event Trigger.
        /// </summary>
        /// <param name="eventTrigger">Event Trigger to select.</param>
        public virtual void SelectEvent(MenuEventTrigger eventTrigger)
        {
            SelectGameObject(eventTrigger.gameObject);
            PlaySelectionAudio();
        }

        private void SetupEventTriggers()
        {
            EventTriggers = new MenuEventTrigger[buttons.Length];
            for (int i = 0; i < buttons.Length; i++)
            {
                var hasTrigger = buttons[i].TryGetComponent(out EventTriggers[i]);
                if (!hasTrigger) EventTriggers[i] = buttons[i].gameObject.AddComponent<MenuEventTrigger>();

                EventTriggers[i].menu = this;
            }
        }
    }
}