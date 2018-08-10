using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//If Shooting, Shoot
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Shooting")]
	public class HandleShooting : StateActions<Sis>
	{
		public override void Execute(Sis owner)
		{
			Weapon weapon = owner.inventory.currentWeapon;
			Weapon.RuntimeWeapon runtime = weapon.runtime;
			int bullets = runtime.currentBullets;

			//Shooting
			if (owner.isShooting)
			{
				owner.isShooting = false;
				
				if (bullets > 0)
				{
					if (Time.realtimeSinceStartup - runtime.lastFired > weapon.fireRate)
					{
						runtime.lastFired = Time.realtimeSinceStartup;
						//Recoil and FX
						runtime.weaponFX.Shoot();
						owner.aiming.StartRecoil();

						//Ballistics
						Vector3 origin = runtime.modelInstance.transform.position;
						Vector3 dir = owner.movementValues.aimPosition - origin;
						weapon.ballistics.Execute(owner, weapon, dir);

						//Decrement Bullets
						runtime.currentBullets--;
						if (runtime.currentBullets < 0)
							runtime.currentBullets = 0;
					}
				}
				else //Reload if out of bullets
				{
					owner.isReloading = true;
				}
			} 
		}
	}
}
