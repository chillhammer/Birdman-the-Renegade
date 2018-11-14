using UnityEngine;

namespace SIS.Characters.Robo
{
	//RoboPadron will shoot his gun
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Shoot")]
	public class Shoot : RoboPadronBTAction
	{
		public float fireRate = 3f;
		public SO.FloatVariable overrideFireRate;
		//public float timer = 0;
		public AudioClip gunShotSound;
		public LayerMask ignoreLayers;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			if (overrideFireRate != null)
				fireRate = overrideFireRate.value;

			owner.shootTimer += owner.delta;

			if (owner.shootTimer > 1 / fireRate)
			{
				owner.shootTimer = 0;
				owner.projectileOnHit.UpdateOnHitSettings(owner, owner.bulletSystem.shape.rotation, ignoreLayers, 1);
				owner.bulletSystem.Play();
				owner.audioSource.PlayOneShot(gunShotSound, 0.1f);
				return AivoTree.AivoTreeStatus.Success;
			}

			return AivoTree.AivoTreeStatus.Running;
		}
	}
}