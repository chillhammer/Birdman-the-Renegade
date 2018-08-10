using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions.Input
{
    [CreateAssetMenu(menuName = "Inputs/Axis")]
    public class InputAxis : Action
    {
        public string targetString;
        public float value;

        public override void Execute()
        {
            value = UnityEngine.Input.GetAxis(targetString);
        }
    }
}
