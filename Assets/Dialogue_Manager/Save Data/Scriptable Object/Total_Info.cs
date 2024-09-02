using System;
using System.Collections.Generic;
using UnityEngine;

namespace DialogueQuest.Data
{
    [Serializable]
    public class Total_Info : ScriptableObject
    {
        [field:SerializeField] public List<string> IDs { get; set; }
        [field:SerializeField] public List<Node_Name_Data> Node_Names { get; set; }
        [field:SerializeField] public List<string> Start_points { get; set; } //stores start point's ID

        public void Instance(List<string> ids , List<Node_Name_Data> names , List<string> start_points)
        {
            IDs = ids;
            Node_Names = names;
            Start_points = start_points;
        }
    }
}