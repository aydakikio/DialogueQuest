using System;
using UnityEngine;

namespace DialogueQuest.Data
{
    [Serializable]
    public class Animation_Data
    {
        //Needs to complete
        [field:SerializeField] public GameObject Object { get; set; }
        [field:SerializeField] public string animation_name { get; set; }
    }
}