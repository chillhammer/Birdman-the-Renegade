using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySlamInRadius")]
    public class HammySlamInRadius : HammyBTAction
    {
		public Sis.SisVariable sis;
		public float force = 10f;
        public override AivoTreeStatus Act(float timeTick, Hammy owner) {
			if (owner.HammerBaseTransform.position.y > 0.1 && owner.InAir()) {
				return AivoTree.AivoTreeStatus.Running;
			}
			if (owner.targetInstance == null) return AivoTree.AivoTreeStatus.Failure;
			Vector3 direction = owner.playerTransform.value.position - owner.targetInstance.transform.position;
			direction.y = 0;
			double distance = direction.magnitude;
			if (distance < owner.radius.value) {
				Debug.Log(distance);
				sis.value.OnHit(owner, owner.damage.value, direction.normalized * force, owner.transform.position);
			}
			owner.SpawnExplosionAtPos(owner.targetInstance.transform.position + Vector3.up * 0.1f);
			owner.PlaySlam();
			owner.DestroyTarget();
			owner.rigid.velocity = Vector3.zero;
			return AivoTree.AivoTreeStatus.Success;
        }
    }
}
