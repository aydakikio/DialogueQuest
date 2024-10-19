using System;
using DialogueQuest.Enumerations;
using UnityEngine;

namespace DialogueQuest.scriptable_object
{
    [Serializable]
    public class Control_Node_Save_SO : ScriptableObject
    {
        [field: SerializeField] public string ID { get; set; }
        [field: SerializeField] public Node_Types type { get; set; }
        [field: SerializeField] public Vector2 position { get; set; }

        public void Initialize(string id ,Node_Types Type , Vector2 Node_position)
        {
            ID = id;
            type = Type;
            position = Node_position;
        }
    }
}