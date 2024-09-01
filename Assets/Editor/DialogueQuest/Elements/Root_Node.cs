using System;
using System.Collections.Generic;
using DialogueQuest.Data;
using UnityEditor.Experimental.GraphView;
using DialogueQuest.Utilities;

namespace DialogueQuest.Elements
{
    public class Root_Node : Node
    {
        private string Node_ID { get; set; }
        
        private List<string> Node_IDs = new List<string>();
        public IReadOnlyCollection<string> Node_IDs_list => Node_IDs;

        private List<Node_Name_Data> Node_names = new List<Node_Name_Data>();
        public IReadOnlyCollection<Node_Name_Data> Node_names_List => Node_names;

        private List<string> start_points = new List<string>();
        public IReadOnlyCollection<string> start_points_list => start_points;
        private Stack<string> temp_values;

        #region ID assignment & Name managment
        
        public string Assign_ID()
        {
            
            Node_ID = Element_Utilities.Hash(new Guid().ToString());
            Node_IDs.Add(Node_ID);

            return Node_ID;
        }

        public void Add_Node_name(Node_Name_Data name_data)
        {
            
            Node_names.Add(name_data);
            
        }
        #endregion

        
        #region Manging Start Points
        public void Add_start_points(string node_name)
        {
            start_points.Add(node_name);
        }
        
        #endregion
       
    }
}