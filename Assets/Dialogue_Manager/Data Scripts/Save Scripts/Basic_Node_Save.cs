using System;
using System.Collections.Generic;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Basic_Node_Save
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public string Name { get; set; }
        
        [field: SerializeField] public Node_Types type { get; set; }
        
        [field: SerializeField] public string Dialogue { get; set; }
        [field: SerializeField] public List<Flag_Save> flags { get; set; }
        
        [field: SerializeField] public List<Choice_Save> choices { get; set; }
        
        [field: SerializeField] public Vector2 position { get; set; }
    }
}