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

        private List<Start_Point_Data> start_points = new List<Start_Point_Data>();
        public IReadOnlyCollection<Start_Point_Data> start_points_list => start_points;
        
        
        #region ID assignment & Name managment
        
        public string Assign_ID()
        {
            Node_ID = Element_Utilities.Hash(new Guid().ToString());
            
            return Node_ID;
        }
        #endregion

        
        #region Manging Start Points
        public void Add_start_points(string node_name , string ID)
        {
            Start_Point_Data point_data = new Start_Point_Data();

            ID = point_data.Node_Id;
            node_name = point_data.Node_Name;
            
            start_points.Add(point_data);
        }
        
        #endregion
       
    }
}