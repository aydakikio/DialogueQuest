using System.Collections.Generic;
using Dialogue_Quest.Window;
using DialogueQuest.Data;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using UnityEditor;

namespace DialogueQuest.Utilities
{
    public static class Save_Utilities
    {
        private static Graph_View graph; 
        
        private static List<Basic_Node> basic_nodes;
        private static List<Control_Base_Node> control_nodes;

        

        #region Main functions
        
        public static void Save()
        {
            
            Create_Save_Folder();
            Get_Graph_Elements();
            save_basic_nodes();
        }

        public static void Load()
        {
            
        }

        #endregion

        #region Save Folder Management

        private static void Create_Save_Folder()
        {
            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save"))
            {
                return;
            }
            
            AssetDatabase.CreateFolder("Assets/Dialogue_Manager/" , "Save" );
        }

        #endregion
        
        
        private static void save_basic_nodes()
        {
            foreach (Basic_Node node in basic_nodes)
            {
                Basic_Nodes node_container = new Basic_Nodes();
                
                node_container.Id = node.ID;
                node_container.name = node.Node_name;
                node_container.type = node.type;
                
                node_container.Dialogue = node.Dialogue;
                
                node_container.Flag_Infos = node.Flags as List<Flag_Data>;
                node_container.Choices = Save_Choices(node.choices);
                
                node_container.Node_Position = node.GetPosition().position;
                
            }            
        }

        private static List<Choice_Data> Save_Choices(List<Choice_Data> current_choices)
        {
            List<Choice_Data> choices = new List<Choice_Data>();

            foreach (Choice_Data choice in current_choices)
            {
                Choice_Data choice_data = new Choice_Data() { Choice_Text = choice.Choice_Text };
                
                choices.Add(choice_data);
            }

            return choices;
        }

        private static void Get_Graph_Elements()
        {
            graph.graphElements.ForEach(element =>
            {
                if (element is Basic_Node base_node)
                {
                    basic_nodes.Add(base_node);

                    return;
                }

                if (element is Control_Base_Node control_node)
                {
                    control_nodes.Add(control_node);
                    
                    return;
                }
                
            } );
        }
    }
}