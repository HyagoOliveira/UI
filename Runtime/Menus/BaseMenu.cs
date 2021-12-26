using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ActionCode.UI
{
    /// <summary>
    /// Base component for menus.
    /// <para>It requires a <see cref="AudioSource"/> and <see cref="GraphicRaycaster"/> component.</para>
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class BaseMenu : CanvasViewer
    {
        [SerializeField, Tooltip("The local GraphicRaycaster component.")]
        protected GraphicRaycaster raycaster;
        [SerializeField, Tooltip("The GameObject that is selected every time this menu is enabled.")]
        protected GameObject firstSelected;

        [Header("Audio")]
        [Tooltip("Audio clip played when any button is submitted.")]
        public AudioClip submit;
        [Tooltip("Audio clip played when any button is selected.")]
        public AudioClip selection;
        [SerializeField, Tooltip("The local Audio Source component.")]
        protected AudioSource audioSource;

        [Space]
        [SerializeField, Tooltip("All local Menu Event Triggers associated to this menu.")]
        protected MenuEventTrigger[] eventTriggers;

        /// <summary>
        /// The current Event System.
        /// </summary>
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
            eventTriggers = GetComponentsInChildren<MenuEventTrigger>(includeInactive: true);
        }

        protected virtual void Awake()
        {
            FindCurrentEventSystem();
            SetupEventTriggers();
        }

        public override void Show()
        {
            base.Show();
            if (firstSelected)
            {
                audioSource.enabled = false;
                CurrentEventSystem.SetSelectedGameObject(firstSelected);
                audioSource.enabled = true;
            }
        }

        /// <summary>
        /// Finds the <see cref="CurrentEventSystem"/>.
        /// </summary>
        public void FindCurrentEventSystem() => CurrentEventSystem = EventSystem.current;

        /// <summary>
        /// Plays the given audio clip.
        /// </summary>
        /// <param name="clip">The clip being played.</param>
        public void PlayAudio(AudioClip clip)
        {
            if (audioSource.enabled) audioSource.PlayOneShot(clip);
        }

        /// <summary>
        /// Plays the <see cref="submit"/> button audio clip.
        /// </summary>
        public void PlaySubmitAudio() => PlayAudio(submit);

        /// <summary>
        /// Plays the <see cref="selection"/> button audio clip.
        /// </summary>
        public void PlaySelectionAudio() => PlayAudio(selection);

        /// <summary>
        /// Selects the first menu trigger.
        /// </summary>
        public void SelectFirstTrigger() => SelectTrigger(0);

        /// <summary>
        /// Selects a menu trigger referenced by the given index.
        /// <para>It checks if the trigger exists first.</para>
        /// </summary>
        /// <param name="index">The trigger index.</param>
        public void SelectTrigger(int index)
        {
            var canSelect = HasTrigger(index);
            if (canSelect) SelectGameObject(eventTriggers[index].gameObject);
        }

        /// <summary>
        /// Checks if the trigger referenced by the given index is selected.
        /// </summary>
        /// <param name="index">The trigger index.</param>
        /// <returns>True if the trigger is selected. False otherwise.</returns>
        public bool IsTriggerSelected(int index)
        {
            var hasTrigger = HasTrigger(index);
            var hasSelectedGO = TryGetCurrentSelectedGameObject(out GameObject currentSelectedGameObject);
            return hasTrigger && hasSelectedGO &&
                currentSelectedGameObject.GetInstanceID() ==
                eventTriggers[index].gameObject.GetInstanceID();
        }

        /// <summary>
        /// Tries to get the current selected GameObject from <see cref="CurrentEventSystem"/>.
        /// </summary>
        /// <param name="currentSelectedGameObject">The GameObject currently considered active by the EventSystem.</param>
        /// <returns>True if a GameObject is currently active by the EventSystem. False otherwise.</returns>
        public bool TryGetCurrentSelectedGameObject(out GameObject currentSelectedGameObject)
        {
            currentSelectedGameObject = CurrentEventSystem.currentSelectedGameObject;
            return currentSelectedGameObject != null;
        }

        /// <summary>
        /// Checks if the trigger referenced by the given index exists.
        /// </summary>
        /// <param name="index">The trigger index.</param>
        /// <returns>True if the trigger exists. False otherwise.</returns>
        public bool HasTrigger(int index) => index < eventTriggers.Length && eventTriggers[index];

        /// <summary>
        /// Selects the given GameObject and deselects the previous one.
        /// </summary>
        /// <param name="gameObject">GameObject to select.</param>
        public void SelectGameObject(GameObject gameObject)
        {
            var canSelect = HasCurrentEventSystem() && !CurrentEventSystem.alreadySelecting;
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

        /// <summary>
        /// Whether the <see cref="CurrentEventSystem"/> is valid.
        /// </summary>
        /// <returns>True if CurrentEventSystem was assigned. False otherwise.</returns>
        public bool HasCurrentEventSystem() => CurrentEventSystem != null;

        private void SetupEventTriggers()
        {
            for (int i = 0; i < eventTriggers.Length; i++)
            {
                eventTriggers[i].menu = this;
            }
        }
    }
}