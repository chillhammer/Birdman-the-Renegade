using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//If Reloading, Reload
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Reloading")]
	public class HandleReloading : SisStateActions
	{
		public override void Execute(Sis owner)
		{
			Weapon weapon = owner.inventory.currentWeapon;
			Weapon.RuntimeWeapon runtime = weapon.runtime;
			int bullets = runtime.currentBullets;

			//Reloading
			if (owner.isReloading)
			{
				int magazine = weapon.magazineBullets;
				if (bullets < magazine)
				{
					//Start Animation
					if (!owner.isInteracting)
					{
						owner.isInteracting = true;
						owner.PlayAnimation("Reload");
						owner.anim.SetBool("InteractingHands", true);
					}
					else
					{
						//Reload at End of Animation
						if (!owner.anim.GetBool("InteractingHands"))
						{
							owner.isReloading = false;
							owner.isInteracting = false;
							owner.inventory.ReloadCurrentWeapon();
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