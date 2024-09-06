using System;
using UnityEngine;

namespace DialogueQuest.Data
{
    [Serializable]
    public class Start_Point_Data
    {
        [field:SerializeField] public string Node_Id { get; set; }
        [field:SerializeField] public Node_Name_Data Node_Name { get; set; }
    }
}