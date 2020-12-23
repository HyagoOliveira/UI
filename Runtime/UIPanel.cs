using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace ActionCode.UI
{
    /// <summary>
    /// This component is used to display a UI Panel containing an audio source, a title, a message and two buttons: Yes and No.
    /// <para>On Awake, the Hide action will be binded to the No button action if none was.</para>
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UIPanel : CanvasDrawer
    {
        public AudioSource audioSource;
        public GraphicRaycaster raycaster;
        public Text titleText;
        public Text messageText;
        public Button noButton;
        public Button yesButton;

        public UnityEvent onShow;
        public UnityEvent onHide;

        public string Title
        {
            get { return titleText.text; }
            set { titleText.text = value; }
        }
        public string Message
        {
            get { return messageText.text; }
            set { messageText.text = value; }
        }

        protected override void Reset()
        {
            base.Reset();
            audioSource = GetComponent<AudioSource>();
            raycaster = GetComponent<GraphicRaycaster>();
        }
        protected override void Awake()
        {
            base.Awake();
            if (noButton && !IsActionBindedToNoButton())
                SetNoClickAction(Hide);
        }

        /// <summary>
        /// Removes all listeners and adds the given action to NoButton onClick event.
        /// </summary>
        /// <param name="action"></param>
        public void SetNoClickAction(UnityAction action)
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(action);
        }

        /// <summary>
        /// Removes all listeners and adds the given action to YesButton onClick event.
        /// </summary>
        /// <param name="action"></param>
        public void SetYesClickAction(UnityAction action)
        {
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(action);
        }

        /// <summary>
        /// Shows the warning panel with the given message and title.
        /// </summary>
        /// <param name="message">Message to prompt</param>
        /// <param name="title">Warning by default</param>
        public void Show(string message, string title = "Warning")
        {
            Message = message;
            Title = title;
            Show();
        }

        /// <summary>
        /// Shows the warning panel with the given message and yes action.
        /// </summary>
        /// <param name="message">Message to prompt</param>
        /// <param name="yesAction">Action executed when Yes button is trigged</param>
        /// <param name="title">Warning by default</param>
        public void Show(string message, UnityAction yesAction, string title = "Warning")
        {
            SetYesClickAction(yesAction);
            Show(message, title);
        }

        /// <summary>
        /// Shows the warning panel with the given message, yes action and no action.
        /// </summary>
        /// <param name="message">Message to prompt</param>
        /// <param name="yesAction">Action executed when Yes button is trigged</param>
        /// <param name="noAction">Action executed when No button is trigged</param>
        /// <param name="title">Warning by default</param>
        public void Show(string message, UnityAction yesAction, UnityAction noAction, string title = "Warning")
        {
            SetNoClickAction(noAction);
            Show(message, yesAction, title);
        }

        public override void Show()
        {
            if (audioSource.clip) audioSource.Play();
            raycaster.enabled = true;
            onShow.Invoke();
            base.Show();
        }

        public override void Hide()
        {
            raycaster.enabled = false;
            onHide.Invoke();
            base.Hide();
        }

        /// <summary>
        /// Returns true if an action is binded to Yes button.
        /// </summary>
        /// <returns>True if an action is binded to Yes button.</returns>
        public bool IsActionBindedToYesButton()
        {
            return yesButton.onClick.GetPersistentEventCount() > 0;
        }

        /// <summary>
        /// Returns true if an action is binded to No button.
        /// </summary>
        /// <returns>True if an action is binded to No button.</returns>
        public bool IsActionBindedToNoButton()
        {
            return noButton.onClick.GetPersistentEventCount() > 0;
        }
    }
}