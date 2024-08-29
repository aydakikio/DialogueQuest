using UnityEngine;

namespace DialogueQuest.Logic
{
    public class Scene_Vision : MonoBehaviour
    {
        public GameObject game_object { get; private set; }
        public Animator animator { get; private set; }

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