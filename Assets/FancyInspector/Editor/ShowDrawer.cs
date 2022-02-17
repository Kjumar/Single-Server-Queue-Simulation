using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(ShowAttribute))]
    public class ShowDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            ShowAttribute attribute = (ShowAttribute) this.attribute;

            bool isReadOnly = !property.editable
                || !attribute.ShouldShowValue
                || !attribute.IsEditable
                || !Application.isPlaying;

            using (new EditorGUI.PropertyScope(position, label, property))
            using (new EditorGUI.DisabledScope(isReadOnly))
            {
                if (!attribute.ShouldShowValue)
                {
                    // The game is not playing and the mode is OnlyOnPlay
                    EditorGUI.LabelField(position, label, new GUIContent("Start playing to see"));
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            bool showChildren = property.isExpanded && ((ShowAttribute) attribute).ShouldShowValue;
            return EditorGUI.GetPropertyHeight(property, label, showChildren);
        }
    }
}
