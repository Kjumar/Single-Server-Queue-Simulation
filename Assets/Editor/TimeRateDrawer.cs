using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(TimeRate))]
    public class TimeRateDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            TimeRate value = (TimeRate) fieldInfo.GetValue(property.serializedObject.targetObject);

            using (EditorGUI.PropertyScope scope = new EditorGUI.PropertyScope(position, label, property))
            {
                label = scope.content;

                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("ratePerHour"), label);

                using (new EditorGUI.IndentLevelScope())
                using (new EditorGUI.DisabledScope(true))
                {
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.FloatField(position, "Time (hrs)", value.MeanTimeHour);
                    position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    EditorGUI.FloatField(position, "Time (mins)", value.MeanTimeMinute);
                }
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 3 * EditorGUIUtility.singleLineHeight + 2 * EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
