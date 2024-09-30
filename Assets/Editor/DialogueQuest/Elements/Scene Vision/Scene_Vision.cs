using UnityEngine;

namespace DialogueQuest.Logic
{
    public class Scene_Vision : MonoBehaviour
    {
        //This class will be completed after developing main features of Dialogue System
        private GameObject game_object;
        private Animator animator;

        public static Animator Get_Animator(string object_name)
        {
            Animator animator = null;

            GameObject[] all_of_gameobjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject paramter in all_of_gameobjects)
            {

                if (paramter.name.ToLower() == object_name.ToLower())
                {
                    animator = paramter.GetComponent<Animator>();
                }
            }
            return animator;
        }
    }
}