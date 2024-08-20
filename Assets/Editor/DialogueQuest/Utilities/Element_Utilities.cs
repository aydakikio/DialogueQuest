using System;
using System.Security.Cryptography;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueQuest.Utilities
{
    public static class Element_Utilities
    {
        #region UI Utilities
        
        public static TextField Create_TextField(string label , string value ,EventCallback<ChangeEvent<string>> On_Change= null)
        {
            TextField textField = new TextField() { label = label, value = value };
            
            if (On_Change != null)
            {
                textField.RegisterValueChangedCallback(Event => { value = Event.newValue;});
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

        #region  Other

        public static Port Create_Port(this Node nodes , Orientation oriantaion , Direction direction , Port.Capacity capacity )
        {
            Port port = nodes.InstantiatePort(oriantaion, direction, capacity, typeof(int));
            
            return port;
        }
        
        public static string Hash(string data)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(data));

                foreach (var item in result)
                {
                    sb.Append(item.ToString("x2"));
                }
            }

            return sb.ToString();
        }

        #endregion
        
    }
}