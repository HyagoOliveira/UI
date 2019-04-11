using UnityEngine;

/// <summary>
/// Base class for UI components with a frequently show and hide behaviour. Thus, it requires 
/// a nested <see cref="Canvas"/> (a Canvas placed as a child of another) for optimization purposes and
/// a <see cref="GraphicRaycaster"/> for receive input.
/// <para>Nested Canvases isolate their children from their parent so a dirty child will not force a 
/// parent Canvas to rebuild its geometry. Therefore, some batches are saved and performance increases.</para>
/// </summary>
[RequireComponent(typeof(Canvas))]
public class CanvasDrawer : AbstractUIDrawer
{
    public Canvas canvas;

    protected override void Reset()
    {
        canvas = GetComponent<Canvas>();        
        base.Reset();
    }

    protected virtual void Awake()
    {
        // only enables animator if canvas is enabled
        if (!canvas.enabled) Hide();
    }

    /// <summary>
    /// Shows this GameObject by enabling its Canvas component.
    /// </summary>
    public override void Show()
    {
        Hidden = false;
        canvas.enabled = animator.enabled = enabled = true;
    }

    /// <summary>
    /// Hides this GameObject by disabling its Canvas component.
    /// </summary>
    public override void Hide()
    {
        Hidden = true;
        canvas.enabled = animator.enabled = enabled = false;
    }
}