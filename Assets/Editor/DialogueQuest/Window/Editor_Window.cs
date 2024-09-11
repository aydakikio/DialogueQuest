using System;
using System.Text.RegularExpressions;
using DialogueQuest;
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
        private Toolbar toolbar;
        private Graph_View graph;
        private string search_values;

        private Label totoal_number;
        
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

        private void Update()
        {
            
            totoal_number.text = graph.get_total_nodes_number();
            
        }

        private void ADD_GraphView()
        {
            graph = new Graph_View();
            graph.StretchToParentSize();
            rootVisualElement.Add(graph);
        }

        #region ToolBar and ToolBar Utilities

        private void Create_Toolbar()
        {
            toolbar = new Toolbar();
            
            file_name_field = Element_Utilities.Create_TextField("File Name:", "New Graph", callback => {
                file_name_field.value = remove_unwanted_chracters(callback.newValue);
            });
            
            //Toolbar Buttons
            Save_button = Element_Utilities.Create_Button("Save");
            Button Load_button = Element_Utilities.Create_Button("Load");
            
            //Node count info 
            Label space = new Label("                "); //Between Save panel and graph status
            
            Label status_label = new Label("  Nodes: ");
            totoal_number = new Label(graph.get_total_nodes_number());
            
            //Caution!!! The fields in 
            //Search bar 
            Label other_space = new Label("                  "); //Between graph status and search bar
            TextField search_field = Element_Utilities.Create_TextField("Search:  " , "type", callback => { search_values = callback.newValue; });
            Button search_button = Element_Utilities.Create_Button("Search" , ()=>{Search_Node_names(search_values);});
            
            //Insert UI Elements to Toolbar
            toolbar.Insert(0,file_name_field);
            toolbar.Insert(1,Save_button);
            toolbar.Insert(2,Load_button);
            
            toolbar.Insert(3,space);
            toolbar.Insert(4,status_label);
            toolbar.Insert(5, totoal_number );
            
            toolbar.Insert(6,other_space);
            toolbar.Insert(7, search_field);
            toolbar.Insert(8,search_button);
            
            
            
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

        private void Search_Node_names(string value)
        {
            
        }
        
        #endregion




        #region Save and Load

        private void Save(string statics_temp_name)
        {
            
        }

        private void Load()
        {
            
        }

        #endregion
        
        
    }
}

