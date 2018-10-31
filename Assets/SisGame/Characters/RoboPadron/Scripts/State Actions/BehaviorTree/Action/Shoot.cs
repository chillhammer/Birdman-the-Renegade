using UnityEngine;

namespace SIS.Characters.Robo
{
	//RoboPadron will shoot his gun
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Shoot")]
	public class Shoot : RoboPadronBTAction
	{
		public float fireRate = 3f;
		public float timer = 0;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			timer += owner.delta;

			if (timer > 1 / fireRate)
			{
				timer = 0;
				owner.projectileOnHit.UpdateOnHitSettings(owner, owner.bulletSystem.shape.rotation, 0, 1);
				owner.bulletSystem.Play();
				return AivoTree.AivoTreeStatus.Success;
			}

			return AivoTree.AivoTreeStatus.Running;
		}
	}
}