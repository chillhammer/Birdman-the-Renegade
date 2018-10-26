using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Items.Weapons {
    public class RigidbodyProjectileOnHit : MonoBehaviour {

        Characters.Character owner;
        Weapon weapon;
        LayerMask ignore;
		float baseDamage;

        Rigidbody rb;

        float killTime;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            if (Time.time >= killTime)
                Destroy(this.gameObject);
                
        }

        public void UpdateOnHitSettings(Characters.Character owner, float baseDamage, LayerMask ignore, float persistTime) {
            this.owner = owner;
			this.baseDamage = baseDamage;
            this.ignore = ignore;
            killTime = Time.time + persistTime;
        }

        void OnCollisionEnter(Collision collision) {
			//Doesn't support ignoring certain layers
			if ((ignore.value & (1 << collision.collider.transform.gameObject.layer)) > 0)
				return;
            IHittable hittable = collision.gameObject.GetComponent<IHittable>();

            if (hittable != null) {
                hittable.OnHit(owner, baseDamage, rb.velocity.normalized, collision.contacts[0].point);
                Destroy(this.gameObject);
            }
        }
    }
}