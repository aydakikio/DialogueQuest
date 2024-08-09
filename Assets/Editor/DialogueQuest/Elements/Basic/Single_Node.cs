using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueQuest.Elements
{
    public class Single_Node : Basic_Node {
        
        private int time = 1;
        
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);
        }

        public override void draw()
        {
            base.draw();
            
            Port base_output = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Output , Port.Capacity.Multi , $"Out({time})");
            
            outputContainer.Add(base_output);
        }

        private void Add_Output()
        {
            Port output = Element_Utilities.Create_Port(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Multi,$"Out({time+1 })");
            output.title = $"Out{time}";
            
            outputContainer.Add(output);
            
            RefreshExpandedState();
        }
    }
}