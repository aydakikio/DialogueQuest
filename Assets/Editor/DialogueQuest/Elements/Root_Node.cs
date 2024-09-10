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

        #region Accessing And Manging Graph Statics

        public void get_name(string graph_name , string temp_name = null)
        {
            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/{graph_name}") == true && temp_name == null)
            {
                Connect_to_static(graph_name);
                return;
            }
            
            Connect_to_static(temp_name);
        }

        private void Connect_to_static(string file_name)
        {
            //var static = new Sqlit
        }

        public void Insert_Node_name()
        {
            
        }

        public void Insert_Node_Id()
        {
            
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