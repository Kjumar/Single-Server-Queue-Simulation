using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(GenerationOptionDropdown))]
    public class GenerationOptionDropdownEditor : UnityEditor.Editor
    {
        private SerializedProperty optionsProperty;
        private SerializedProperty onOptionSelectedProperty;

        private void OnEnable()
        {
            optionsProperty = serializedObject.FindProperty("options");
            onOptionSelectedProperty = serializedObject.FindProperty("onOptionSelected");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            if (GUILayout.Button("Refresh List of Generation Options"))
            {
                BuildListOfGenerationOptions();
                return;
            }

            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            EditorGUILayout.LabelField("Options");

            using (new EditorGUI.IndentLevelScope())
            using (new EditorGUI.DisabledGroupScope(true))
            {
                int count = optionsProperty.arraySize;

                for (int i = 0; i < count; i++)
                {
                    EditorGUILayout.ObjectField(
                        optionsProperty.GetArrayElementAtIndex(i),
                        new GUIContent(i.ToString()));
                }
            }

            EditorGUILayout.Space(EditorGUIUtility.standardVerticalSpacing);

            EditorGUILayout.PropertyField(onOptionSelectedProperty);

            serializedObject.ApplyModifiedProperties();
        }

        private void BuildListOfGenerationOptions()
        {
            GenerationOptionDropdown obj = target as GenerationOptionDropdown;

            if (obj == null)
            {
                EditorGUILayout.HelpBox("Object not set as GenerationOptionDropdown", MessageType.Error);
                return;
            }

            Dropdown dropdown = obj.GetComponent<Dropdown>();

            GenerationOption[] list = AssetDatabase.FindAssets($"t:{nameof(GenerationOption)}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<GenerationOption>).ToArray();

            FieldInfo optionsField = obj.GetType().GetField("options", BindingFlags.Instance | BindingFlags.NonPublic);

            if (optionsField == null)
            {
                EditorGUILayout.HelpBox("Failed to find field \"options\"", MessageType.Error);
                return;
            }

            optionsField.SetValue(obj, list);

            if (dropdown == null)
                return;

            dropdown.ClearOptions();
            dropdown.AddOptions(list.Select(option => option.GetName()).ToList());
        }
    }
}
