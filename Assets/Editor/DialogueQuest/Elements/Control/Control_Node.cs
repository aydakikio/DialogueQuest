using DialogueQuest.Enumerations;
using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

namespace DialogueQuest.Elements
{
    public class Control_Node : Root_Node
    {
        public Node_Types type { get; set; }
        public string ID { get; set; }
        
        public virtual void Initialize(Vector2 position)
        {
            ID = Root_Node.Assign_ID();
            
            SetPosition(new Rect(position , Vector2.zero ));
        }
        public virtual void Draw()
        {
            Button add_input = Element_Utilities.Create_Button("Add Input" , ()=> Add_Input());
            
            mainContainer.Insert(1,add_input);
        }

        private void Add_Input()
        {
            
            Port input = this.Create_Port( Orientation.Horizontal , Direction.Input,Port.Capacity.Multi);
            input.portName = $"In";
            
            inputContainer.Add(input);
            
            RefreshExpandedState();
            
        }
    }
}