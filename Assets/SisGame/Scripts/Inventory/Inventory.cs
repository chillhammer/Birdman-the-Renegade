using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Items.Weapons;

namespace SIS.Items
{
	[System.Serializable]
	public class Inventory
	{
		public string weaponName;
		public Weapon currentWeapon;

		public void ReloadCurrentWeapon()
		{
			Weapon.RuntimeWeapon runtime = currentWeapon.runtime;
			int bulletsNeeded = runtime.magazineCapacity - runtime.magazineSize;

			if (currentWeapon.infiniteTotalBullets)
			{
				runtime.magazineSize += bulletsNeeded;
				return;
			}

			//Check if we have enough bullets for full reload
			if (runtime.currentBullets < bulletsNeeded) {
				runtime.magazineSize += runtime.currentBullets;
				runtime.currentBullets = 0;
			} else {
				runtime.magazineSize = runtime.magazineCapacity;
				runtime.currentBullets -= bulletsNeeded;
			}
		}
	}
}
