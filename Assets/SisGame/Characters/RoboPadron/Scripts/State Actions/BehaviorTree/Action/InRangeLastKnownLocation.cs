using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/In Range Last Known Location")]
	public class InRangeLastKnownLocation : RoboPadronBTAction
	{
		public float range = 2f;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			float dist = Vector3.Distance(owner.mTransform.position, owner.playerLastKnownLocation.position);
			if (dist <= range)
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Failure;
		}
	}
}