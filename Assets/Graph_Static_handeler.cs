using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace DialogueQuest
{
    public class Graph_Static_handeler
    {
        
        private string temp_name;
        private string full_path = Path.GetFullPath(Environment.CurrentDirectory);
        
        public void create_graph_static()
        {
            temp_name = Hash(new Guid().ToString());
            if (AssetDatabase.IsValidFolder("Assets/Dialogue_Manager/Save") == false)
            {
                AssetDatabase.CreateFolder("Assets/Dialogue_Manager/", "Save");
            }

            if (AssetDatabase.IsValidFolder($"Assets/Dialogue_Manager/Save/{temp_name}") == false)
            {
                AssetDatabase.CreateFolder($"Assets/Dialogue_Manager/Save/" , temp_name);
            }
            
            var statics = new SqliteConnection($"Data Source=file:{full_path}//Dialogue_Manager/Save/{temp_name}/Graph_Statics.db");
            statics.Open();

            var dbCommand = new SqliteCommand(@"CRATE TABLE Graph_statics(id INTEGER PRIMARY KEY , node_id TEXT NOT NULL , node_name TEXT NOT NULL , start_point BOOLEAN  )" , statics);
            dbCommand.ExecuteNonQuery();
            
            Debug.Log("The db crated");
            statics.Close();
        }

        public void rename_the_graph_static_folder(string graph_file_name)
        {
            
        }

        public void Add_items_to_statics()
        {
            
        }

        public void Remove_items_from_statics()
        {
            
        }

        public void Update_the_values()
        {
            
        }


        #region Other

        public static string Hash(string data)
        {
            var sb = new StringBuilder();
            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(data));

                foreach (var item in result) sb.Append(item.ToString("x2"));
            }

            return sb.ToString();
        }

        #endregion
    }
}