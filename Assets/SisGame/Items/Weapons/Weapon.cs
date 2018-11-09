using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Items.Weapons
{
	[CreateAssetMenu(menuName = "Items/Weapon")]
	public class Weapon : Item
	{
		public bool decrementBulletsOnShoot = true;
		public bool infiniteTotalBullets = false;
		public int numBullets = 30;
		public int magazineCapacity = 30;
		public float fireRate = 0.2f;
		public float recoilLength = 0.2f;
		public int damageOnHit = 1;

		public SO.Vector3Variable holdingPosition; //used in IKAiming, to snap arm to weapon
		public SO.Vector3Variable holdingEulers;

		public bool IKLeftHand = false;
		public SO.Vector3Variable leftHoldingPosition; //used in IKAiming
		public SO.Vector3Variable leftHoldingEulers;

		public GameObject modelPrefab;
		public RuntimeWeapon runtime;

		public AnimationCurve recoilY;
		public AnimationCurve recoilZ;

		public AudioClip fireAudio;
		public AudioClip reloadAudio;

		public Ballistics ballistics;
		public string topHalfAnimatorLayerName = "RevolverTopHalf"; //for custom animations

		//Will Instiate Runtime Object of this Scriptable Object
		public void Init()
		{
			runtime = new RuntimeWeapon();

			CreateModel();


			runtime.lastFired = 0;
			runtime.currentBullets = numBullets;
			runtime.magazineCapacity = magazineCapacity;
			runtime.magazineSize = magazineCapacity;
		}

		public void CreateModel()
		{
			runtime.modelInstance = Instantiate(modelPrefab);
			runtime.weaponTip = runtime.modelInstance.transform.Find("WeaponTip");
			runtime.weaponFX = runtime.modelInstance.GetComponent<WeaponFX>();
			if (runtime.weaponFX == null)
			{
				runtime.weaponFX = runtime.modelInstance.AddComponent<WeaponFX>();
			}
			runtime.weaponFX.Init(runtime.weaponTip, fireAudio);

			if (runtime.weaponTip)
			{
				runtime.weaponTip.SetParent(null); //Detach
			}
			ballistics.Init(this);
		}

		//InGame Settings
		[System.Serializable]
		public class RuntimeWeapon
		{
			public GameObject modelInstance;
			public Transform weaponTip;
			public WeaponFX weaponFX;
			public float lastFired;
			public int currentBullets;
			public int magazineCapacity;
			public int magazineSize;
		}
	}
}
