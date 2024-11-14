using System.Collections.Generic;
using System.Linq;
using DialogueManager.Data_Scripts;
using DialogueQuest.Elements;
using DialogueQuest.scriptable_object;
using DialogueQuest.Utilities;
using UnityEditor;
using UnityEngine.UIElements;

namespace Editor.DialogueQuest.Inspector
{
    [CustomEditor(typeof(Inspector_Data))]
    public class Dialogue_Runtime_Settings : UnityEditor.Editor
    {
        private SerializedProperty graph_countainer_property;
        private SerializedProperty  selected_Startpoint_index;
        

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

        private void Draw_Start_point_Area( Graph_Container runtime_data)
        {
            Inspector_Utility.Draw_title("Start Point ");

            string[] start_points_name = runtime_data.graph_start_points.Values.Select(node => node.name).ToArray();
            
            int old_StartPoint_Index = selected_Startpoint_index.intValue;

            int selected_point_index = 0; //needs to update 

            string selected_point_name = start_points_name[selected_point_index];
            
            Basic_Node_Save_SO se = (Basic_Node_Save_SO)Save_Utilities.Load_Asset<Basic_Node_Save_SO>($"Assets/DialogueManager/Save/Cache/{runtime_data.File_name}/Elements/basic" , selected_point_name );
        }
    }
}