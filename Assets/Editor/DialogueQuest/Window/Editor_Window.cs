using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DialogueQuest;
using DialogueQuest.Elements;
using DialogueQuest.Utilities;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Search;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
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
        
        //private int current_search_index = 0;
        //private List<Basic_Node> founded_nodes = new List<Basic_Node>();
        
        //List of nodes
        //List of start points 
        
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
            Save_button = Element_Utilities.Create_Button("Save" , ()=> save());
            Button Load_button = Element_Utilities.Create_Button("Load" , ()=> load());
            
            //Node count info 
            Label space_between_save_panel_and_Graph_status = new Label("                "); //Between Save panel and graph status
            
            Label status_label = new Label("  Nodes: ");
            totoal_number = new Label(graph.get_total_nodes_number());
            
            /*  /!\ Caution: Disabled due to problems!
             
            //Search Bar
            Label space_between_search_field_and_graph_status = new Label("        ");

            TextField search_field = Element_Utilities.Create_TextField("" ,"Search Here! " );
            search_field.MarkDirtyRepaint();

            Button search_button = Element_Utilities.Create_Button("Search" , ()=>search(search_field.value));

            //Result Panel
            Label space_between_searchbar_and_result_panel = new Label("                  ");


            Button previous_button = Element_Utilities.Create_Button("Previous" , ()=>previous_item_in_search_results());

            Label space_between_buttons = new Label("   ");
            Button next_button = Element_Utilities.Create_Button("Next" , ()=> next_item_in_search_results() );

            */
            
            //Insert UI Elements to Toolbar
            toolbar.Insert(0,file_name_field);
            toolbar.Insert(1,Save_button);
            toolbar.Insert(2,Load_button);
            
            toolbar.Insert(3,space_between_save_panel_and_Graph_status);
            toolbar.Insert(4,status_label);
            toolbar.Insert(5, totoal_number );
            
            
            /*  /!\ Caution: Disabled due to problems!
          toolbar.Insert(6,space_between_search_field_and_graph_status);
          toolbar.Insert(7,search_field);

          toolbar.Insert(8, search_button);

          toolbar.Insert(9, space_between_searchbar_and_result_panel);
          toolbar.Insert(10,previous_button);
          toolbar.Insert(11, space_between_buttons);
          toolbar.Insert(12 , next_button);
          */
            
            rootVisualElement.Add(toolbar);
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
        
        private string remove_unwanted_chracters(string text)
        {
            return Regex.Replace(text,@"\(([#%&{}\\<>*?/ $!'"":@+`|=.])\W", string.Empty);
        }
        
        #endregion

        #region Search_graph --> /!\ Caution: Disabled due to problems! 
        /*
        private void search(string value)
        {
            founded_nodes.Clear();
            founded_nodes = graph.Search_The_Graph(value);
            current_search_index = 0;
        }

        private void next_item_in_search_results()
        {
            current_search_index++;
            Basic_Node node;
            
            //Changes style to result style

            
            if (current_search_index >= founded_nodes.Count)
            {
                node = founded_nodes[0];
                graph.zoom_in_found_node(node);
                //Changes style to result style
                
                return;
            } 
            node = founded_nodes[current_search_index];
            graph.zoom_in_found_node(node);
            
        }

        private void previous_item_in_search_results()
        {
            current_search_index--;
            Basic_Node node;
            if (current_search_index < 1 )
            {
                int index = founded_nodes.Count - 1;
                node = founded_nodes[index];
                graph.zoom_in_found_node(node);
                
                //Changes style to result style
                
                return;
            }
            
            node = founded_nodes[current_search_index];
            graph.zoom_in_found_node(node);
            
            //Changes style to result style
            
        }

            */
        
        #endregion
        

        #region Save & Load

        private void save()
        {
            //Add new if for preventing graph to be saved with errors
            if (string.IsNullOrEmpty(file_name_field.value))
            {
                EditorUtility.DisplayDialog("Invalid file name!" , "Please ensure the file name that you typed in is valid ", "OK");
                return;
            }
            
            Save_Utilities.Start_Saving(graph , file_name_field.value);
            Save_Utilities.Save();
        }

        private void load()
        {
            
        }
        

        #endregion
    }
}

