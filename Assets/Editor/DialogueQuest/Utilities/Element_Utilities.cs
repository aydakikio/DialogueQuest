using System;
using UnityEngine.UIElements;

namespace Editor.DialogueQuest.Utilities
{
    public class Element_Utilities
    {
        #region UI Utilities
        
        public static TextField Create_TextField(string label , string value ,EventCallback<ChangeEvent<string>> On_Change= null)
        {
            TextField textField = new TextField() { label = label, value = value };
            
            if (On_Change != null)
            {
                textField.RegisterValueChangedCallback(On_Change);
            }
            
            return textField;
        }

        public static Button Create_Button(string text , Action On_Click = null)
        {
            Button button = new Button(On_Click) { text = text };
            
            return button;
        }

        public static Foldout Create_FoldOut(string title , bool collapesed = false)
        {
            Foldout foldout = new Foldout() { text = title, value = !collapesed };
            
            return foldout;
        }

        public static TextField Create_Text_Box(string value = null , EventCallback<ChangeEvent<string>> On_Change = null)
        {
            TextField text_box = Create_TextField("", value, On_Change);
            text_box.multiline = true;
            
            return text_box;
        }

        #endregion
        
        
    }
}