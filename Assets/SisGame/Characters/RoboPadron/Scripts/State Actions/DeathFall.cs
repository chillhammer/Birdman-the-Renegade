using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Robo
{
	//What can RoboPadron see?
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/DeathFall")]
	public class DeathFall : RoboPadronBTAction
	{
		public AnimationCurve fallProgression;
		public AudioClip deathFallSound;

		public float secondsToFall = 5f;

		public override AivoTree.AivoTreeStatus Act(float TimeTick, RoboPadron owner)
		{
			if (owner.deathFallProgress.timer == 0)
			{
				float angleFall = Random.value * 360 * Mathf.Deg2Rad;
				owner.deathFallProgress.fallTowards = new Vector3(Mathf.Sin(angleFall), 0, Mathf.Cos(angleFall));
				owner.deathFallProgress.originalRotation = owner.mTransform.rotation;
				owner.anim.SetFloat("Vertical", 0);
				owner.PlaySound(deathFallSound);
				owner.StartCoroutine("Die");
			}

			if (owner.deathFallProgress.timer < secondsToFall) {
				owner.deathFallProgress.timer += owner.delta;

				Vector3 vectorFall = owner.deathFallProgress.fallTowards;
				float progression = fallProgression.Evaluate((owner.deathFallProgress.timer / secondsToFall));
				Vector3 currentFall = Vector3.Slerp(Vector3.up, vectorFall, progression);
				owner.mTransform.rotation = owner.deathFallProgress.originalRotation *
					Quaternion.FromToRotation(Vector3.up, currentFall);
			} else
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Running;
		}

		
	}
}