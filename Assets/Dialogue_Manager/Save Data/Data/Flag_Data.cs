using System;
using UnityEngine;

namespace DialogueQuest.Data
{
    [Serializable]
    public class Flag_Data
    {
        [field:SerializeField] public string Flag_text { get; set; }
        [field:SerializeField] public  Basic_Nodes Next_Node { get; set; }
    }
}