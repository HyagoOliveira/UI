using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Toggle;

/// <summary>
/// Button Toggle -- swaps sprites based on its state
/// </summary>
[AddComponentMenu("UI/ButtonToggle", 36)]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public sealed class ButtonToggle : Selectable, IPointerClickHandler, ISubmitHandler
{
    [Tooltip("Graphic Image the toggle should be working with.")]
    public Image graphic;
    [Tooltip("Sprite to swap Graphic Image when toggle is on.")]
    public Sprite onSprite;
    [Tooltip("Sprite to swap Graphic Image when toggle is off.")]
    public Sprite offSprite;
    public bool isOn = true;

    /// <summary>Callback fired when button is pressed.</summary>
    [Header("Events")]
    public ToggleEvent onValueChanged = new ToggleEvent();

    protected override void Reset()
    {
        graphic = GetComponent<Image>();
        onSprite = graphic.sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        InternalToggle();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        InternalToggle();
    }

    private void InternalToggle()
    {
        if (!IsActive() || !IsInteractable()) return;

        isOn = !isOn;
        if (isOn)
        {
            if (onSprite) graphic.sprite = onSprite;
            else graphic.color -= Color.white * .5f;
        }
        else
        {
            if (offSprite) graphic.sprite = offSprite;
            else graphic.color = Color.white;
        }


        if (onSprite && offSprite) graphic.sprite = isOn ? onSprite : offSprite;
        onValueChanged.Invoke(isOn);
    }
}