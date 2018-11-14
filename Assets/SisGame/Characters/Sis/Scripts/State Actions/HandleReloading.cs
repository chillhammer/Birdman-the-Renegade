using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//If Reloading, Reload
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Reloading")]
	public class HandleReloading : SisStateActions
	{
		public SO.BoolVariable isReloading;
		public override void Execute(Sis owner)
		{
			Weapon weapon = owner.inventory.currentWeapon;
			Weapon.RuntimeWeapon runtime = weapon.runtime;

			isReloading.value = owner.isReloading;
			//Reloading
			if (owner.isReloading)
			{
				if (runtime.magazineSize < runtime.magazineCapacity)
				{
					if (runtime.currentBullets == 0)
					{
						//Chik Chik
					}
					else //Start Reloading!
					{
						owner.isShooting = false;
						//Start Animation
						if (owner.reloadingDoOnce)
						{
							Debug.Log("Starting reload");
							int bulletsNeeded = runtime.magazineCapacity - runtime.magazineSize;
							if (runtime.currentBullets < bulletsNeeded)
								bulletsNeeded = runtime.currentBullets;

							//Hotfix for reloading while switching weapons
							
							if (weapon.name == "Blunderbuss")
							{
								owner.anim.SetInteger("GunIndex", 1);
								
							} else
							{
								owner.anim.SetInteger("GunIndex", 0);
							}
							

							owner.anim.SetBool("Reload", true);
							owner.anim.SetInteger("AmmoToLoad", bulletsNeeded);

							owner.reloadingDoOnce = false;
							owner.doneReloading = false;
						}
						else
						{
							//Reload at End of Animation
							if (owner.doneReloading)
							{
								Debug.Log("Finishing reload");
								owner.anim.SetBool("Reload", false);
								owner.isReloading = false;
								owner.doneReloading = false;
								owner.reloadingDoOnce = true;
								owner.inventory.ReloadCurrentWeapon();
							}
						}
					}
				}
				else //Magazine is Full. Can't Reload
				{
					owner.isReloading = false;
				}
			}
		}
	}
}