using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace SA
{
    [CreateAssetMenu]
    public class FollowTransform : Action
    {
        public TransformVariable targetTransform;
        public TransformVariable currentTransform;
        public float speed = 9;

        public override void Execute()
        {
            if (targetTransform.value == null)
                return;
            if (currentTransform.value == null)
                return;

            Vector3 targetPosition =
                Vector3.Lerp(currentTransform.value.position, targetTransform.value.position, Time.deltaTime * speed);
            currentTransform.value.position = targetPosition;
        }
    }
}
