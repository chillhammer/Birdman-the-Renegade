using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AivoTree;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyMoveTowardsTarget")]
    public class HammyMoveTowardsTarget : HammyBTAction
    {
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			if (owner.targetInstance == null) {
				return AivoTree.AivoTreeStatus.Running; //hack
			}
			owner.rigid.velocity = Vector3.zero;
			Vector3 direction = owner.targetInstance.transform.position - owner.HammerBaseTransform.position;
			direction.y = 0;
			Quaternion quatDirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			if (Quaternion.Angle(owner.mTransform.rotation, quatDirection) < 10) {
				owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation, quatDirection, 1 * Time.deltaTime);
			}
			Vector3 motion = Vector3.ClampMagnitude(direction, 1) * 5;
			// Vector3 motion = owner.mTransform.forward * owner.moveSpeed.value;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
    }
}
