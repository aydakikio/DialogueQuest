using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Choice_Save
    {
        [field: SerializeField] public string Choice_Text { get; set; }
        [field: SerializeField] public Port output_port { get; set; }
        [field: SerializeField] public string Node_Id { get; set; }
    }
}