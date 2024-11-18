using System.Collections.Generic;
using DialogueQuest.scriptable_object;

namespace DialogueManager.Runtime_Scripts
{
    public class Dialogue_Decoder
    {
        private static Graph_Container inspector_countainer;//needs a better name
        private static List<string> Dialogues_in_order;
        private static List<Basic_Node_Save_SO> node_data_in_order;  
        


        private static void Inisalize(Graph_Container runtime_info)
        {
            inspector_countainer = runtime_info;
            Dialogues_in_order = new List<string>();
            node_data_in_order = new List<Basic_Node_Save_SO>();
        }

        private static void Decode()
        {
            
        }

        private void decode_dialogues()
        {
            
        }

        private void decode_Flags()//use later in control flow parts
        {
            
        }
        
        
    }
}