using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Graph_Save : ScriptableObject
    {
        [field: SerializeField] public string File_Name { get; set; }
        [field: SerializeField] public List<Basic_Node_Save> Basic_nodes { get; set; }
        [field: SerializeField] public List<Control_Node_Save> control_nodes { get; set; }
        
        [field: SerializeField] public List<string> Basic_nodes_old_names { get; set; }

        public void Initialize( string file_name)
        {
            File_Name = file_name;

            Basic_nodes = new List<Basic_Node_Save>();
            control_nodes = new List<Control_Node_Save>();
            
        }
    }
}