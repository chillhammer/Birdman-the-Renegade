using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using SIS.Items;
using SIS.Items.Weapons;

//Assume everythin in the MinimumViableHammy is mandatory to get a StateMachine character running
//	unless specified Optional

//To get the Behavior Editor working, you can go to SisGame/Editor/BehaviorEditor/Characters/Sis
//	duplicate and then just replace Sis with Hammy

namespace SIS.Characters.Ham
{
	//
	public class Hammy : Character, IHittable
	{
		#region StateMachine Setup

		//Must Start Off in a State
		[SerializeField] private HammyState startingState;

		//Optional. Use StateActionComposite to run multiple actions on create
		[SerializeField] private HammyStateActions initActionsBatch;

		public StateMachine<Hammy> stateMachine;

		private new void Start()
		{
			base.Start();
			stateMachine = new StateMachine<Hammy>(this, startingState, initActionsBatch);
		}
		//Run State Machine Logic
		private void FixedUpdate()
		{
			if (IsDead()) return;
			stateMachine.FixedTick();
		}
		private void Update()
		{
			if (IsDead()) return;
			stateMachine.Tick();
		}
		public override void ChangeState(int transitionIndex)
		{
			var newState = stateMachine.currentState.transitions[transitionIndex].targetState;
			stateMachine.currentState = newState;
			stateMachine.currentState.OnEnter(this);
		}
		#endregion

		public SO.TransformVariable playerTransform;
		public ParticleSystem Explosion;
		public GameObject targetPrefab;
		public SO.TransformVariable cameraTransform;
		public AudioClip Slam;
		[SerializeField] public SO.FloatVariable maxHealth;

		[SerializeField] public SO.FloatVariable damage;
		[SerializeField] public SO.FloatVariable radius;
		[SerializeField] public SO.FloatVariable moveSpeed;
		[SerializeField] public SO.FloatVariable fleeSpeed;
		public float FallSpeed = 1;
		public float RotSpeed = 1;
		[HideInInspector] public GameObject targetInstance;
		[HideInInspector] public Transform FinTransform;
		[HideInInspector] public Transform HammerBaseTransform;
		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;
		[HideInInspector] public List<int> patrolRoute = new List<int>();
		[HideInInspector] public int patrolIndex = 0;
		[HideInInspector] public bool sameRoomAsPlayer;
		[HideInInspector] public bool wasHit;
		//Must atleast override since it is abstract
		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			//Hammy: TrajectorySystem.Init()
			waypointNavigator = GetComponent<Waypoints.WaypointNavigator>();
			FinTransform = mTransform.FindDeepChild("Fin Reference");
			HammerBaseTransform = mTransform.FindDeepChild("Hammer Base");
			health = maxHealth.value;
			anim.speed = 1.5f;
		}

		public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
		{
			wasHit = true;
			health -= baseDamage;
			rigid.AddForceAtPosition(dir, pos);

			if (onHitDelegate != null)
				onHitDelegate();

			if (health <= 0)
			{
				Collider[] colliders = GetComponentsInChildren<Collider>();
				foreach (Collider col in colliders) {
					col.isTrigger = true;
				}
				Destroy(targetInstance);
				StartCoroutine(Death());
			}
		}
		//Allows for game controller to mark enemy as dead
		public bool IsDead()
		{
			return health <= 0;
		}
		public void PlaySound(AudioClip audio)
		{
			audioSource.PlayOneShot(audio, 0.5f);
		}

		public void PlaySlam() {
			audioSource.PlayOneShot(Slam, 0.2f);
		}

		public int GetNextPatrolRoomIndex()
		{
			patrolIndex++;
			if (patrolIndex >= patrolRoute.Count) return -1;
			return patrolRoute[patrolIndex];
		}

		public void SpawnTargetAtPos(Vector3 pos) {
			targetInstance = Instantiate(targetPrefab, pos, Quaternion.identity);
			targetInstance.transform.localScale = new Vector3(radius.value, 0.1f, radius.value);
		}

		public void DestroyTarget() {
			Destroy(targetInstance);
			targetInstance = null;
		}

		public void SpawnExplosionAtPos(Vector3 pos) {
			Instantiate(Explosion, pos, Quaternion.Euler(-90, 0, 0));
			StartCoroutine(CameraShake(2));
		}

		public bool InAir() {
			return FinTransform.position.y > 0.1;
		}

		private IEnumerator Death() {
			anim.SetBool("Slam", false);
			rigid.velocity = Vector3.zero;
			float time = 5;
			while (time > 0) {
				time -= Time.deltaTime;
				mTransform.position += Vector3.down * FallSpeed * Time.deltaTime;
				mTransform.rotation *= Quaternion.Euler(RotSpeed * Time.deltaTime, 0, RotSpeed * Time.deltaTime);
				yield return null;
			}
			Destroy(gameObject);
		}

		public IEnumerator CameraShake(float time) {
			// Vector3[] dir = new Vector3 {Vector3.up, Vector3.down, Vector3.left, Vector3.right};
			while (time > 0) {
				time -= Time.deltaTime;
				// cameraTransform.value.position += Vector3.up;
				yield return null;
			}
		}

	}
}