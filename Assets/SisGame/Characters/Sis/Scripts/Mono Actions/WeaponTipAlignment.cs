using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName ="Actions/Mono Actions/Weapon Tip Alignment")]
	public class WeaponTipAlignment : Action
	{
		public SisVariable sis;

		public override void Execute()
		{
			if (sis.value != null)
			{
				Items.Weapons.Weapon.RuntimeWeapon runtime = sis.value.inventory.currentWeapon.runtime;

				Vector3 origin = (runtime.weaponTip == null ? runtime.modelInstance.transform.position : runtime.weaponTip.position);
				Vector3 dir = sis.value.movementValues.aimPosition - runtime.weaponTip.position;

				//Actual Alignment
				runtime.weaponTip.rotation = Quaternion.LookRotation(dir);

			} else
			{
				Debug.LogWarning("Sis not defined!");
			}
		}
	}
}