using System.Collections.Generic;
using UnityEngine.UIElements;
using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueQuest.Elements
{
    public class Basic_Node : Root_Node
    {
        private Root_Node root = new Root_Node();
        public string Node_name { get; set; }
        public string Dialogue { get; set; }
        public void Initialize(Vector2 position)
        {
            
        }
        public virtual void draw()
        {
            TextField Get_node_name = Element_Utilities.Create_TextField("Node Name",Node_name);
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
            
            //Insert Input Container's Ports
            
            inputContainer.Add(Base_input);
            
            //Insert Dialogue Box
            Dialogue_box.Add(Dialogue_Field);
            Dialogue_foldOut.Add(Dialogue_box);
            
            extensionContainer.Add(Dialogue_foldOut);
            
        }

        private void Add_Input()
        {
            Port input = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Input,Port.Capacity.Multi);
            inputContainer.Add(input);
            
            RefreshExpandedState();
        }
        
        
        private DropdownField Create_Drop_Down(string add_addition_item = null , int index = -1 )
        {
            var Drop_Down = new DropdownField(null ,new List<string>{ "ADD ITEM", "Input"} , 0);

            if (add_addition_item != null && index >= 0 )
            {
                Drop_Down.choices.Insert(index , add_addition_item);
            }
            
            return Drop_Down;
        }
    }
}