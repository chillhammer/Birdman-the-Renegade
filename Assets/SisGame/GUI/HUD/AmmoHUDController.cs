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
				Weapon.RuntimeWeapon runtimeWeapon = sisVariable.value.inventory.currentWeapon.runtime;
				int magazine = runtimeWeapon.magazineSize;
				int total = runtimeWeapon.currentBullets;
				string ammoText = (magazine < 10 ? " " : "") + magazine.ToString() + " | " + total.ToString();
				textUI.text = ammoText;
			}
		}
	}
}