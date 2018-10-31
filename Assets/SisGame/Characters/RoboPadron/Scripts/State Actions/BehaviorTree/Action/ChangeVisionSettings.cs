using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to see differently
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Change Vision Settings")]
	public class ChangeVisionSettings : RoboPadronBTAction
	{
		public float fov;
		public float maxDistance;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			owner.vision.angleFOV = fov;
			owner.vision.maxDistance = maxDistance;
			return AivoTree.AivoTreeStatus.Success;
		}
	}
}