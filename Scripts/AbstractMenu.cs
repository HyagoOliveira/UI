using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Abstract class for UI Menus.
/// </summary>
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(GraphicRaycaster))]
public abstract class AbstractMenu : CanvasDrawer
{
    public GraphicRaycaster raycaster;
    [Tooltip("Selectables components as buttons and toggles.")]
    public Selectable[] selectables;

    [Header("Audio")]
    public AudioSource audioSource;
    [Tooltip("Sound played when a selectable component is highlighted.")]
    public AudioClip onHighlightedSound;
    [Tooltip("Sound played when a selectable component is clicked.")]
    public AudioClip onClickSound;

    protected override void Reset()
    {
        base.Reset();
        raycaster = GetComponent<GraphicRaycaster>();
        audioSource = GetComponent<AudioSource>();
        selectables = GetComponentsInChildren<Selectable>();
    }
    protected override void Awake()
    {
        BindSoundsToSelectables();
        base.Awake();
    }

    public override void Show()
    {
        raycaster.enabled = true;
        base.Show();
    }

    public override void Hide()
    {
        raycaster.enabled = false;
        base.Hide();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) OnBackButtonPressed();
    }
    protected virtual void OnBackButtonPressed()
    {
        if (Hidden) Show();
        else Hide();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void PlayHighlightedSound()
    {
        PlaySound(onHighlightedSound);
    }
    public void PlayClickSound()
    {
        PlaySound(onClickSound);
    }

    private void BindSoundsToSelectables()
    {
        // binds onHighlightedSound and onClickSound audio clips to all selectable components
        for (int i = 0; i < selectables.Length; i++)
        {
            if (onHighlightedSound)
            {
                // creates or get the EventTrigger component and set its PointerEnter event type to PlayHighlightedSound
                EventTrigger eventTrigger = 
                    selectables[i].gameObject.GetComponent<EventTrigger>() ??
                    selectables[i].gameObject.AddComponent<EventTrigger>();

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((eventData) => { PlayHighlightedSound(); });
                eventTrigger.triggers.Add(entry);
            }

            if (onClickSound)
            {
                Button button = selectables[i] as Button;
                if (button) button.onClick.AddListener(PlayClickSound);
                else
                {
                    Toggle toggle = selectables[i] as Toggle;
                    if (toggle) toggle.onValueChanged.AddListener((selected) => { PlayClickSound(); });
                }
            }
        }
    }
}