using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JustGame.Scripts.Enemy
{
    [CustomPropertyDrawer(typeof(BrainTransition))]
    public class BrainTransitionCustomPropertyDrawer : PropertyDrawer
    {
        const float LineHeight = 16f;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            Rect position = rect;
            foreach (SerializedProperty a in property)
            {
                var height = Mathf.Max(LineHeight, EditorGUI.GetPropertyHeight(a));
                position.height = height;

                if(a.name == "BrainDecision")
                {
                    // draw the decision dropdown
                    DrawSelectionDropdown(position, property);

                    // draw the base decision field
                    position.y += height;
                    EditorGUI.PropertyField(position, a, new GUIContent(a.name));
                    position.y += height;
                }
                else
                {
                    EditorGUI.PropertyField(position, a, new GUIContent(a.name));
                    position.y += height;
                }
            }
        }

        protected virtual void DrawSelectionDropdown(Rect position, SerializedProperty prop)
        {
            BrainDecision thisDecision = prop.objectReferenceValue as BrainDecision;
            BrainDecision[] decisions = (prop.serializedObject.targetObject as EnemyBrain).GetAttachedDecisions();
            int selected = 0;
            int i = 1;
            string[] options = new string[decisions.Length + 1];
            options[0] = "None";
            foreach (BrainDecision decision in decisions)
            {
                string name = string.IsNullOrEmpty(decision.Label) ? decision.GetType().Name : decision.Label;
                options[i] = i.ToString() + " - " + name;
                if (decision == thisDecision)
                {
                    selected = i;
                }
                i++;
            }

            EditorGUI.BeginChangeCheck();
            selected = EditorGUI.Popup(position, selected, options);
            if (EditorGUI.EndChangeCheck())
            {
                prop.objectReferenceValue = (selected == 0) ? null : decisions[selected - 1];
                prop.serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(prop.serializedObject.targetObject);
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 0;
            
            foreach (SerializedProperty a in property)
            {
                var h = Mathf.Max(LineHeight, EditorGUI.GetPropertyHeight(a));
                if(a.name == "BrainDecision")
                {
                    height += h * 2;
                }
                else
                {
                    height += h;
                }
            }
            return height;
        }
    }
}

