using System;
using System.Collections.Generic;
using DialogueQuest.Data;
using UnityEditor.Experimental.GraphView;
using DialogueQuest.Utilities;
using UnityEditor;

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
        
       
    }
}