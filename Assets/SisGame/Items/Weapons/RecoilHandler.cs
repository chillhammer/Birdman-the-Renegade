using UnityEngine;
using System.Collections;

namespace SIS.Items.Weapons
{
	//Gets offset positions from weapon
	public class RecoilHandler
	{
		float recoilTimer;
		float recoilTime;
		bool isRecoiling;

		public Vector3 OffsetPosition { get; private set; }
		public Vector3 OffsetRotation { get; private set; }

		public void StartRecoil(float recoilTime)
		{
			if (isRecoiling)
			{
				//Debug.Log("Recoil Failed. Restarting recoiling");
				return;
			}

			isRecoiling = true;
			recoilTimer = 0;
			OffsetPosition = Vector3.forward;
			this.recoilTime = recoilTime;
		}

		public void Tick(Weapon weapon, float delta)
		{
			if (!isRecoiling)
			{
				OffsetPosition = Vector3.forward;
				return;
			}
			recoilTimer += delta;
			if (recoilTimer > recoilTime)
			{
				recoilTimer = -1;
				isRecoiling = false;
			}
			float progress = recoilTimer / recoilTime;
			OffsetPosition = Vector3.forward * weapon.recoilZ.Evaluate(progress) + Vector3.up * weapon.recoilY.Evaluate(progress);
			OffsetRotation = Vector3.zero;// Vector3.right * 90 * -weapon.recoilY.Evaluate(progress);
		}


		
	}
}