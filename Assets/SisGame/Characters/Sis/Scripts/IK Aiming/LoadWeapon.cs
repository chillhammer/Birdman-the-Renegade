using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SIS.States;
using SIS.Managers;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//Action that sets up weapon from inventory
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Load Weapon")]
	public class LoadWeapon : SisStateActions
	{

		public override void Execute(Sis owner)
		{
			ResourcesManager rm = GameManagers.ResourceManager;

			//Create Weapon
			Weapon targetWeapon = (Weapon)rm.InstiateItem(owner.inventory.weaponName);
			owner.inventory.currentWeapon = targetWeapon;
			targetWeapon.Init();

			//Set Weapon Transform
			Transform rightHand = owner.anim.GetBoneTransform(HumanBodyBones.RightHand);
			Transform weaponTransform = targetWeapon.runtime.modelInstance.transform;
			weaponTransform.parent = rightHand;
			weaponTransform.localPosition = Vector3.zero;
			weaponTransform.localEulerAngles = Vector3.zero;
			weaponTransform.localScale = new Vector3(1,1,1);

			//Update IK
			owner.aiming.UpdateWeaponAiming(targetWeapon);
		}
	}
}
