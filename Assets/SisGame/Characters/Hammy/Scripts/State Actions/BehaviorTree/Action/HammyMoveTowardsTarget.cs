using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AivoTree;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyMoveTowardsTarget")]
    public class HammyMoveTowardsTarget : HammyBTAction
    {
		public float moveSpeed = 3f;
		// public float turnSpeed = 2f;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			if (owner.targetInstance == null) {
				return AivoTree.AivoTreeStatus.Running; //hack
			}
			owner.rigid.velocity = Vector3.zero;
			Vector3 direction = owner.targetInstance.transform.position - owner.HammerBaseTransform.position;
			direction.y = 0;
			// owner.transform.forward = direction;
			Vector3 motion = direction * moveSpeed * Time.deltaTime;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
    }
}
