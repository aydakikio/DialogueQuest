using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialogue_Quest.Window
{
    public class Graph_View : GraphView
    {
        public Graph_View()
        {
            ADD_Grid_Background();
            Add_Maniplators();
        }

        private void ADD_Grid_Background()
        {
            GridBackground gridBackground = new GridBackground();
            gridBackground.StretchToParentSize();
            Insert(0 , gridBackground );
        }

        private void Add_Maniplators()
        {
            this.AddManipulator(new ContentDragger());
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            //Set other manipulators
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new SelectionDragger());
        }
    }
}

