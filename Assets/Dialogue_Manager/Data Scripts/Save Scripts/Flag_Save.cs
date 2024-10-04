using System;
using UnityEngine;

namespace DialogueQuest.Data.Save
{
    [Serializable]
    public class Flag_Save
    {
        [field: SerializeField] public string Flag_text { get; set; }
        [field: SerializeField] public string Node_ID { get; set; }
    }
}