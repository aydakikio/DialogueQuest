using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using UnityEditor;

namespace Editor.DialogueQuest.Inspector
{
    [CustomEditor(typeof(Root_Node))]
    public class Dialogue_Runtime_Settings : UnityEditor.Editor
    {
        private SerializedProperty graph_countainer_property;

        private void OnEnable()
        {
            graph_countainer_property = serializedObject.FindProperty("Graph_Container");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
        }
    }
}