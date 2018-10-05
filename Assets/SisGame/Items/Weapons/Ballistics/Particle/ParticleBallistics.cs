using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.Items.Weapons
{
	//Shoots child particle system, and uses that as bullet
	[CreateAssetMenu(menuName = "Items/Ballistics/Particle")]
	public class ParticleBallistics : Ballistics
	{
		public string particleSystemName;
		ParticleSystem part;
		ParticleProjectileOnHit onHitSettings;

		public override void Init(Weapon weapon)
		{
			Transform partTransform = weapon.runtime.weaponTip.Find(particleSystemName);
			if (partTransform != null)
			{
				part = partTransform.GetComponent<ParticleSystem>();
				onHitSettings = partTransform.gameObject.AddComponent<ParticleProjectileOnHit>();
			} else
			{
				Debug.LogWarning("Could not find ParticleSystem: " + particleSystemName);
			}
		}

		public override void Execute(Character owner, Weapon weapon, Vector3 intendedDirection)
		{
			onHitSettings.UpdateOnHitSettings(owner, weapon, intendedDirection, ignoreLayers);
			part.Play();
		}
	}
}
