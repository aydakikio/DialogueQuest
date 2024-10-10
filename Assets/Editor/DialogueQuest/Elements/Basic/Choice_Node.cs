using System.Collections.Generic;
using System.Linq;
using DialogueQuest.Data;
using DialogueQuest.Enumerations;
using DialogueQuest.Utilities;
using NUnit.Framework;
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
            Choice_Data init_choice = new Choice_Data(){Choice_Text = "New Choice"};
            choices.Add(init_choice);

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

            foreach (Choice_Data choice in choices)
            {
                Port init_choice_port = Create_Choice_Port(choice);

                outputContainer.Add(init_choice_port);
            }

            RefreshExpandedState();
        }

        private Port Create_Choice_Port(object data)
        {
            Port out_put = this.Create_Port(Orientation.Horizontal, Direction.Output , Port.Capacity.Multi);
            out_put.portName = null;
            out_put.userData = data;
            Choice_Data choice = (Choice_Data) data;
            
            
            
            Button choice_delete_button = Element_Utilities.Create_Button("X", () =>
            {
                if (choices.Count == 1) return;
                if (out_put.connected)
                {
                    //out_put.DisconnectAll();
                    if (out_put.connections != null)
                    {
                        graph.DeleteElements(out_put.connections);
                    }
                    
                }
                
                choices.Remove(choice);
                outputContainer.Remove(out_put);
            });

            TextField choice_text_field = Element_Utilities.Create_TextField(null, choice.Choice_Text,callback => { choice.Choice_Text = callback.newValue;});

            choice_text_field.style.minWidth = 20;
            choice_text_field.style.maxWidth = 80;
            
            out_put.Add(choice_text_field);
            out_put.Add(choice_delete_button);
            
            return out_put;
        }
    }
}