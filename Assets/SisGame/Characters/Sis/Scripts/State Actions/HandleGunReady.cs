using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;
using System;

namespace SIS.Characters.Sis
{
	//If Shooting, Become Gun Ready to shoot again
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle GunReady")]
	public class HandleGunReady : SisStateActions
	{
		public SO.BoolVariable isGunReady;
		public SO.BoolVariable isShooting;
		public float gunReadyLength = 0.6f;

		float gunReadyTimer = 0;

		public override void Execute(Sis owner)
		{
			if (isShooting.value && !owner.isAiming && !owner.isReloading)
			{
				gunReadyTimer = gunReadyLength;
			}

			if (gunReadyTimer > 0)
			{
				gunReadyTimer -= owner.delta;
			}

			isGunReady.value = gunReadyTimer > 0;
			owner.isGunReady = isGunReady.value;
		}
	}
}
