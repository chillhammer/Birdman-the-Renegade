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
		public AudioClip Hurt;
		public AudioClip Fall;
		public AudioClip Unburrow;
		[SerializeField] public SO.FloatVariable maxHealth;

		[SerializeField] public SO.FloatVariable damage;
		[SerializeField] public SO.FloatVariable radius;
		[SerializeField] public SO.FloatVariable moveSpeed;
		[SerializeField] public SO.FloatVariable fleeSpeed;
		public float FallSpeed = 1;
		public float RotSpeed = 1;
		[HideInInspector] public GameObject targetInstance;
		[HideInInspector] public Transform ModelTransform;
		[HideInInspector] public Transform TailTransform;
		[HideInInspector] public Transform FinTransform;
		[HideInInspector] public Transform HammerBaseTransform;
		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;
		[HideInInspector] public List<int> patrolRoute = new List<int>();
		[HideInInspector] public int patrolIndex = 0;
		[HideInInspector] public bool sameRoomAsPlayer;
		[HideInInspector] public bool wasHit;
		[HideInInspector] public float waitTimer = 0; //Used in HammyWait
		//Must atleast override since it is abstract
		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			//Hammy: TrajectorySystem.Init()
			waypointNavigator = GetComponent<Waypoints.WaypointNavigator>();
			FinTransform = mTransform.FindDeepChild("Fin Reference");
			HammerBaseTransform = mTransform.FindDeepChild("Hammer Base");
			TailTransform = mTransform.FindDeepChild("tail_base");
			ModelTransform = mTransform.FindDeepChild("Hammy Model");
			health = maxHealth.value;
			anim.speed = 1.5f;
		}

		public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
		{
			wasHit = true;
			health -= baseDamage;
			rigid.AddForceAtPosition(dir, pos);
			PlaySound(Hurt);

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
		public int GetScore()
		{
			return 20;
		}
		public void PlaySound(AudioClip audio)
		{
			audioSource.PlayOneShot(audio, 0.5f);
		}

		public void PlaySlam() {
			audioSource.PlayOneShot(Slam, 0.2f);
		}

		public void PlayUnburrow()
		{
			audioSource.PlayOneShot(Unburrow, 0.1f);
		}

		public int GetNextPatrolRoomIndex()
		{
			patrolIndex++;
			if (patrolIndex >= patrolRoute.Count) return -1;
			return patrolRoute[patrolIndex];
		}

		public void SpawnTargetAtPos(Vector3 pos) {
			targetInstance = Instantiate(targetPrefab, pos, Quaternion.identity);
			//targetInstance.transform.localScale = new Vector3(radius.value, 0.1f, radius.value);
			targetInstance.GetComponent<HammyTargetGrowth>().scaleTarget = radius.value;
		}

		public void DestroyTarget() {
			if (targetInstance != null)
				Destroy(targetInstance);
			targetInstance = null;
		}

		public void SpawnExplosionAtPos(Vector3 pos) {
			Instantiate(Explosion, pos, Quaternion.Euler(-90, 0, 0));
			StartCoroutine(CameraShake(0.4f));
		}
		public bool InAir() {
			return (TailTransform.position.y > 1);
		}
		private IEnumerator Death() {
			DestroyTarget();
			anim.enabled = false;
			float time = 0.5f;
			rigid.velocity = Vector3.zero;
			yield return new WaitForSeconds(time);
			/*
			float time = 2;
			while (time > 0) {
				time -= Time.deltaTime;
				if (!InAir()) mTransform.position += Vector3.up * FallSpeed * Time.deltaTime * 0.3f;
				ModelTransform.RotateAround(TailTransform.position, ModelTransform.forward, Time.deltaTime * RotSpeed);
				yield return null;
			}
			*/
			PlaySound(Fall);
			time = 2;
			Vector3 newPos = mTransform.position;
			newPos.y = -2f;
			if (InAir())
				newPos.y = -8f;
			while (time > 0) {
				time -= Time.deltaTime;
				//mTransform.position += Vector3.down * 2 * FallSpeed * Time.deltaTime;

				mTransform.position = Vector3.Lerp(mTransform.position, newPos, 2f * Time.deltaTime);

				yield return null;
			}
			
			Destroy(gameObject);
		}

		public IEnumerator CameraShake(float time) {
			Vector3[] dir = new Vector3[4];
			dir[0] = Vector3.up;
			dir[1] = Vector3.down;
			dir[2] = Vector3.left;
			dir[3] = Vector3.right;
			while (time > 0) {
				time -= Time.deltaTime;
				int ind = Random.Range(0,3);
				cameraTransform.value.position += dir[ind] * 0.1f;
				yield return null;
			}
		}

	}
}