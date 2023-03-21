using UnityEditor;
using UnityEditor.UI;

namespace ActionCode.UI.Editor
{
    /// <summary>
    /// Custom Editor for the <see cref="GradientSlider"/> Component.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(GradientSlider))]
    public sealed class GradientSliderEditor : SliderEditor
    {
        private SerializedProperty gradientProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            gradientProperty = serializedObject.FindProperty(nameof(GradientSlider.gradient));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(gradientProperty);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}