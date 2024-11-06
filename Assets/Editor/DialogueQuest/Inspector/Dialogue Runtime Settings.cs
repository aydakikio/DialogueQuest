using DialogueManager.Data_Scripts;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using DialogueQuest.Utilities;
using UnityEditor;

namespace Editor.DialogueQuest.Inspector
{
    [CustomEditor(typeof(Inspector_Data))]
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
            
            Draw_Graph_Container_Area();
            Graph_Container current_runtime_data = (Graph_Container)graph_countainer_property.objectReferenceValue;
            if (current_runtime_data is null )
            {
                
                return;
            }
        }

        private void Draw_Graph_Container_Area()
        {
            Inspector_Utility.Draw_title("Graph Container");
            graph_countainer_property.Draw_PropertyField();
            Inspector_Utility.Draw_Space();
        }

        private void Draw_Start_point_filter()
        {
            Inspector_Utility.Draw_title("Filters");
            // var Drop_Down = new DropdownField(null ,new List<string>{ "ADD ITEM", "Input","Flag"} , 0);

            //Drop
        }
    }
}