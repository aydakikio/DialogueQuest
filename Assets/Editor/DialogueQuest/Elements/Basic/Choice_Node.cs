using DialogueQuest.Data;
using DialogueQuest.Enumerations;
using DialogueQuest.Utilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace DialogueQuest.Elements
{
    public class Choice_Node : Basic_Node
    {

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);


            type = Node_Types.Choice_Node;
        }

        public override void draw()
        {
            base.draw();

            Button Add_Choice = Element_Utilities.Create_Button("Add Choice" , () =>
            {
                Choice_Data data = new Choice_Data() { Choice_Text = "New Choice" };
                choices.Add(data);
                Port port = Create_Choice_Port(data);
                outputContainer.Add(port);
            });
            
            mainContainer.Insert(1 , Add_Choice);
            
            RefreshExpandedState();
        }

        private Port Create_Choice_Port(object data)
        {
            Port out_put = this.Create_Port(Orientation.Horizontal, Direction.Output , Port.Capacity.Multi);
            out_put.userData = data;
            Choice_Data choice = (Choice_Data)  data;
            
            Button choice_delete_button = Element_Utilities.Create_Button("X", () =>
            {
                if (choices.Count == 1) return;
                if (out_put.connected) foreach (var connection in out_put.connections) out_put.Disconnect(connection);
                
                choices.Remove(choice);
                graph.RemoveElement(out_put);
            });

            TextField choice_text_field = Element_Utilities.Create_TextField("", "New Choice",
                callback => { choice.Choice_Text = callback.newValue; });
            
            
            out_put.Insert(0, choice_delete_button);
            out_put.Insert(1,choice_text_field);
            out_put.Insert(2 , out_put);
            
            return out_put;
        }
    }
}