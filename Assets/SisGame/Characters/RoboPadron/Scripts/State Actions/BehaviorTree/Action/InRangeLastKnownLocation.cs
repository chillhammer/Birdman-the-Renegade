using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/In Range Last Known Location")]
	public class InRangeLastKnownLocation : RoboPadronBTAction
	{
		public float range = 2f;
		public SO.FloatVariable overrideRange;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			if (overrideRange != null)
			{
				range = overrideRange.value;
			}
			float dist = Vector3.SqrMagnitude(owner.playerLastKnownLocation.position - owner.mTransform.position);
			//Debug.Log("Range to LKL: " + Mathf.Sqrt(dist));
			if (dist <= range * range)
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Failure;
		}
	}
}