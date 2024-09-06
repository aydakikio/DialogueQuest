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
        
        
        private List<Node_Id_data> Node_IDs = new List<Node_Id_data>(); 
        public IReadOnlyCollection<Node_Id_data> Node_IDs_list => Node_IDs; 
        

        private List<Node_Name_Data> Node_names = new List<Node_Name_Data>();
        public IReadOnlyCollection<Node_Name_Data> Node_names_List => Node_names;
        

        private List<Start_Point_Data> start_points = new List<Start_Point_Data>();
        public IReadOnlyCollection<Start_Point_Data> start_points_list => start_points;
        
        
        #region ID assignment & Name managment
        
        public string Assign_ID()
        {
            Node_Id_data Id_data = new Node_Id_data();
            
            Node_ID = Element_Utilities.Hash(new Guid().ToString());
            Node_ID = Id_data.Id ;
            
            Node_IDs.Add(Id_data);
            
            return Node_ID;
        }

        public void Add_Node_name(Node_Name_Data name_data)
        {
            
            Node_names.Add(name_data);
            
        }
        #endregion

        
        #region Manging Start Points
        public void Add_start_points(Node_Name_Data node_name , string ID)
        {
            Start_Point_Data point_data = new Start_Point_Data();

            ID = point_data.Node_Id;
            node_name = point_data.Node_Name;
            
            start_points.Add(point_data);
        }
        
        #endregion
       
    }
}