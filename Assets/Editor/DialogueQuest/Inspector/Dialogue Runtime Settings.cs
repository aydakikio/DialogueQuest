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

        private Basic_Node_Save_SO selcted_start_point;

        private int old_StartPoint_Index;
        

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
            Draw_Start_point_Area(current_runtime_data);
            
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

            string[] start_points_names = runtime_data.graph_start_points.Values.Select(node => node.name).ToArray();
            
            old_StartPoint_Index = selected_Startpoint_index.intValue = 0;
            
            int selected_point_index = Inspector_Utility.Draw_Dropdown_field("Start Point", 0, start_points_names);

            string selected_point_name = start_points_names[selected_point_index];
            
             selcted_start_point = (Basic_Node_Save_SO)Save_Utilities.Load_Asset<Basic_Node_Save_SO>($"Assets/DialogueManager/Save/Cache/{runtime_data.File_name}/Elements/basic" , selected_point_name );
             
             Inspector_Utility.Draw_Space();
        }

        private void Draw_dialogue_show_settings()
        {
             Inspector_Utility.Draw_title("Dialogue  Runtime Settings");
             
             
             
             Inspector_Utility.Draw_Space();
        }

        private void Draw_dialogue_flow_area()
        {
            //The adjustant for showing dialogues as one charcter at one time 
        }
        
    }
}