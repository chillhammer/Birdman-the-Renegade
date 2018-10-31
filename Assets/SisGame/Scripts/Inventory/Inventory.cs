using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Items.Weapons;

namespace SIS.Items
{
	[System.Serializable]
	public class Inventory
	{
		public Weapon currentWeapon;
		public List<Weapon> weapons;
		[SerializeField] int currentWeaponIndex = 0;
		Dictionary<string, Weapon> initializedWeapons;

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

		public bool SpawnCurrentWeapon(Transform parentTransform)
		{
			if (currentWeapon != null && weapons[currentWeaponIndex].name == currentWeapon.name)
			{
				return false;
			}
			Managers.ResourcesManager rm = Managers.GameManagers.ResourceManager;


			//Create Weapon
			string weaponName = weapons[currentWeaponIndex].name;
			Weapon targetWeapon;
			if (initializedWeapons == null)
				initializedWeapons = new Dictionary<string, Weapon>(); 
			if (initializedWeapons.TryGetValue(weaponName, out targetWeapon))
			{
				currentWeapon = targetWeapon;
				targetWeapon.CreateModel();
			} else
			{
				targetWeapon = (Weapon)rm.InstiateItem(weaponName);
				currentWeapon = targetWeapon;
				targetWeapon.Init();
				initializedWeapons.Add(weaponName, targetWeapon);
			}

			if (parentTransform != null)
			{
				//Set Weapon Transform
				Transform rightHand = parentTransform;
				Transform weaponTransform = targetWeapon.runtime.modelInstance.transform;
				weaponTransform.parent = rightHand;
				weaponTransform.localPosition = Vector3.zero;
				weaponTransform.localEulerAngles = Vector3.zero;
				weaponTransform.localScale = new Vector3(1, 1, 1);
			}

			return true;
		}

		public void ChangeWeaponIndex(int change)
		{
			currentWeaponIndex += change;
			if (currentWeaponIndex < 0)
				currentWeaponIndex = weapons.Count - 1;
			else
				currentWeaponIndex %= weapons.Count;
		}
	}
}
