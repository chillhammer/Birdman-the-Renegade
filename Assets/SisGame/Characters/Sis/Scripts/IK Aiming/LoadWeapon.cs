using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SIS.States;
using SIS.Managers;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//Action that sets up weapon from inventory
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Load Weapon")]
	public class LoadWeapon : SisStateActions
	{

		public override void Execute(Sis owner)
		{

			if (owner.inventory.SpawnCurrentWeapon(owner.anim.GetBoneTransform(HumanBodyBones.RightHand))) {
				//Update IK
				owner.aiming.UpdateWeaponAiming(owner.inventory.currentWeapon);
			}
		}
	}
}
