using System.Collections.Generic;
using UnityEngine.UIElements;
using DialogueQuest.Utilities;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
           private List<string> Flags = new List<string>();
           public IReadOnlyCollection<string> Flags_List => Flags;
           
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
                }
            });
            
            
            //Basic input
            Port Base_input = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Input,Port.Capacity.Multi);
            
            //Create Dialogue Box
            VisualElement Dialogue_box = new VisualElement();
            Foldout Dialogue_foldOut = Element_Utilities.Create_FoldOut("Dialogue;");
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
               if (repeat_time % 3 ==0 )
               {
                   foreach (string item in Flags_List)
                   {
                       Debug.Log(item);
                   }
               }
               
               TextField Flag_Name = Element_Utilities.Create_TextField($"Flag{repeat_time}" , Flag );
               Flag_Name.MarkDirtyRepaint();
               Flag_Name.RegisterValueChangedCallback(Event =>
               {
                   //The next line should run at 1s if there is no new input 
                    Flags.Add(Flag_Name.value);
               });
               
               
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
            Port input = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Input,Port.Capacity.Multi);
            inputContainer.Add(input);
            
            RefreshExpandedState();
        }
        
        
        private DropdownField Create_Drop_Down(string add_addition_item = null , int index = -1 )
        {
            var Drop_Down = new DropdownField(null ,new List<string>{ "ADD ITEM", "Input","Flag"} , 0);

            if (add_addition_item != null && index >= 0 )
            {
                Drop_Down.choices.Insert(index , add_addition_item);
            }
            
            return Drop_Down;
        }
    }
}