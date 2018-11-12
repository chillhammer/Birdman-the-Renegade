using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;
using System;

namespace SIS.Characters.Sis
{
	//If Shooting, Shoot
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Shooting")]
	public class HandleShooting : SisStateActions
	{
		public SO.BoolVariable isShooting;
		public float shootingLength = 0.2f;

		float shootingTimer = 0;

		bool startedShooting;
		bool hasShot;

		private void OnEnable()
		{
			startedShooting = false;
			hasShot = false;
		}

		public override void Execute(Sis owner)
		{
			Weapon weapon = owner.inventory.currentWeapon;
			Weapon.RuntimeWeapon runtime = weapon.runtime;

			isShooting.value = startedShooting;

			//Update Shooting Variables for FX and Ballistics
			Vector3 origin = (runtime.weaponTip == null ? runtime.modelInstance.transform.position : runtime.weaponTip.position);
			Vector3 dir = owner.movementValues.aimPosition - origin;



			//Shooting
			if (owner.isShooting && startedShooting && !hasShot && Time.timeScale != 0)
			{
				hasShot = true;
				if (runtime.magazineSize > 0)
				{
					if (Time.realtimeSinceStartup - runtime.lastFired > weapon.fireRate)
					{
						runtime.lastFired = Time.realtimeSinceStartup;

						//Ballistics
						if (weapon.ballistics != null)
							weapon.ballistics.Execute(owner, weapon.damageOnHit, dir, origin);

						//Recoil and FX
						runtime.weaponFX.Shoot(dir);
						owner.aiming.StartRecoil();

						//Decrement Bullets
						if (weapon.decrementBulletsOnShoot)
						{
							runtime.magazineSize--;
							if (runtime.magazineSize < 0)
								runtime.currentBullets = 0;
						}
					}
				}
				else //Reload if out of bullets
				{
					startedShooting = false;
					isShooting.value = false;
					owner.isShooting = false;
					owner.isReloading = true;
				}
			}

			if (owner.isShooting && !startedShooting && !owner.isReloading && Time.timeScale != 0)
			{
				isShooting.value = true;
				startedShooting = true;
			}

			//Delay
			if (startedShooting)
			{
				DelayStopShooting(owner);
			}

		}

		private void DelayStopShooting(Sis owner)
		{
			//yield return new WaitForSeconds(1f);
			shootingTimer += owner.delta;
			if (shootingTimer > shootingLength) {
				isShooting.value = false;
				owner.isShooting = false;
				startedShooting = false;
				shootingTimer = 0;
				hasShot = false;
			}
		}
	}
}
