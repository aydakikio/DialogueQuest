using System;
using UnityEditor;
using UnityEngine;

namespace DialogueQuest.Utilities
{
    public static class Inspector_Utility
    {
        public static void draw_disabled_fields(Action action)
        {
            EditorGUI.BeginDisabledGroup(true);
            action.Invoke();
            EditorGUI.EndDisabledGroup();
        }

        public static void Draw_title(string label)
        {
            EditorGUILayout.LabelField(label , EditorStyles.whiteBoldLabel);
        }

        public static void Draw_HelpBox(string message , MessageType type = MessageType.Info , bool wide = true)
        {
            EditorGUILayout.HelpBox(message, type , wide);
        }

        public static int Draw_PopUP(string label , SerializedProperty selected_Index_Property , String[] options)
        {
            return EditorGUILayout.Popup(label,selected_Index_Property.intValue ,options);
        }

        public static int Draw_PopUP(string label , int selected_Index , String[] options )
        {
            return EditorGUILayout.Popup(label, selected_Index, options);
        }

        public static bool Draw_PropertyField(this SerializedProperty property)
        {
            return EditorGUILayout.PropertyField(property);
        }

        public static void Draw_Space(int size = 4)
        {
            EditorGUILayout.Space(size);
        }
    }
}