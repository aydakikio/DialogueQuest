using System;
using System.Collections.Generic;
using Dialogue_Quest.Window;
using DialogueQuest.Data;
using DialogueQuest.Data.Save;
using DialogueQuest.Enumerations;
using UnityEngine.UIElements;
using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueQuest.Elements
{
    public class Basic_Node : Root_Node
    {
        public string Node_name { get; set; }
        public string Dialogue { get; set; }
        public Node_Types type { get; set; }
        public string ID { get; set; }
        public List<Choice_Save> choices { get; set; }
        
        
        private int Flag_Count_num = 1;
        
        private int input_count_num = 1;

        protected Graph_View graph;
        
        #region Flag resourses

           private Foldout Flag_Fold_Out = Element_Utilities.Create_FoldOut("Flag");
            
           private string Flag = "New Flag";

           public List<Flag_Save> Flags { get; set; }
           
        #endregion
       
        #region UI and Position 
        
        public virtual void Initialize(Vector2 position)
        {
            Node_name = "New Node";
            
            ID = Root_Node.Assign_ID();
            choices = new List<Choice_Save>();
            Flags = new List<Flag_Save>();
            
            Dialogue = "Dialgue Text";
            
            SetPosition(new Rect(position , Vector2.zero));
            
        }
        public virtual void draw()
        {
            TextField Get_node_name = Element_Utilities.Create_TextField(null,Node_name , callback =>
            {
                TextField target = (TextField)callback.target;
                target.value = callback.newValue;

                Node_name = target.value;

            });
            Get_node_name.MarkDirtyRepaint();
            
            
            
                      
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
                }
            });
            
            
            //Basic input
            Port Base_input = this.Create_Port(Orientation.Horizontal , Direction.Input,Port.Capacity.Multi );
            Base_input.portName = $"In {input_count_num} ";
            
            //Create Dialogue Box
            VisualElement Dialogue_box = new VisualElement();
            Foldout Dialogue_foldOut = Element_Utilities.Create_FoldOut("Dialogue" , true);
            TextField Dialogue_Field = Element_Utilities.Create_Text_Box(Dialogue);
            
            
            //Insert Node Name Field 
            titleContainer.Insert(0 , Get_node_name);
            titleButtonContainer.Insert(0, option_menu);
            
            //Insert Input Container's Ports
            inputContainer.Add(Base_input);
            
            //Insert Dialogue Box
            Dialogue_box.Add(Dialogue_Field);
            Dialogue_foldOut.Add(Dialogue_box);
            
            
            extensionContainer.Add(Dialogue_foldOut);
            
            RefreshExpandedState();
        }
        
        private DropdownField Create_Drop_Down()
        {
            var Drop_Down = new DropdownField(null ,new List<string>{ "ADD ITEM", "Input","Flag"} , 0);
            
            return Drop_Down;
        }
        
        #endregion

        #region Flag

        private void ADD_Flag()
        {
            Flag_Save flag = new Flag_Save();
            Flags.Add(flag);
            
            var Flag_Name = Element_Utilities.Create_TextField($"Flag{Flag_Count_num}", Flag);
            Flag_Name.MarkDirtyRepaint();

            Flag_Name.RegisterValueChangedCallback(Event => { flag.Flag_text = Event.newValue; });

            Button delete_flag = Element_Utilities.Create_Button("X" , () => Delete_Flag(flag));

            VisualElement Flag_panel = new VisualElement();
            
            Flag_panel.Insert(0, delete_flag) ;//Beta
            Flag_panel.Insert(1, Flag_Name);
            Flag_Fold_Out.Add(Flag_panel);

            Flag_Fold_Out.MarkDirtyRepaint();
            extensionContainer.Add(Flag_Fold_Out);
            
            Flag_Count_num++;
        }

        private void Delete_Flag(Flag_Save flag_item)
        {
            if (Flags.Count <= 1 )
            {
                extensionContainer.Remove(Flag_Fold_Out);
                
            }

            Flags.Remove(flag_item);
        }
        
        #endregion

        #region Port Management

        
        private void Add_Input()
        {
            input_count_num++;
            Port input = this.Create_Port( Orientation.Horizontal , Direction.Input,Port.Capacity.Single);
            input.portName = $"In {input_count_num} ";
            
            inputContainer.Add(input);
            
            RefreshExpandedState();

        }

        public void Disconnect_All_Ports()
        {
            Disconnect_Inputs();
            Disconnect_Outputs();
        }

        private void Disconnect_Inputs()
        {
            Disconnect_Ports(inputContainer);
        }

        private void Disconnect_Outputs()
        {
            Disconnect_Ports(outputContainer);
        }

        private void Disconnect_Ports(VisualElement port_Container)
        {
            foreach (Port port in port_Container.Children())
            {
                if (!port.connected)
                {
                    continue;
                }
                graph.DeleteElements(port.connections);
                
            }
        }
        
        #endregion
        
        
    }
}