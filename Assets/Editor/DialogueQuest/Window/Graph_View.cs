using System;
using System.Collections.Generic;
using System.Linq;
using DialogueQuest;
using DialogueQuest.Data.Save;
using DialogueQuest.Elements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using DialogueQuest.Enumerations;
using DialogueQuest.scriptable_object;
using UnityEditor;

namespace Dialogue_Quest.Window
{
    public class Graph_View : GraphView
    {
        //Dictionary of start points 
        private Dictionary<string, Start_Node_Save> start_nodes;
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
            this.AddManipulator(Create_Contextal_Menu("Add Node (Quit Node)" ,"control" , Node_Types.Quit_Node));
            
            //Set other manipulators
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new ContentDragger());
        }

        #region Node Creation & Menu Manipulators
        
        private IManipulator Create_Contextal_Menu(string action_title ,string parent_node_type,Node_Types Node_type )
        {
            ContextualMenuManipulator Menu = new ContextualMenuManipulator(Event => Event.menu.AppendAction(action_title ,action_Event => AddElement(Create_Node( action_Event.eventInfo.localMousePosition, parent_node_type , Node_type))));
            return Menu;
        }
        public Node Create_Node(Vector2 position , string parnent_node_type , Node_Types Node_type)
        {
            //For Control nodes
            if (parnent_node_type.ToLower() == "control")
            {
                Type control_node_type = Type.GetType($"DialogueQuest.Elements.{Node_type}");

                Control_Node control_node = (Control_Node)Activator.CreateInstance(control_node_type);
                control_node.Initialize(position);
                control_node.Draw();

                return control_node;
            }
            
            // For Basic nodes
            
            Type basic_node_type = Type.GetType($"DialogueQuest.Elements.{Node_type}");
            
            Basic_Node basicNode = (Basic_Node)Activator.CreateInstance(basic_node_type);
                
            basicNode.Initialize(position);
            basicNode.draw();
                
            return basicNode;
        }
        
        #endregion
        
        #region Port Check & Port Delete

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



        #region Graph Elements Remove
        
        
        private void On_Graph_Elements_Delete()
        {
             
            deleteSelection = (operationName, askUser) =>
            {
                Type edge_type = typeof(Edge);
                
                List<Basic_Node> Basic_Nodes_To_Delete = new List<Basic_Node>();
                List<Edge> Edges_To_Delete = new List<Edge>();

                foreach (GraphElement selected_Element in selection)
                {
                    if (selected_Element is Basic_Node node)//Make it Sepearate for Basic Nodes
                    {
                        Basic_Nodes_To_Delete.Add(node);
                        
                        continue;
                    }

                    if (selected_Element.GetType() == typeof(Edge))
                    {
                        Edge edge = (Edge)selected_Element;
                        
                        Edges_To_Delete.Add(edge);
                        
                        continue;
                    }
                    
                }
                
                DeleteElements(Edges_To_Delete);
                
                foreach (Basic_Node node_To_remove in Basic_Nodes_To_Delete)
                {
                    Remove_Basic_Node(node_To_remove);
                    node_To_remove.Disconnect_All_Ports();
                    
                    Remove(node_To_remove);
                }
                
               
            };
        }

        private void Remove_Basic_Node(Basic_Node node)
        {
            //string node_name = node.Node_name.ToLower();
            //graph_base_nodes.Remove(node);
            
            
        }
        
        #endregion

        #region Graph search  --> /!\ Caution: Disabled due to problems! !!!!!!
        
        /*
        public List<Basic_Node> Search_The_Graph(string value)
        {
            List<Basic_Node> found_basic_nodes = new List<Basic_Node>();
            found_basic_nodes.Clear();
            
            foreach (Basic_Node node in this.nodes )
            {
                if (node.Node_name.Contains(value) == true)
                {
                    found_basic_nodes.Add(node);
                }
            }

            if (found_basic_nodes.Count != 0)
            {
                return found_basic_nodes;
            }
            
            
            Debug.Log($" Your Search - {value} - did not match any node name ");

            return null;
        }

        public void zoom_in_found_node(Node node)
        {
            FrameAll();
            Vector3 xx = node.GetPosition().center;
            UpdateViewTransform( xx , Vector3.one );
        }
        
        */
        
        #endregion
        
        #region Start Point Management

        private void get_selection_of_nodes()
        {/*
            if (this.selection != null)
            {
                foreach (Basic_Node node in this.selection)
                {
                    ContextualMenuManipulator Menu = new ContextualMenuManipulator(Event => Event.menu.AppendAction());
                    //this.AddManipulator();
                }
            }
            */
        }
        private void Manage_Start_Points(int mode,List<Basic_Node> selected_base_nodes)
        {
            //Only basic nodes can be considered  as start point !
            switch (mode)
            {
                case 0 : //Adds to list of start points
                    foreach (Basic_Node node_To_Add in selected_base_nodes)
                    {
                        Start_Node_Save start_node_container = new Start_Node_Save()
                            { Node_ID = node_To_Add.ID, type = node_To_Add.type , position = node_To_Add.GetPosition().position };
                        //Changes style to costume style
                        start_nodes.Add(node_To_Add.ID , start_node_container);
                    }
                    break;
                case 1: //Removes from list of start points
                    foreach (var node_To_Delete in selected_base_nodes)
                    {
                        if (start_nodes.ContainsKey(node_To_Delete.ID))
                        {
                            start_nodes.Remove(node_To_Delete.ID);
                            
                            //Reset style to default
                        }
                    }
                    break;
            } 
        }

        #endregion
        
        
        #region Other

        public string get_total_nodes_number()
        {
            return this.nodes.Count().ToString();
        }
        
        
        #endregion
        
        
    }
}

