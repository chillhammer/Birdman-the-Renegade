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
        [SerializeField]
        AudioSource audioSource;
        [SerializeField]
        AudioClip angrySound;
        [SerializeField]
        Sis.SisVariable sisVariable;
        [SerializeField]
        AnimationCurve deathSizeCurve;
        WaypointNavigator waypointNavigator;
        CharacterController charactercontroller;

        private float timer = 0f;
        public bool dead = false;
        [SerializeField]
        private bool angry = false;
        public float normalspeed = 1;
        public float angryspeed = 4;
        public float hitDistance = 1f;
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
            if (dead)
            {
                timer += Time.deltaTime;

                float deathTime = 2f;
                float deathSize = deathSizeCurve.Evaluate(timer / deathTime);
                transform.localScale = new Vector3(deathSize, deathSize, deathSize);

                if (timer >= deathTime)
                {
                    Destroy(gameObject);
                }
                return;
            }
            if (playerTransform.value != null)
            {
                //If There is a Straight Path
                if (angry && waypointNavigator.PathFinished && CanSeePlayer())
                {
                    //Naive Pathfinding
                    Vector3 dirtoplayer = playerTransform.value.position - transform.position;
                    charactercontroller.Move(angryspeed * dirtoplayer * Time.deltaTime);
                    dirtoplayer.y = 0;
                    Quaternion quatdir = Quaternion.LookRotation(dirtoplayer);
                    float rotationspeed = 5f;
                    transform.rotation =
                        Quaternion.Slerp(transform.rotation, quatdir, rotationspeed * Time.deltaTime);
                }
                else
                {
                    //Smart pathfinding
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
                                audioSource.PlayOneShot(angrySound, 0.1f);
                                //Debug.Log("Bill saw the player");

                            }
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
                dir.y = 0;
                Quaternion quatdir = Quaternion.LookRotation(dir);
                float rotationspeed = 2.0f;
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, quatdir, rotationspeed * Time.deltaTime);
            }
            //Hit Sis!
            if (Vector3.Distance(transform.position, playerTransform.value.position) < hitDistance)
            {
                sisVariable.value.OnHit(null, 3f, Vector3.zero, Vector3.zero);
                SetDead();

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
            SetDead();
        }

        public bool IsDead()
        {
            return dead;
            
        }

        void SetDead()
        {
            dead = true;
            timer = 0;
        }

        public void PlaySound(AudioClip audio)
        {
            audioSource.PlayOneShot(audio);
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, hitDistance);
        }
    }
}