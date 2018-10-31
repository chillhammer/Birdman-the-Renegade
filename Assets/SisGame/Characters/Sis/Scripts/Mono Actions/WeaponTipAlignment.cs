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
		public SO.TransformVariable tipTransform;

		public override void Execute()
		{
			if (sis.value != null)
			{
				Items.Weapons.Weapon.RuntimeWeapon runtime = sis.value.inventory.currentWeapon.runtime;

				if (runtime.weaponTip == null)
				{
					Debug.LogWarning("There is no WeaponTip on weapon " + runtime.modelInstance.name);
					return;
				}

				Vector3 origin = tipTransform.value.position;
				Vector3 dir = sis.value.movementValues.aimPosition - origin;

				//Actual Alignment
				runtime.weaponTip.rotation = Quaternion.LookRotation(dir);

				runtime.weaponTip.position = origin;

			} else
			{
				Debug.LogWarning("Sis not defined!");
			}
		}
	}
}