using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Graph_Save : ScriptableObject
    {
        [field: SerializeField] public string File_Name { get; set; }
        [field: SerializeField] public List<Basic_Node_Save> Basic_nodes { get; set; }
        [field: SerializeField] public List<Control_Node_Save> control_nodes { get; set; }
        [field: SerializeField] public Dictionary<string, Basic_Node_Save> start_points { get; set; }
        
        [field: SerializeField] public List<string> Basic_nodes_old_names { get; set; }
        
        [field: SerializeField] public List<Edge> graph_edges { get; set; }

        public void Initialize( string file_name)
        {
            File_Name = file_name;

            Basic_nodes = new List<Basic_Node_Save>();
            control_nodes = new List<Control_Node_Save>();

            graph_edges = new List<Edge>();
        }
    }
}