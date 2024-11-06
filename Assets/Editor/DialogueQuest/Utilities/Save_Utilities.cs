using System.Collections.Generic;
using System.Linq;
using Dialogue_Quest.Window;
using DialogueQuest.Data;
using DialogueQuest.Data.Save;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;

namespace DialogueQuest.Utilities
{
    public static class Save_Utilities
    {
        private static Graph_View graph;

        private static string graph_file_name;
        
        private static List<Basic_Node> basic_nodes;
        private static List<Control_Node> control_nodes;
        private static Dictionary<string, Basic_Node> start_points;

        private static Dictionary<string, Basic_Node_Save_SO> created_basic_nodes;
        private static Dictionary<string, Control_Node_Save_SO> created_control_node;
        private static List<Edge> created_edges;
        

        private static Dictionary<string, Basic_Node> loaded_basic_nodes;
        private static Dictionary<string, Control_Node> loaded_control_node;

        

        
        public static void Start_Saving (Graph_View graph_view , string file_name)
        {
            graph = graph_view;

            graph_file_name = file_name;

            basic_nodes = new List<Basic_Node>();
            control_nodes = new List<Control_Node>();
            
            
            created_basic_nodes = new Dictionary<string, Basic_Node_Save_SO>();
            created_control_node = new Dictionary<string, Control_Node_Save_SO>();
            start_points = new Dictionary<string, Basic_Node>();
            created_edges = new List<Edge>();
            

            loaded_basic_nodes = new Dictionary<string, Basic_Node>();
            loaded_control_node = new Dictionary<string, Control_Node>();
            
        } 
        
        #region Main functions
        
        public static void Save()
        {

           Create_root_folders();
           Get_Graph_Elements();
           
           Graph_Save editor_data = Create_Asset<Graph_Save>($"Assets/DialogueManager/Save/Cache/{graph_file_name}" , graph_file_name);
           editor_data.Initialize(graph_file_name);
           
           Graph_Container runtime_data = Create_Asset<Graph_Container>("Assets/DialogueManager/save/SavedGraphs" , graph_file_name);
           runtime_data.Initialize(graph_file_name);
           
           save_nodes(0,editor_data , runtime_data);//For basic nodes
           save_nodes(1 , editor_data , runtime_data);

           editor_data.graph_edges = created_edges;
           
           save_asset(editor_data);
           save_asset(runtime_data);
           
           AssetDatabase.CopyAsset($"Assets/DialogueManager/Save/Cache/{graph_file_name}/{graph_file_name}.asset", $"Assets/DialogueManager/Save/Cache/{graph_file_name}/recovery/{graph_file_name}");
           
        }

