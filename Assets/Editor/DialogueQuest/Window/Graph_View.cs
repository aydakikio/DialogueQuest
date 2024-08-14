using System;
using System.Collections;
using System.Collections.Generic;
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
            ADD_Grid_Background();
            Add_Graph_Styles();
            Add_Maniplators();
        }

        private void ADD_Grid_Background()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0 , gridBackground );
        }

        private void Add_Maniplators()
        {
            this.AddManipulator(new ContentDragger());
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
            
            //Create ADD Node Menu
            this.AddManipulator(Create_Contextal_Menu("Add Node (Single Node) " , "Basic" , Node_Types.Single_Node));

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
        private Node Create_Node(Vector2 position , string parnent_node_type , Node_Types Node_type){
            
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

        #region UI

        private void Add_Grid_Background()
        {
            GridBackground background = new GridBackground();
            background.StretchToParentSize();
            Insert(0,background);
        }

        private void Add_Graph_Styles()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("Assets/Editor Default Resources/UI variables/Graph_style.uss");
            styleSheets.Add(styleSheet);
        }
        
        #endregion
    }
}

