using UnityEditor;
using UnityEditor.UI;

namespace ActionCode.UI.Editor
{
    /// <summary>
    /// Custom Editor for the <see cref="DelayedButton"/> Component.
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(DelayedButton))]
    public sealed class DelayedButtonEditor : ButtonEditor
    {
        private SerializedProperty delayProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            delayProperty = serializedObject.FindProperty(nameof(DelayedButton.Delay).ToLower());
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(delayProperty);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();
        }
    }
}