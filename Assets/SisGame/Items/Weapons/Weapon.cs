using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Items.Weapons
{
	[CreateAssetMenu(menuName = "Items/Weapon")]
	public class Weapon : Item
	{
		public int magazineBullets = 30;
		public float fireRate = 0.2f;
		public float recoilLength = 0.2f;

		public SO.Vector3Variable holdingPosition;
		public SO.Vector3Variable holdingEulers;
		public GameObject modelPrefab;
		public RuntimeWeapon runtime;

		public AnimationCurve recoilY;
		public AnimationCurve recoilZ;
		
		public Ballistics ballistics;

		//Will Instiate Runtime Object of this Scriptable Object
		public void Init()
		{
			runtime = new RuntimeWeapon();

			runtime.modelInstance = Instantiate(modelPrefab);
			runtime.weaponFX = runtime.modelInstance.GetComponent<WeaponFX>();
			if (runtime.weaponFX == null)
			{
				runtime.weaponFX = runtime.modelInstance.AddComponent<WeaponFX>();
			}
			runtime.weaponFX.Init();

			runtime.lastFired = 0;
			runtime.currentBullets = magazineBullets;
		}

		//InGame Settings
		public class RuntimeWeapon
		{
			public GameObject modelInstance;
			public WeaponFX weaponFX;
			public float lastFired;
			public int currentBullets;
		}
	}
}
