using System.Collections.Generic;
using System.Linq;
using Dialogue_Quest.Window;
using DialogueQuest.Data;
using DialogueQuest.Data.Save;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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
        private static Dictionary<string, Control_Node_Save_SO> created_control_node;
        

        private static Dictionary<string, Basic_Node> loaded_basic_nodes;
        private static Dictionary<string, Control_Node> loaded_control_node;

        
        private static void ON_Start_Of_Saving (Graph_View graph_view , string file_name)
        {
            graph = graph_view;

            graph_file_name = file_name;

            basic_nodes = new List<Basic_Node>();
            control_nodes = new List<Control_Node>();
            

            created_basic_nodes = new Dictionary<string, Basic_Node_Save_SO>();
            created_control_node = new Dictionary<string, Control_Node_Save_SO>();

            loaded_basic_nodes = new Dictionary<string, Basic_Node>();
            loaded_control_node = new Dictionary<string, Control_Node>();
        } 
        
        #region Main functions
        
        public static void Save(Graph_View graph_view , string file_name)
        {
            
           ON_Start_Of_Saving(graph_view , file_name);

           Create_root_folders();
           Get_Graph_Elements();

           Graph_Save editor_data = new Graph_Save();
           editor_data.Initialize(file_name);

           Graph_Container runtime_data = new Graph_Container();
           runtime_data.Initialize(file_name);
           
           save_nodes(0,editor_data , runtime_data);//For basic nodes
           save_nodes(1 , editor_data , runtime_data);
           
           
           
           save_asset(editor_data);
           save_asset(runtime_data);
        }

        public static void Load()
        {
            
            Graph_Save editor_info = Load_Asset<Graph_Save>($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}" , graph_file_name);

            if (editor_info == null)
            {
                //EditorUtility.DisplayDialog ("The file couldn't find!" , "The file at path could not find \n\n" +
                //                                                         "" + "" + "" , "OK"); //Shows Error 
                
                return;
            }
            
        }

        #endregion


        #region Load Graph Elements

        private static void Load_Nodes(int mode, List<Basic_Node_Save> basic_nodes, List<Control_Node_Save> control_nodes)
        {
            switch (mode)
            {
                case 0 : // For basic nodes
                    foreach (Basic_Node_Save basic_node_data in basic_nodes)
                    {
                        List<Choice_Save> choices = clone_choices(basic_node_data.choices);

                        Basic_Node basic_node =
                            graph.Create_Node(basic_node_data.Position, "Basic", basic_node_data.type) as Basic_Node;

                        basic_node.ID = basic_node_data.ID;
                        basic_node.Dialogue = basic_node_data.Dialogue;
                        basic_node.Node_name = basic_node_data.Name;
                        basic_node.choices = basic_node_data.choices;
                        basic_node.Flags = basic_node_data.flags;
                        
                        basic_node.draw();
                        
                        graph.Add(basic_node);
                        
                        loaded_basic_nodes.Add(basic_node.ID , basic_node);
                    }
                    break;
                case 1 : //For Control nodes 
                    foreach (Control_Node_Save control_node in control_nodes)
                    {
                        
                    }
                    break;
            }
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

        private static void save_nodes(int mode,Graph_Save graph_data , Graph_Container graph_container)
        {
            switch (mode)
            {
                case 0: // Basic _Node saving
                    List<string> basic_node_names = new List<string>();

                    foreach (Basic_Node node in basic_nodes)
                    {
                        save_basic_nodes_To_Graph(node,graph_data);
                        save_basic_nodes_To_SO_container(node , graph_container);
                    }
            
                    update_choice_connection();
                    update_basic_nodes(basic_node_names, graph_data);
                    
                    break;
                case 1 : //Control nodes saving
                    foreach (Control_Node node in control_nodes)
                    {
                        save_control_nodes_to_editor(node,graph_data);
                        save_control_nodes_to_SO_container(node , graph_container);
                    }
                    break;
            }
        }

        #region Editor part Saving
        
        private static void save_basic_nodes_To_Graph(Basic_Node node,Graph_Save graphSave)
        {

            Basic_Node_Save basic_node_Container = new Basic_Node_Save()
            {
                ID = node.ID, 
                Name = node.Node_name , 
                type = node.type,
                Dialogue = node.Dialogue,
                flags = clone_flags(node.Flags),
                choices = clone_choices(node.choices),
                Position = node.GetPosition().position
            };
                
            graphSave.Basic_nodes.Add(basic_node_Container);
        }

        private static void save_control_nodes_to_editor(Control_Node node , Graph_Save graphSave)
        {
            Control_Node_Save control_node_container = new Control_Node_Save()
            {
                ID = node.ID,
                type = node.type,
                Position = node.GetPosition().position
            };
            
            graphSave.control_nodes.Add(control_node_container);
        }
        
        #endregion

        #region Save runtime infos (Nodes)

        private static void save_basic_nodes_To_SO_container(Basic_Node node  , Graph_Container graph_Container)
        {
            Basic_Node_Save_SO basic_node_container;
            
            basic_node_container = Create_Asset<Basic_Node_Save_SO>($"Assets/Dialogue_Manager/Save/Cache/Save/Cache/{graph_file_name}/Elements/Basic" , node.Node_name);
            graph_Container.graph_basic_nodes.Add(basic_node_container);
            
            basic_node_container.Initialize(node.Node_name , node.type ,node.Dialogue, Convert_To_Flag_Data(node.Flags), Convert_To_Choice_Data(node.choices) ,node.GetPosition().position);
            
            created_basic_nodes.Add(node.ID , basic_node_container);
            save_asset(basic_node_container);
        }

        private static void save_control_nodes_to_SO_container(Control_Node node , Graph_Container graph_Container)
        {
            
        }
        
        private static void update_basic_nodes(List<string> current_Basic_Nodes_name , Graph_Save graph_data)
        {
            if (graph_data.Basic_nodes != null && graph_data.Basic_nodes.Count != 0)
            {
                List<string> remove_nodes = graph_data.Basic_nodes_old_names.Except(current_Basic_Nodes_name).ToList();

                foreach (string name in remove_nodes)
                {
                    AssetDatabase.DeleteAsset($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements/Basic/{name}.asset");
                }
            }
        }

        #endregion


        #region Save addictional componets 
        private static List<Flag_Save> clone_flags(List<Flag_Save> nodeFlags)
        {

            List<Flag_Save> flags = new List<Flag_Save>();

            foreach (Flag_Save flag in nodeFlags)
            {
                Flag_Save flag_container = new Flag_Save() { Flag_text = flag.Flag_text , Node_ID = flag.Node_ID};
                
                flags.Add(flag_container);
            }

            return flags;
        }

        private static List<Choice_Save> clone_choices(List<Choice_Save> node_choices)
        {
            List<Choice_Save> choices = new List<Choice_Save>();

            foreach (Choice_Save choice in node_choices)
            {
                Choice_Save choice_container = new Choice_Save() {Choice_Text = choice.Choice_Text  , Node_Id = choice.Node_Id};
                
                choices.Add(choice_container);
            }
            
            return choices;
        }
        
        private static List<Flag_Save> save_flags(List<Flag_Save> node_Flags)
        {
            List<Flag_Save> flags = new List<Flag_Save>();

            foreach (Flag_Save flag in node_Flags)
            {
                Flag_Save flag_data = new Flag_Save() { Flag_text = flag.Flag_text };

                flags.Add(flag_data);
            }

            return flags;
        }

        private static List<Choice_Save> Save_Choices(List<Choice_Save> current_choices)
        {
            List<Choice_Save> clone_choices = new List<Choice_Save>();

            foreach (Choice_Save choice in current_choices)
            {
                Choice_Save choice_data = new Choice_Save() { Choice_Text = choice.Choice_Text };
                
                clone_choices.Add(choice_data);
            }

            return clone_choices;
        }
        
        private static void update_choice_connection()
        {
            foreach (var node in basic_nodes)
            {
                var node_container = created_basic_nodes[node.ID];

                for (var choice_index = 0; choice_index < node.choices.Count; choice_index++)
                {
                    var node_choice = node.choices[choice_index];

                    if (string.IsNullOrEmpty(node_container.Id)) continue;

                    node_container.Choices[choice_index].NextSavedBasicNodeSaveSO = created_basic_nodes[node.ID];
                }
            }
        }

        #endregion

        #region Convert editor infos to runtime 

        private static List<Choice_Data> Convert_To_Choice_Data(List<Choice_Save> node_Choices)
        {
            List<Choice_Data> data_choices = new List<Choice_Data>();

            foreach (Choice_Save choice in node_Choices)
            {
                Choice_Data data = new Choice_Data() { Choice_Text = choice.Choice_Text };
                
                data_choices.Add(data);
            }
            return data_choices;
        }

        private static List<Flag_Data> Convert_To_Flag_Data(List<Flag_Save> node_flags)
        {
            List<Flag_Data> data_flags = new List<Flag_Data>();

            foreach (Flag_Save flag in node_flags)
            {
                Flag_Data data = new Flag_Data() { Flag_text = flag.Flag_text };
                
                data_flags.Add(data);
            }
            return data_flags;
        }


        #endregion

        

        
        #endregion

        #region Save Folder & File Management

        private static void Create_root_folders()
        {
            Create_Save_Folders();
            Create_Cache_folders();
        }
        private static void Create_Save_Folders()
        {
            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save") == false)
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/", "Save");

            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") &&
                AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs")) return;

            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") == false &&
                AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs"))
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save", "Cache");

                return;
            }

            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Cache") &&
                AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save/Saved_Graphs") == false)
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save/", "Saved_Graphs");

                return;
            }

            AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save", "Cache");
            AssetDatabase.CreateFolder("Assets/Dialogue_Manager/Save/", "Saved_Graphs");
        }

        private static void Create_Cache_folders()
        {
            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}") == false)
            {
                AssetDatabase.CreateFolder($"Assets/Dialogue_Manager/Save/Cache" , $"{graph_file_name}");
            }

            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements") == false)
            {
                AssetDatabase.CreateFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}" , "Elements");
            }

            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements/Basic") == false)
            {
                AssetDatabase.CreateFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements" , "Basic");
            }

            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements/Control") == false)
            {
                AssetDatabase.CreateFolder($"Assets/Dialogue_Manager/Save/Cache/{graph_file_name}/Elements", "Control");
            }
        }

        private static T Create_Asset<T>(string path, string name) where T : ScriptableObject
        {
            var full_path = $"{path}/{name}.asset";

            var asset = Load_Asset<T>(path, name);

            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<T>();

                AssetDatabase.CreateAsset(asset, full_path);
            }

            return asset;
        }

        private static T Load_Asset<T>(string path, string Asset_name) where T : ScriptableObject
        {
            var full_path = $"{path}/{Asset_name}.asset";

            return AssetDatabase.LoadAssetAtPath<T>(full_path);
        }

        private static void save_asset(Object asset)
        {
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        
        #endregion


    }
}