using System;
using System.Collections.Generic;
using DialogueQuest.Data;
using DialogueQuest.Data.Save;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.scriptable_object
{
    [Serializable]
    public class Basic_Node_Save_SO : ScriptableObject
    {
        [field:SerializeField] public string Id { get; set; }
        [field:SerializeField] public string name { get; set; } //Node Name
        [field:SerializeField] public Node_Types type { get; set; } //Node Type
        [field:SerializeField] [field: TextArea() ] public string Dialogue { get; set; } // Dialogue Text
        [field:SerializeField] public List<Flag_Data> Flag_Infos { get; set; }
        [field:SerializeField] public List<Choice_Data> Choices { get; set; }
        [field:SerializeField]public Vector2 Node_Position { get; set; }
        public void Initialize(string Name , Node_Types Type , string dialogue , List<Flag_Data> flag_infos,List<Choice_Data> choices , Vector2 position)
        {
            name = Name;
            type = Type;
            Dialogue = dialogue;
            Flag_Infos = flag_infos;
            Choices = choices;
            Node_Position = position;
        }
    }
}