using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.Items.Weapons
{
	//Shoots simple bullet in a perfect ray
	[CreateAssetMenu(menuName = "Items/Ballistics/Rigidbody")]
	public class RigidbodyBallistics : Ballistics
	{
        [SerializeField]
        private GameObject projectile;
        [SerializeField]
        private float projectileSpeed;
        [SerializeField]
        private float persistTime;
		[SerializeField]
		private float upwardsForce;

		private GameObject container;

        public override void Init(Weapon weapon)
		{
            //Find the container to hold all the bullets
            container = GameObject.Find("Projectiles");
            if (container == null) {
                //Make one at the top level
                container = new GameObject("Projectiles");
            }
        }

		public override void Execute(Character owner, float baseDamage, Vector3 intendedDirection, Vector3 origin)
		{
			Vector3 dir = intendedDirection + Vector3.up * upwardsForce; //account for gravity
            GameObject proj = Instantiate<GameObject>(projectile, origin, Quaternion.identity);
            Rigidbody rb = proj.GetComponent<Rigidbody>();
            RigidbodyProjectileOnHit onHit = proj.GetComponent<RigidbodyProjectileOnHit>();

            if (rb == null) {
                Debug.LogWarning("RigidbodyBallistics projectiles must have a rigidbody component");
                return;
            }
            if (onHit == null) {
                Debug.LogWarning("RigidbodyBallistics projectiles must have a RigidbodyProjectileOnHit component");
                return;
            }

            onHit.UpdateOnHitSettings(owner, baseDamage, ignoreLayers, persistTime);
            rb.velocity = dir.normalized * projectileSpeed;
            proj.transform.parent = container.transform;
		}
	}
}
