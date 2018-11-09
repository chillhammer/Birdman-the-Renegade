using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySlamInRadius")]
    public class HammySlamInRadius : HammyBTAction
    {
		public double radius;
		public Sis.SisVariable sis;
		public float force = 10f;
        public override AivoTreeStatus Act(float timeTick, Hammy owner) {
			if (owner.HammerBaseTransform.position.y > 0.1)
				return AivoTree.AivoTreeStatus.Running;

			Vector3 direction = owner.playerTransform.value.position - owner.targetInstance.transform.position;
			direction.y = 0;
			double distance = direction.magnitude;
			if (distance < radius) {
				Debug.Log(distance);
				sis.value.OnHit(owner, 5, direction.normalized * force, owner.transform.position);
			}
			owner.DestroyTarget();
			return AivoTree.AivoTreeStatus.Success;
        }
    }
}
