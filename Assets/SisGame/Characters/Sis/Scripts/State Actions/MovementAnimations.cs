using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//Set Anim Float for Move Amount
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Movement Animations")] 
	public class MovementAnimations : SisStateActions
	{
		public string floatName;
		public override void Execute(Sis owner)
		{
			if (owner.anim == null)
			{
				Debug.Log("No Animator Found in action MovementAnimations");
				return;
			}
			owner.anim.SetFloat(floatName, owner.movementValues.moveAmount, 0.1f, owner.delta);

		}
	}
}