using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//This action creates the IKAiming Component
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Init Actions/Init IK Aiming")]
	public class InitIKAiming : SisStateActions
	{
		public override void Execute(Sis owner)
		{
			GameObject model = owner.anim.gameObject;

			owner.aiming = model.AddComponent<IKAiming>();
			owner.aiming.Init(owner);
		}
	}
}
