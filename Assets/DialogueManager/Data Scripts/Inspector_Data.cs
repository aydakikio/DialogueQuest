using DialogueQuest.scriptable_object;
using UnityEngine;
using UnityEngine.Serialization;

namespace DialogueManager.Data_Scripts
{
    
    public class Inspector_Data : MonoBehaviour
    {
        [SerializeField] private Graph_Container graph_Container;//runtime container
        [SerializeField] private Basic_Node_Save_SO basic_node;
        [SerializeField] private int dialogue_flow_speed;
        [SerializeField] private bool show_as_one_charcter_at_once;
    }
}