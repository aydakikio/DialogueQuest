using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueQuest.scriptable_object
{
    public class Graph_Container : ScriptableObject
    {
        [field: SerializeField ] public string File_name { get; set;  }
        [field: SerializeField ] public List<Basic_Nodes> graph_nodes { get; set; } 
        
        public void Initialize(string file_name)
        {
            File_name = file_name;
            
            graph_nodes = new List<Basic_Nodes>();
        }

        public List<string> Get_Graph_Nodes_Names()
        {
            List<string> Node_names = new List<string>();

            foreach (var node in graph_nodes)
            {
                if (node.name != null)
                {
                    continue;
                }
                
                graph_nodes.Add(node);
            }
            
            return Node_names;
        }

    }
}