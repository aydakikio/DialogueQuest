using System;
using System.Text.RegularExpressions;
using DialogueQuest.Utilities;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Dialogue_Quest.Window
{
    public class Editor_Window: EditorWindow
    {
        private TextField file_name_field;
        private Button Save_button;
        
        [MenuItem("Window/Dialogue_Quest")]    
        public static void ShowExample()
        {
            Editor_Window window = GetWindow<Editor_Window>("Dialogue_Quest_Editor_Window");
        }

        private void OnEnable()
        {
            ADD_GraphView();
            Create_Toolbar();
        }

        private void ADD_GraphView()
        {
            Graph_View graph = new Graph_View();
            graph.StretchToParentSize();
            rootVisualElement.Add(graph);
        }

        #region ToolBar and ToolBar Utilities

        private void Create_Toolbar()
        {
            Toolbar toolbar = new Toolbar();
            file_name_field = Element_Utilities.Create_TextField("File Name:", "New Graph", callback => {
                file_name_field.value = remove_unwanted_chracters(callback.newValue);
            });
            
            //Toolbar Buttons
            Save_button = Element_Utilities.Create_Button("Save");
            Button Load_button = Element_Utilities.Create_Button("Load");
            
            //Insert UI Elements to Toolbar
            toolbar.Insert(0,file_name_field);
            toolbar.Insert(1,Save_button);
            toolbar.Insert(2,Load_button);
            
            rootVisualElement.Add(toolbar);
        }

        private string remove_unwanted_chracters(string text)
        {
            return Regex.Replace(text,@"\(([#%&{}\\<>*?/ $!'"":@+`|=.])\W", string.Empty);
        }
        
        private void save_button_state (int mode)
        {
            switch (mode)
            {
                case 0: //Enables Save Button
                    Save_button.SetEnabled(true);
                    break;
                case 1: //Disables Save Button
                    Save_button.SetEnabled(false);
                    break;
            }
        }
        
        #endregion


        #region Save and Load

        private void Save()
        {
            
        }

        private void Load()
        {
            
        }

        #endregion
        
    }
}

