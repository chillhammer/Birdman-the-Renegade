using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.BehaviorEditor
{
    //[CreateAssetMenu(menuName = "Editor/Comment Node")]
    public class CommentNode<C> : DrawNode<C> where C : Character
    {
        
        public override void DrawWindow(BaseNode<C> b)
        {
            b.comment = GUILayout.TextArea(b.comment, 200);
        }

        public override void DrawCurve(BaseNode<C> b)
        {
        }
    }
}
