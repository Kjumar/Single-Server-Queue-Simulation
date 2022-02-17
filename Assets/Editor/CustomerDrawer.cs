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

                Rect line1 = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                Rect line2 = new Rect(
                    position.x,
                    line1.yMax + EditorGUIUtility.standardVerticalSpacing,
                    position.width,
                    EditorGUIUtility.singleLineHeight);
                Rect line3 = new Rect(
                    position.x,
                    line2.yMax + EditorGUIUtility.standardVerticalSpacing,
                    position.width,
                    EditorGUIUtility.singleLineHeight);

                label.text += $" #{value.id}";
                EditorGUI.LabelField(line1, label);

                using (new EditorGUI.IndentLevelScope())
                {
                    EditorGUI.FloatField(line2, "interarrival", value.interarrivalTime);
                    EditorGUI.FloatField(line3, "service", value.serviceTime);
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
