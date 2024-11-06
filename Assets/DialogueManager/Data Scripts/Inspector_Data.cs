using DialogueQuest.scriptable_object;
using UnityEngine;
using UnityEngine.Serialization;

namespace DialogueManager.Data_Scripts
{
    
    public class Inspector_Data : MonoBehaviour
    {
        [SerializeField] private Graph_Container graph_Container;//runtime container
        [SerializeField] private Basic_Node_Save_SO basic_node;
        
    }
}