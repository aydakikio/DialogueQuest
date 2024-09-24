using UnityEditor;

namespace DialogueQuest.Utilities
{
    public static class Save_Utilities
    {
        public static void Save()
        {
            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save") == false )
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/" , "Save" );
            }
            
        }
    }
}