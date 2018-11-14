using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	//If Reloading, Reload
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Handle Weapon Switching")]
	public class HandleWeaponSwitching : SisStateActions
	{
		public override void Execute(Sis owner)
		{
			//Debug.Log("SwitchChange: " + owner.switchWeaponChange);
			owner.inventory.ChangeWeaponIndex(owner.switchWeaponChange);
			GameObject oldWeaponModel = owner.inventory.currentWeapon.runtime.modelInstance;
			GameObject oldWeaponTip = owner.inventory.currentWeapon.runtime.weaponTip.gameObject;
			if (owner.inventory.SpawnCurrentWeapon(owner.anim.GetBoneTransform(HumanBodyBones.RightHand)))
			{
				//owner.OnStopReloading();

				//owner.anim.SetBool("Reload", false);
				owner.PlaySound(owner.switchWeaponSound);
				Destroy(oldWeaponModel);
				Destroy(oldWeaponTip);
				//Update IK
				owner.aiming.UpdateWeaponAiming(owner.inventory.currentWeapon);
			}
		}
	}
}