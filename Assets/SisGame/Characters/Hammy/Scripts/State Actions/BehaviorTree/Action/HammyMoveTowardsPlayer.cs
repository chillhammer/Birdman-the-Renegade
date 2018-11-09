using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyMoveTowardsPlayer")]
    public class HammyMoveTowardsPlayer : HammyBTAction
    {
		public float turnSpeed = 2f;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.rigid.velocity = Vector3.zero;
			Vector3 direction = owner.playerTransform.value.position - owner.mTransform.position;
			direction.y = 0;
			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), turnSpeed * Time.deltaTime);
			Vector3 motion = owner.mTransform.forward * owner.moveSpeed.value;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
    }
}
