using System;
using System.Collections.Generic;
using DialogueQuest.Data.Save;
using UnityEngine;

namespace DialogueQuest.scriptable_object
{
    public class Graph_Container : ScriptableObject
    {
        [field: SerializeField ] public string File_name { get; set;  }
        [field: SerializeField ] public List<Basic_Node_Save_SO> graph_basic_nodes { get; set; } 
        [field: SerializeField] public List<Control_Node_Save_SO> graph_control_nodes { get; set; }
        [field: SerializeField] public Dictionary<string, Basic_Node_Save_SO> graph_start_points;
        
        public void Initialize(string file_name)
        {
            File_name = file_name;
            
            graph_basic_nodes = new List<Basic_Node_Save_SO>();
            graph_control_nodes = new List<Control_Node_Save_SO>();
            graph_start_points = new Dictionary<string, Basic_Node_Save_SO>();
        }

        public List<string> Get_Graph_Nodes_Names()
        {
            List<string> Node_names = new List<string>();

            foreach (var node in graph_basic_nodes)
            {
                if (node.name != null)
                {
                    continue;
                }
                
                graph_basic_nodes.Add(node);
            }
            
            return Node_names;
        }

    }
}