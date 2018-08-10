using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters.Sis;

namespace SIS.Items.Weapons
{
	[CreateAssetMenu(menuName = "Characters/Sis/Ballistics/Ray")]
	public abstract class RayBallistics : Ballistics<Sis>
	{
		public override void Execute(Sis owner, Weapon weapon)
		{
			Vector3 origin = weapon.runtime.modelInstance.transform.position;
			Vector3 dir = owner.movementValues.aimPosition - origin;
			Ray ray = new Ray(origin, dir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, owner.ignoreLayers))
			{
				IHittable isHittable = hit.transform.GetComponentInParent<IHittable>();
				if (isHittable == null)
				{
					//Particle Effect
					//GameObject hitParticle = GameObject.
				} else
				{
					isHittable.OnHit(owner, weapon, dir, hit.point);
				}
			}
		}
	}
}
