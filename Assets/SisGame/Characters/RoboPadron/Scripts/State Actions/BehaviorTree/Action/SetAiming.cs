using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Set Aiming")]
	public class SetAiming : RoboPadronBTAction
	{
		public bool targetBool = true;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			owner.isAiming = targetBool;

			//Debug.Log("Setting Aiming to " + targetBool);

			return AivoTree.AivoTreeStatus.Success;
		}
	}
}