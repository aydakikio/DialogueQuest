using System;
using System.Collections.Generic;
using DialogueQuest;
using DialogueQuest.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using DialogueQuest.Enumerations;
using UnityEditor;

namespace Dialogue_Quest.Window
{
    public class Graph_View : GraphView
    {
        
        public Graph_View()
        {
            Add_Grid_Background();
            Add_Graph_Styles();
            Add_Maniplators();
        }
        
        private void Add_Maniplators()
        {
            this.AddManipulator(new ContentDragger());
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            //Create ADD Node Menu
            this.AddManipulator(Create_Contextal_Menu("Add Node (Single Node) " , "Basic" , Node_Types.Single_Node));
            this.AddManipulator(Create_Contextal_Menu("Add Node (Choice Node) " , "Basic" , Node_Types.Choice_Node));
            
            
            //Set other manipulators
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new SelectionDragger());
        }

        #region Node Creation & Menu Manipulators
        
        private IManipulator Create_Contextal_Menu(string action_title ,string parent_node_type,Node_Types Node_type )
        {
            ContextualMenuManipulator Menu = new ContextualMenuManipulator(Event => Event.menu.AppendAction(action_title ,action_Event => AddElement(Create_Node( action_Event.eventInfo.localMousePosition, parent_node_type , Node_type))));
            return Menu;
        }
        private Node Create_Node(Vector2 position , string parnent_node_type , Node_Types Node_type)
        {

            
            //For special nodes
            if (parnent_node_type.ToLower() == "special")
            {
                
            }
            
            // For Basic nodes
            
            Type basic_node_type = Type.GetType($"DialogueQuest.Elements.{Node_type}");
            
            Basic_Node basicNode = (Basic_Node)Activator.CreateInstance(basic_node_type);
                
            basicNode.Initialize(position);
            basicNode.draw();
                
            return basicNode;
        }
        
        #endregion
        
        #region Port Check

        public override List<Port> GetCompatiblePorts(Port Start_Port, NodeAdapter Node_Adapter)
        {
            List<Port> Compatible_ports = new List<Port>();
            ports.ForEach(port => { 
                
                if(Start_Port == port)
                {

                    return;
                } 
                if(Start_Port.node == port.node)
                {
                    return;
                }
                if(Start_Port.direction == port.direction)
                {
                    return;
                }
                
                Compatible_ports.Add(port);
                
            });
            
            return Compatible_ports;
            
        }

        #endregion
        
        
        #region UI

        private void Add_Grid_Background()
        {
            GridBackground background = new GridBackground();
            background.StretchToParentSize();
            Insert(0,background);
        }

        private void Add_Graph_Styles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/UIVariables/Graph_style.uss");
            styleSheets.Add(styleSheet);
            
        }
        
        #endregion

        #region Other

        public string get_total_nodes_number()
        { 
            return this.nodes.ToString();//Beta
        }

        private void Search_the_graph()
        {
            //this.CollectElements();
            this.Fi
        }
        #endregion
    }
}

