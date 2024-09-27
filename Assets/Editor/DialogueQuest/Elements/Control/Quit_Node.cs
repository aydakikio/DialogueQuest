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
            base.Draw();
            
            Texture2D Quit_picture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor Default Resources/Images/exit_sign.png");

            Image Picture_Container = new Image() { image = Quit_picture , style = { width = Quit_picture.width , height = Quit_picture.height }};
            
            outputContainer.Add(Picture_Container);
        }
       
    }
}