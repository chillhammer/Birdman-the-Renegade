using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Shoot Action")]
	public class ShootAction : StateActions
	{

		public override void Execute(StateManager stateManager)
		{
			//Reloading
			if (stateManager.isReloading)
			{
				int bullets = stateManager.inventory.currentWeapon.currentBullets;
				int magazine = stateManager.inventory.currentWeapon.magazineBullets;
				if (bullets < magazine)
				{
					if (!stateManager.isInteracting)
					{
						stateManager.isInteracting = true;
						stateManager.PlayAnimation("Rifle Reload");
						stateManager.anim.SetBool("interacting_hands", true);
					}
					else
					{
						if (!stateManager.anim.GetBool("interacting_hands"))
						{
							stateManager.isReloading = false;
							stateManager.isInteracting = false;
							stateManager.inventory.ReloadCurrentWeapon();
						}
					}
				return;
				}
				else
				{
					stateManager.isReloading = false;
				}
			}
			//Shooting
			if (stateManager.isShooting)
			{
				stateManager.isShooting = false;
				Weapon w = stateManager.inventory.currentWeapon;
				if (w.currentBullets > 0)
				{
					if (Time.realtimeSinceStartup - w.runtime.weaponHook.lastFired > w.fireRate)
					{
						w.runtime.weaponHook.lastFired = Time.realtimeSinceStartup;
						w.runtime.weaponHook.Shoot();
						stateManager.animHook.RecoilAnim();
						if (!w.overrideBallistics)
						{
							if (stateManager.ballisticsAction != null)
							{
								stateManager.ballisticsAction.Execute(stateManager, w);
							}
						} else
						{
							w.ballistics.Execute(stateManager, w);
						}

						w.currentBullets--;
						if (w.currentBullets < 0)
							w.currentBullets = 0;
					}

				}
			} else
			{
				stateManager.isReloading = true;
			}
		}
	}
}
