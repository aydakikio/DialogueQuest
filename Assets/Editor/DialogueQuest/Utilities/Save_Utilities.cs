using System.Collections.Generic;
using Dialogue_Quest.Window;
using DialogueQuest.Data;
using DialogueQuest.Data.Save;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using UnityEditor;
using UnityEngine;

namespace DialogueQuest.Utilities
{
    public static class Save_Utilities
    {
        private static Graph_View graph;

        private static string graph_file_name;
        
        private static List<Basic_Node> basic_nodes;
        private static List<Control_Node> control_nodes;

        private static Dictionary<string, Basic_Node_Save_SO> created_basic_nodes;
        private static Dictionary<string, Control_Node> created_control_node;

        private static Dictionary<string, Basic_Node_Save_SO> loaded_basic_nodes;
        private static Dictionary<string, Control_Node> loaded_control_node;

        

        #region Main functions
        
        public static void Save(Graph_View graph_view , string file_name)
        {
            ON_Start_Of_Saving(graph_view , file_name);
            
            Create_Save_Folder();
            Get_Graph_Elements();
            
            Graph_Save graph_data = Create_Asset<Graph_Save>("Assets/Dialogue_Manager/Save/Cache" , graph_file_name ); // PATH: "Assets/Dialogue_Manager/Save/Cache"
            graph_data.Initialize(graph_file_name);

            Graph_Container graph_container = Create_Asset<Graph_Container>( "Assets/Dialogue_Manager/Save/Saved_Graphs" , graph_file_name); // PATH: "Assets/Dialogue_Manager/Save/Saved_Graphs"
            graph_container.Initialize(graph_file_name);
            
            save_basic_nodes();
        }

        public static void Load()
        {
            
        }

        #endregion

        #region Save Folder & File Management 

        private static void Create_Save_Folder()
        {
            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save") == false )
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/" , "Save" );
                
            }
            
            if ((AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") == true) && (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs") == true ))
            {
                return;
            }

            if ((AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") == false ) && (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs") == true ))
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save" , "Cache");
                
                return;
            } 
            if ((AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") == true ) && (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs") == false ))
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save/" , "Saved_Graphs");
                
                return;
            }
            
            AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save" , "Cache");
            AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save/" , "Saved_Graphs");
        }

        private static T Create_Asset<T>(string path, string name ) where T : ScriptableObject
        {
            string full_path = $"{path}/{name}.asset";
            
            T asset = Load_Asset<T>(path, name);
            
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();
                
                AssetDatabase.CreateAsset(asset,full_path);
            }

            return asset;
        }

        private static T Load_Asset<T>(string path, string Asset_name) where T : ScriptableObject
        {
            string full_path = $"{path}/{Asset_name}.asset";

            return AssetDatabase.LoadAssetAtPath<T>(full_path);
        }

        private static void save_asset()
        {
            
        }
        #endregion

        #region Save Graph Elements

        private static void Get_Graph_Elements()
        {
            graph.graphElements.ForEach(element =>
            {
                if (element is Basic_Node base_node)
                {
                    basic_nodes.Add(base_node);

                    return;
                }

                if (element is Control_Node control_node)
                {
                    control_nodes.Add(control_node);
                    
                    return;
                }
                
            } );
        }
        
        private static void save_basic_nodes()
        {
            foreach (Basic_Node node in basic_nodes)
            {
                Basic_Node_Save_SO nodeSaveSoContainer = new Basic_Node_Save_SO();
                
                nodeSaveSoContainer.Id = node.ID;
                nodeSaveSoContainer.name = node.Node_name;
                nodeSaveSoContainer.type = node.type;
                
                nodeSaveSoContainer.Dialogue = node.Dialogue;
                
                nodeSaveSoContainer.Flag_Infos = node.Flags as List<Flag_Data>;
                nodeSaveSoContainer.Choices = Save_Choices(node.choices);
                
                nodeSaveSoContainer.Node_Position = node.GetPosition().position;
                
            }            
        }

        private static List<Choice_Data> Save_Choices(List<Choice_Data> current_choices)
        {
            List<Choice_Data> choices = new List<Choice_Data>();

            foreach (Choice_Data choice in current_choices)
            {
                Choice_Data choice_data = new Choice_Data() { Choice_Text = choice.Choice_Text };
                
                choices.Add(choice_data);
            }

            return choices;
        }

        private static void Save_Control_Nodes()
        {
            /*
            foreach (Control_Node node in control_nodes)
            {
                Control_Node_Save control_node_container = new Control_Node_Save();
                
                
            }
            */
        }
        #endregion

        private static void ON_Start_Of_Saving (Graph_View graph_view , string file_name)
        {
            graph = graph_view;

            graph_file_name = file_name;

            basic_nodes = new List<Basic_Node>();
            control_nodes = new List<Control_Node>();
        } 
    }
}