using UnityEngine;

namespace ActionCode.UI
{
    /// <summary>
    /// UI class for toggling visibility of a <see cref="Canvas"/> component. It requires a nested 
    /// <see cref="Canvas"/> component (a Canvas placed as a child of another) for optimization.
    /// <para>
    /// Nested Canvases isolate their children from their parent so a dirty child will not force a 
    /// parent Canvas to rebuild its geometry. Therefore, some batches are saved and performance is increased.
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class CanvasViewer : MonoBehaviour
    {
        [SerializeField, Tooltip("The canvas component")]
        private Canvas canvas;

        /// <summary>
        /// Whether the canvas is visible.
        /// </summary>
        public virtual bool Visible
        {
            get => canvas.enabled;
            set
            {
                enabled = value;
                canvas.enabled = value;
            }
        }

        protected virtual void Reset() => canvas = GetComponent<Canvas>();

        /// <summary>
        /// Shows this GameObject by enabling its Canvas component.
        /// </summary>
        [ContextMenu("Show")]
        public virtual void Show() => Visible = true;

        /// <summary>
        /// Hides this GameObject by disabling its Canvas component.
        /// </summary>
        [ContextMenu("Hide")]
        public virtual void Hide() => Visible = false;

        /// <summary>
        /// Toggles this GameObject visibility.
        /// </summary>
        public void Toggle()
        {
            if (Visible) Hide();
            else Show();
        }
    }
}