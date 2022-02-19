using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(Customer), true)]
    public class CustomerDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            Customer value = property.objectReferenceValue as Customer;

            using (new EditorGUI.PropertyScope(position, label, property))
            using (new EditorGUI.DisabledScope(true))
            {
                if (!value)
                {
                    EditorGUI.TextField(position, label, "Null");
                    return;
                }

                position.height = EditorGUIUtility.singleLineHeight;
                label.text += $" #{value.id}";
                EditorGUI.LabelField(position, label);

                using (new EditorGUI.IndentLevelScope())
                {
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.FloatField(position, "interarrival", value.interarrivalTime);
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.FloatField(position, "service", value.serviceTime);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (property.objectReferenceValue as Customer)
                ? 3 * EditorGUIUtility.singleLineHeight + 2 * EditorGUIUtility.standardVerticalSpacing
                : EditorGUIUtility.singleLineHeight;
        }
    }
}
