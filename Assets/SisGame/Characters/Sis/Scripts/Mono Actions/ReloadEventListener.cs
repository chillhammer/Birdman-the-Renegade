using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Characters.Sis
{
	//Listens for Reload event from animator
	public class ReloadEventListener : MonoBehaviour
	{
		public SisVariable sis;

		public void Reload()
		{
			if (sis.value != null)
			{
				Items.Weapons.Weapon weapon = sis.value.inventory.currentWeapon;
				Items.Weapons.Weapon.RuntimeWeapon runtime = sis.value.inventory.currentWeapon.runtime;

				runtime.weaponFX.Reload(weapon.reloadAudio);
			}
			else
			{
				Debug.LogWarning("Sis not defined!");
			}
		}
	}
}