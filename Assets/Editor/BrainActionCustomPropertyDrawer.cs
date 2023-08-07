using JustGame.Scripts.Enemy;
using UnityEditor;
using UnityEngine;

namespace JustGame.Scripts.EditorExtension
{
    [CustomPropertyDrawer(typeof(BrainAction))]
    public class BrainActionCustomPropertyDrawer : PropertyDrawer
    {
        private const float LineHeight = 16f;
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            // determines the height of the Action property
            var height = Mathf.Max(LineHeight, EditorGUI.GetPropertyHeight(property));
            Rect position = rect;
            position.height = height;

            // draws the dropdown
            DrawSelectionDropdown(position, property);

            // draws the base field
            position.y += height;
            EditorGUI.PropertyField(position, property);
        }
        
        private void DrawSelectionDropdown(Rect position, SerializedProperty prop)
        {
            BrainAction thisAction = prop.objectReferenceValue as BrainAction;
            BrainAction[] actions = (prop.serializedObject.targetObject as EnemyBrain).GetAttachedActions();
            int selected = 0;
            int i = 1;
            string[] options = new string[actions.Length + 1];
            options[0] = "None";
            foreach (BrainAction action in actions)
            {
                string name = string.IsNullOrEmpty(action.Label) ? action.GetType().Name : action.Label;
                options[i] = i.ToString() + " - " + name;
                if (action == thisAction)
                {
                    selected = i;
                }
                i++;
            }

            EditorGUI.BeginChangeCheck();
            selected = EditorGUI.Popup(position, selected, options);
            if (EditorGUI.EndChangeCheck())
            {
                prop.objectReferenceValue = (selected == 0) ? null : actions[selected - 1];
                prop.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(prop.serializedObject.targetObject);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var h = Mathf.Max(LineHeight, EditorGUI.GetPropertyHeight(property));
            float height = h * 2;
            return height;
        }
    }
}

