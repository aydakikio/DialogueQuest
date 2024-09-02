using System;
using System.Collections.Generic;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.Data
{
    [Serializable]
    public class Basic_Nodes : ScriptableObject
    {
        [field:SerializeField] public string Id { get; set; }
        [field:SerializeField] public string name { get; set; } //Node Name
        [field:SerializeField] public Node_Types type { get; set; } //Node Type
        [field:SerializeField] [field: TextArea() ] public string Dialogue { get; set; } // Dialogue Text
        [field:SerializeField] public List<Flag_Data> Flag_Infos { get; set; }
        [field:SerializeField] public List<Choice_Data> Choices { get; set; }
        public void Instance(string Name , Node_Types Type , string dialogue , List<Flag_Data> flag_infos,List<Choice_Data> choices)
        {
            name = Name;
            type = Type;
            Dialogue = dialogue;
            Flag_Infos = flag_infos;
            Choices = choices;
        }
    }
}