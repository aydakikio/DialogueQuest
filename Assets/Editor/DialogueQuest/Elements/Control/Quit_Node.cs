using System.Drawing;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UIElements.Image;

namespace DialogueQuest.Elements
{
    public class Quit_Node:Control_Base_Node
    {
        public override void Initialize(Vector2 position)
        {
            base.Initialize(position);
        }

        public override void Draw()
        {
            Texture2D Quit_picture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor Default Resources/Images/exit_sign.png");
            VisualElement Quit_picture_container = new VisualElement();
            Quit_picture_container.style.backgroundImage = Quit_picture;
            outputContainer.Add(Quit_picture_container);
        }
       
    }
}