        public static void Load()
        {
            
            Graph_Save editor_info = Load_Asset<Graph_Save>($"Assets/DialogueManager/Save/Cache/{graph_file_name}" , graph_file_name);

            if (editor_info == null)
            {
                EditorUtility.DisplayDialog ("The file couldn't find!" , "The file at path could not find \n\n" +
                                                                         $"Assets/DialogueManager/Save/Cache/{graph_file_name} \n\n" + "please find and restore the file to previous shown path by going to \n\n" + 
                                                                         $"Assets/DialogueManager/Save/Cache/{graph_file_name}/recovery/{graph_file_name}\n\n " , "OK"); //Shows Error 
                
                return;
            }
            
            Editor_Window.update_file_name(editor_info.File_Name);
            Load_Nodes(0,editor_info.Basic_nodes , null); //For Basic Nodes 
            Load_Nodes(1 , null , editor_info.control_nodes); //For Control Nodes
            Load_Node_connections(editor_info); //For created edges
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
                        basic_node.choices = choices;
                        basic_node.Flags = basic_node_data.flags;
                        
                        if (basic_node_data.Is_Start_point is true)
                        {
                            
                            start_points.Add(basic_node.ID , basic_node);
                            //Changes style to
                        }
                        basic_node.draw();
                        
                        graph.Add(basic_node);
                        
                        loaded_basic_nodes.Add(basic_node.ID , basic_node);
                    }
                    break;
                case 1 : //For Control nodes 
                    foreach (Control_Node_Save control_node_data in control_nodes)
                    {
                        Control_Node control_node = graph.Create_Node(control_node_data.Position , "Control" , control_node_data.type) as Control_Node;

                        control_node.ID = control_node_data.ID;
                        
                        control_node.Draw();
                        
                        graph.Add(control_node);
                        
                        loaded_control_node.Add(control_node.ID , control_node);
                    }
                    break;
            }
        }

        private static void Load_Node_connections(Graph_Save editor_data)
        {
            foreach (Edge connection in editor_data.graph_edges)
            {
                Port output_current_node = connection.output;
                

                Edge node_connection = output_current_node.ConnectTo(connection.input);
                
                graph.AddElement(node_connection);
                
                connection.output.node.RefreshPorts();
                connection.input.node.RefreshPorts();
            }
        }

        #endregion



        #region Save Graph Elements

        private static void Get_Graph_Elements()
        {
            created_edges = graph.edges.ToList();
            start_points = graph.get_start_points();
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
            bool is_start_point = false;
            Basic_Node_Save_SO basic_node_container;
            
            basic_node_container = Create_Asset<Basic_Node_Save_SO>($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements/Basic" , node.Node_name);
            graph_Container.graph_basic_nodes.Add(basic_node_container);

            if (start_points.ContainsKey(node.ID))
            {
                is_start_point = true;
            }
            
            basic_node_container.Initialize(node.Node_name ,is_start_point,node.type ,node.Dialogue, Convert_To_Flag_Data(node.Flags), Convert_To_Choice_Data(node.choices) ,node.GetPosition().position);
            
            
            created_basic_nodes.Add(node.ID , basic_node_container);
            save_asset(basic_node_container);
        }

        private static void save_control_nodes_to_SO_container(Control_Node node , Graph_Container graph_Container)
        {
            Control_Node_Save_SO control_node_container;
            
            control_node_container = Create_Asset<Control_Node_Save_SO>($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements/Control" , node.ID);
            
            graph_Container.graph_control_nodes.Add(control_node_container);
            
            control_node_container.Initialize(node.ID , node.type , node.GetPosition().position);
            created_control_node.Add(node.ID , control_node_container);
            
            save_asset(control_node_container);
        }
        
        private static void update_basic_nodes(List<string> current_Basic_Nodes_name , Graph_Save editor_data)
        {
            if (editor_data.Basic_nodes != null && editor_data.Basic_nodes.Count != 0)
            {
                List<string> remove_nodes = editor_data.Basic_nodes_old_names.Except(current_Basic_Nodes_name).ToList();

                foreach (string name in remove_nodes)
                {
                    AssetDatabase.DeleteAsset($"Assets/DialogueManager/save/cache/{graph_file_name}/Elements/Basic/{name}.asset");
                }
            }

            editor_data.Basic_nodes_old_names = new List<string>(current_Basic_Nodes_name);
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
        
        private static void update_choice_connection()
        {
            
            foreach (Basic_Node node in basic_nodes)
            {
                Basic_Node_Save_SO node_container = created_basic_nodes[node.ID];

                for (var choice_index = 0; choice_index < node.choices.Count; choice_index++)
                {
                    var node_choice = node.choices[choice_index];

                    if (string.IsNullOrEmpty(node_container.Id)) continue;

                    if (node_choice.output_port.connected != true) return;

                    foreach (Edge connection in node_choice.output_port.connections)
                    {
                        if (connection.input.GetType().IsSubclassOf(typeof(Basic_Node)))
                        {
                            node_choice.Node_Id = node.ID;
                            node_container.Choices[choice_index].NextSavedBasicNodeSaveSO =
                                created_basic_nodes[node.ID];
                        }
                        else
                        {
                            Control_Node c_node = connection.input.node as Control_Node;

                            node_choice.Node_Id = c_node.ID;
                            node_container.Choices[choice_index].NextSavedControlNodeSO =
                                created_control_node[c_node.ID];
                        }
                    }
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
            if (AssetDatabase.IsValidFolder("Assets/DialogueManager/save") == false)
            {
                AssetDatabase.CreateFolder("Assets/DialogueManager", "save");
            }
            
            if (AssetDatabase.IsValidFolder("Assets/DialogueManager/save/Cache") == false )
            {
                AssetDatabase.CreateFolder("Assets/DialogueManager/save", "cache");
            }
            
            if (AssetDatabase.IsValidFolder("Assets/DialogueManager/save/SavedGraphs") == false)
            {
                AssetDatabase.CreateFolder("Assets/DialogueManager/save" , "SavedGraphs");
            }
            
        }

        private static void Create_Cache_folders()
        {
            if (AssetDatabase.IsValidFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}") == false)
            {
                AssetDatabase.CreateFolder($"Assets/DialogueManager/Save/Cache" , $"{graph_file_name}");
            }

            if (AssetDatabase.IsValidFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements") == false)
            {
                AssetDatabase.CreateFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}" , "Elements");
            }

            if (AssetDatabase.IsValidFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements/basic") == false)
            {
                AssetDatabase.CreateFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements" , "Basic");
            }

            if (AssetDatabase.IsValidFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements/Control") == false)
            {
                AssetDatabase.CreateFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/Elements", "Control");
            }

            if (AssetDatabase.IsValidFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}/recovery") == false)
            {
                AssetDatabase.CreateFolder($"Assets/DialogueManager/Save/Cache/{graph_file_name}", "recovery");
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