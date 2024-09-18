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
        private Node_Types node_type = Node_Types.Choice_Node;

        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);
            type = Node_Types.Choice_Node;
        }

        public override void draw()
        {
            base.draw();

            Button Add_Choice = Element_Utilities.Create_Button("Add Choice" , () => Create_Choice_Port());
            
            mainContainer.Insert(1 , Add_Choice);
        }

        private void Create_Choice_Port()
        {
            Choice_Data choice_data = new Choice_Data(){Choice_Text = "New Choice"};
            choices.Add(choice_data);
            
            VisualElement choice_panel = new VisualElement();
            
            Port out_put = this.Create_Port(Orientation.Horizontal, Direction.Output , Port.Capacity.Multi);

            Button choice_delete_button = Element_Utilities.Create_Button("X", ()=> {delete_choice_port(out_put, choice_panel , choice_data ); });

            TextField choice_text_field = Element_Utilities.Create_TextField("", "New Choice",
                callback => { choice_data.Choice_Text = callback.newValue; });
            
            
            choice_panel.Insert(0, choice_delete_button);
            choice_panel.Insert(1,choice_text_field);
            choice_panel.Insert(2 , out_put);
            
            outputContainer.Add(choice_panel);

            RefreshExpandedState();
        }

        private void delete_choice_port(Port port , VisualElement panel , Choice_Data data)
        {
            if (choices.Count == 1) return;
            if (port.connected)  graph.DeleteElements(port.connections);
            
            choices.Remove(data);
            outputContainer.Remove(panel);
        }
    }
}