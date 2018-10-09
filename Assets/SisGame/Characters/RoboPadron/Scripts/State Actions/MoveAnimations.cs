using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Move Animations")]
	public class MoveAnimations : RoboPadronStateActions
	{
		public string forwardAnimatorText = "Vertical";

		public override void Execute(RoboPadron owner)
		{

			if (owner.anim == null)
				return;

			owner.anim.SetFloat(forwardAnimatorText, owner.rigid.velocity.magnitude, 0.1f, owner.delta);
		}
	}
}