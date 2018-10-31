using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that waits
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Wait")]
	public class Wait : RoboPadronBTAction
	{
		public float secondsToWait = 1f;
		float timer = 0;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			timer += timeTick;
			//Debug.Log("Timer: " + timer);
			if (timer > secondsToWait)
			{
				timer = 0;
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Running;
		}
	}
}