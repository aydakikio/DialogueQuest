using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.UIElements;
using DialogueQuest.Utilities;
using Unity.Plastic.Antlr3.Runtime.Misc;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;

namespace DialogueQuest.Elements
{
    public class Basic_Node : Root_Node
    {
        private Root_Node root = new Root_Node();
        public string Node_name { get; set; }
        public string Dialogue { get; set; }
        private int repeat_time = 1;
        
        #region Flag resourses

           private Foldout Flag_Fold_Out = Element_Utilities.Create_FoldOut("Flag");
        
           private string Flag = "New Flag";
           
           
        #endregion
       
        #region UI and Position 
        
        public virtual void Initialize(Vector2 position)
        {
            Node_name = "New Node";
            
            root.Assign_ID();
            Debug.Log(Node_ID);
            
            Dialogue = "Dialgue Text";
            
            SetPosition(new Rect(position , Vector2.zero));
        }
        public virtual void draw()
        {
            TextField Get_node_name = Element_Utilities.Create_TextField(null,Node_name);
            Get_node_name.MarkDirtyRepaint();
            
            root.Add_Node_name(Node_name);
            
            //Create Drop Down 
            var option_menu = Create_Drop_Down();
            option_menu.RegisterValueChangedCallback(Event =>
            {
                switch (Event.newValue)
                {
                    case "Input":
                        Add_Input();
                        option_menu.value = "ADD ITEM";
                        break;
                    case "Flag":
                        ADD_Flag();
                        option_menu.value = "ADD ITEM";
                        break;
                    case "Animation" :
                        break;
                }
            });
            
            
            //Basic input
            Port Base_input = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Input,Port.Capacity.Multi , $"In({repeat_time})");
            
            //Create Dialogue Box
            VisualElement Dialogue_box = new VisualElement();
            Foldout Dialogue_foldOut = Element_Utilities.Create_FoldOut("Dialogue" , true);
            TextField Dialogue_Field = Element_Utilities.Create_Text_Box(Dialogue);
            
            
            //Insert Node Name Field 
            titleContainer.Insert(0 , Get_node_name);
            titleContainer.Insert(1 , option_menu);
            
            //Insert Input Container's Ports
            inputContainer.Add(Base_input);
            
            //Insert Dialogue Box
            Dialogue_box.Add(Dialogue_Field);
            Dialogue_foldOut.Add(Dialogue_box);
            
            extensionContainer.Add(Dialogue_foldOut);
            
        }
        
        #endregion

        #region Flag

        private void ADD_Flag()
        {
            
            var Flag_Name = Element_Utilities.Create_TextField($"Flag{repeat_time}", Flag);
            Flag_Name.MarkDirtyRepaint();

            Flag_Name.RegisterValueChangedCallback(Event => { Flag = Event.newValue; });

            VisualElement Flag_panel = new VisualElement();

            Flag_panel.Add(Flag_Name);
            Flag_Fold_Out.Add(Flag_panel);

            Flag_Fold_Out.MarkDirtyRepaint();
            extensionContainer.Add(Flag_Fold_Out);
            
            repeat_time++;
        }
        
        #endregion

        
        private void Add_Input()
        {
            Port input = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Input,Port.Capacity.Multi, $"In{repeat_time+1}");
            
            inputContainer.Add(input);
            
            RefreshExpandedState();
            
        }
        
        
        private DropdownField Create_Drop_Down()
        {
            var Drop_Down = new DropdownField(null ,new List<string>{ "ADD ITEM", "Input","Flag"} , 0);
            
            return Drop_Down;
        }
        
    }
}