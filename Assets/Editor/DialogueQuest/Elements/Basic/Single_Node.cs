using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;


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
            
            Port base_output = Element_Utilities.Create_Port(this , Orientation.Horizontal , Direction.Output , Port.Capacity.Multi);
            base_output.portName = $"Out {time} ";

            Button add_Output = Element_Utilities.Create_Button("Add Output", () => Add_Output());
            
            outputContainer.Add(base_output);
            mainContainer.Insert(1, add_Output);
        }

         private void Add_Output()
         {
             time++;
            Port output = Element_Utilities.Create_Port(this, Orientation.Horizontal, Direction.Output, Port.Capacity.Multi);
            output.portName = $"Out{time} ";
            
            outputContainer.Add(output);
            
            RefreshExpandedState();
        }
    }
}