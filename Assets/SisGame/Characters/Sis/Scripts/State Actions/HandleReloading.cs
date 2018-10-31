using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//If Reloading, Reload
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Reloading")]
	public class HandleReloading : SisStateActions
	{
		private bool reloading = false;
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
						if (!owner.doneReloading && !reloading)
						{
							owner.anim.SetTrigger("Reload");
							reloading = true;
						}
						else
						{
							//Reload at End of Animation
							if (owner.doneReloading)
							{
								owner.isReloading = false;
								owner.doneReloading = false;
								reloading = false;
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