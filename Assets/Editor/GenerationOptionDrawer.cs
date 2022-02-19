using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomPropertyDrawer(typeof(GenerationOption), true)]
    public class GenerationOptionDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position,
            SerializedProperty property,
            GUIContent label)
        {
            GenerationOption value = property.objectReferenceValue as GenerationOption;
            GenerationOption[] allOptions = GetAllOptions().ToArray();

            Rect currentPosition = position;

            using (EditorGUI.PropertyScope scope = new EditorGUI.PropertyScope(position, label, property))
            {
                label = scope.content;

                currentPosition.height = EditorGUIUtility.singleLineHeight;
                using (EditorGUI.ChangeCheckScope change = new EditorGUI.ChangeCheckScope())
                {
                    int option = EditorGUI.Popup(
                        currentPosition,
                        label,
                        Array.IndexOf(allOptions, value),
                        allOptions
                            .Select(option => option?.GetName() ?? "Null")
                            .Select(name => new GUIContent(name))
                            .ToArray());

                    if (change.changed)
                    {
                        property.objectReferenceValue = allOptions[option];
                        return;
                    }
                }
            }

            if (!value)
                return;

            using (new EditorGUI.IndentLevelScope())
            {
                currentPosition.y = currentPosition.yMax + GetBaseHeight();
                currentPosition.height = position.yMax - currentPosition.y;
                value.OnPropertyGUI(currentPosition);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            GenerationOption value = property.objectReferenceValue as GenerationOption;

            if (!value)
                return EditorGUIUtility.singleLineHeight;

            float additionalHeight = value.GetGUIHeight();

            if (additionalHeight > 0.0f)
                additionalHeight += EditorGUIUtility.standardVerticalSpacing;

            return EditorGUIUtility.singleLineHeight + GetBaseHeight() + additionalHeight;
        }

        private static IEnumerable<GenerationOption> GetAllOptions()
        {
            return AssetDatabase.FindAssets($"t:{nameof(GenerationOption)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GenerationOption>);
        }

        private static float GetBaseHeight()
        {
            return 0.0f * (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);

        }
    }
}
