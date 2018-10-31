using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.Items.Weapons
{
	//Shoots simple bullet in a perfect ray
	[CreateAssetMenu(menuName = "Items/Ballistics/Ray")]
	public class RayBallistics : Ballistics
	{
		public override void Init(Weapon weapon)
		{
		}

		public override void Execute(Character owner, float baseDamage, Vector3 intendedDirection, Vector3 origin)
		{
			Vector3 dir = intendedDirection;
			Ray ray = new Ray(origin, dir);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, ~ignoreLayers))
			{
				IHittable isHittable = hit.transform.GetComponentInParent<IHittable>();
				if (isHittable == null)
				{
					//Particle Effect
					//GameObject hitParticle = GameObject.
				} else
				{
					isHittable.OnHit(owner, baseDamage, dir, hit.point);
				}
			}
		}
	}
}
