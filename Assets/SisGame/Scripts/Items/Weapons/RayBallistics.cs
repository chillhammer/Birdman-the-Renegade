using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters.Sis;

namespace SIS.Items.Weapons
{
	[CreateAssetMenu(menuName = "Ballistics/Ray")]
	public abstract class RayBallistics : Ballistics
	{
		public override void Execute(Sis stateManager, Weapon weapon)
		{
			Vector3 origin = weapon.runtime.modelInstance.transform.position;
			Vector3 dir = stateManager.movementValues.aimPosition - origin;
			Ray ray = new Ray(origin, dir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, stateManager.ignoreLayers))
			{
				IHittable isHittable = hit.transform.GetComponentInParent<IHittable>();
				if (isHittable == null)
				{
					//Particle Effect
					//GameObject hitParticle = GameObject.
				} else
				{
					isHittable.OnHit(stateManager, weapon, dir, hit.point);
				}
			}
		}
	}
}
