using UnityEngine;

/// <summary>
/// Abstract class for UI components.
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
public abstract class AbstractUIDrawer : MonoBehaviour
{
    public Animator animator;

    public bool Hidden { get; protected set; }

    protected virtual void Reset()
    {
        animator = GetComponent<Animator>();
        SetupAnimator();
    }        

    /// <summary>
    /// Shows this GameObject.
    /// </summary>
    public virtual void Show()
    {
        Hidden = false;
        gameObject.SetActive(true);
    }
    /// <summary>
    /// Hides this GameObject.
    /// </summary>
    public virtual void Hide()
    {
        Hidden = true;
        gameObject.SetActive(false);
    }

    protected virtual void SetupAnimator()
    {
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}