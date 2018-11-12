using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters.Sis;
using SIS.Items.Weapons;
using TMPro;

namespace SIS.HUD
{
	public class AmmoHUDController : MonoBehaviour
	{
		public SisVariable sisVariable;
		public TextMeshProUGUI textUI;

		// Update is called once per frame
		void Update()
		{
			if (sisVariable.value != null)
			{
				Weapon weapon = sisVariable.value.inventory.currentWeapon;
				if (weapon != null)
				{
					Weapon.RuntimeWeapon runtimeWeapon = weapon.runtime;
					int magazine = runtimeWeapon.magazineSize;

					int total = runtimeWeapon.currentBullets;
					string ammoText = (magazine < 10 ? "0" : "") + magazine.ToString() + " | " +
						(weapon.infiniteTotalBullets ? "Inf." : total.ToString());
					textUI.text = ammoText;
				}
			}
		}
	}
}