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
        private Graph_Static_handeler static_handler;
        private string statics_temp_name;
        private Toolbar toolbar;
        private Graph_View graph;

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
            Label status_label = new Label("  Nodes: ");
            totoal_number = new Label(graph.get_total_nodes_number());
            
            //Insert UI Elements to Toolbar
            toolbar.Insert(0,file_name_field);
            toolbar.Insert(1,Save_button);
            toolbar.Insert(2,Load_button);
            toolbar.Insert(3,status_label);
            toolbar.Insert(4 , totoal_number );
            
            
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

        #region Graph Statics

        private void Create_Graph_Statics(int mode)
        {
            static_handler = new Graph_Static_handeler();

            statics_temp_name = static_handler.create_graph_static();
            
            
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

