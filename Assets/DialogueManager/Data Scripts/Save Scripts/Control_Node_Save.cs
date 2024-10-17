using System;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Control_Node_Save
    {
        public string ID { get; set; }
        public Node_Types type { get; set; }
        public Vector2 Position { get; set; }
    }
}