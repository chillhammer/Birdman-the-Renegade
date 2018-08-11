using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/UpdateRecoil")]
	public class UpdateRecoil : SisStateActions
	{
		public override void Execute(Sis owner)
		{
			owner.aiming.HandleRecoil();
		}
	}
}