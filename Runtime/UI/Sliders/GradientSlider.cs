using UnityEngine;
using UnityEngine.UI;

namespace ActionCode.UI
{
    /// <summary>
    /// Uses the <see cref="gradient"/> to update the fill image color according with the current Slider value.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class GradientSlider : Slider
    {
        public Gradient gradient;

        private Graphic fill;

#if UNITY_EDITOR
        protected override void Reset()
        {
            base.Reset();
            fill = null;
        }
#endif

        protected override void Set(float input, bool sendCallback = true)
        {
            base.Set(input, sendCallback);

            if (fill == null && fillRect != null)
                fill = fillRect.GetComponent<Graphic>();

            if (fill) fill.color = gradient.Evaluate(m_Value);
        }
    }
}