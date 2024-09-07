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
            //Be completed in future 
        }

        public void Remove_start_points()
        {
            
        }
        
        #endregion
       
    }
}