using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Items.Weapons {
    public class RigidbodyProjectileOnHit : MonoBehaviour {

        Characters.Character owner;
        Weapon weapon;
        LayerMask ignore;

        Rigidbody rb;

        float killTime;

        void Start() {
            rb = GetComponent<Rigidbody>();
        }

        void Update() {
            if (Time.time >= killTime)
                Destroy(this.gameObject);
                
        }

        public void UpdateOnHitSettings(Characters.Character owner, Weapon wep, LayerMask ignore, float persistTime) {
            this.owner = owner;
            weapon = wep;
            this.ignore = ignore;
            killTime = Time.time + persistTime;
        }

        void OnCollisionEnter(Collision collision) {
            //Doesn't support ignoring certain layers

            IHittable hittable = collision.gameObject.GetComponent<IHittable>();

            if (hittable != null) {
                hittable.OnHit(owner, weapon, rb.velocity.normalized, collision.contacts[0].point);
                Destroy(this.gameObject);
            }
        }
    }
}