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
			int magazineAmount = currentWeapon.magazineBullets;

			currentWeapon.runtime.currentBullets = magazineAmount;
		}
	}
}
