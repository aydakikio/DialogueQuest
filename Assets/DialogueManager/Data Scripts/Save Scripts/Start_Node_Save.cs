using System;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Start_Node_Save
    {
        public string Node_ID { get; set; }
        public Node_Types type { get; set; }
        
        public Vector2 position { get; set; }
    }
}