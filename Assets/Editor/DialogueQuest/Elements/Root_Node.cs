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
        
        protected static string Assign_ID()
        {
            string Node_ID = Element_Utilities.Hash(new Guid().ToString());
            
            return Node_ID;
        }
    }
}