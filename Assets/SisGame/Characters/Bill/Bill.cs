using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Waypoints;
namespace SIS.Characters.Bill
{
    public class Bill : MonoBehaviour, IHittable
    {
        [SerializeField]
        SO.TransformVariable playerTransform;
        [SerializeField]
        LayerMask visionMask;
        WaypointNavigator waypointNavigator;
        CharacterController charactercontroller;

        private float timer = 0f;
        [SerializeField]
        private bool angry = false;
        public float normalspeed = 1;
        public float angryspeed = 4;
        // Use this for initialization
        void Start()
        {
            waypointNavigator = GetComponent<WaypointNavigator>();
            waypointNavigator.StartNavigation(playerTransform.value.position);
            charactercontroller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {   
            if (playerTransform.value != null)
            {
                timer += Time.deltaTime;
                if (timer > 0.5f)
                {
                    timer = 0;
                    waypointNavigator.StartNavigation(playerTransform.value.position);
                    // First time seeing the player.
                    if (angry == false)
                    { 
                        if (CanSeePlayer())
                        {
                            angry = true;
                            Debug.Log("Bill saw the player");

                        }
                    }
                }
            }
            
            // Movement and rotation.
            if (!waypointNavigator.PathFinished)
            {
                float speed = (angry ? angryspeed : normalspeed);
                Vector3 dir = waypointNavigator.DirectionToWaypoint;
                charactercontroller.Move(dir * speed * Time.deltaTime);
                Quaternion quatdir = Quaternion.LookRotation(dir);
                float rotationspeed = 2.0f;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, quatdir, rotationspeed * Time.deltaTime);
            }

        }

        bool CanSeePlayer()
        {
            Vector3 dirtoplayer = playerTransform.value.position - transform.position;
            Ray ray = new Ray(transform.position, dirtoplayer);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f, visionMask))
            {
                Debug.Log(raycastHit.collider.name);
                if (raycastHit.collider.tag == "Player")
                    return true;
            }
            return false;

        }

        public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
        {
            //To do: Create Explosion, death animation.
            Destroy(gameObject);
        }

        public bool IsDead()
        {
            return false;
            
        }

        public void PlaySound(AudioClip audio)
        {
            //To do: add audio source component.
        }
    }
}