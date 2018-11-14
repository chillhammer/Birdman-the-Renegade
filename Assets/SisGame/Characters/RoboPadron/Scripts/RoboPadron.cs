using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using SIS.Items;
using SIS.Items.Weapons;

namespace SIS.Characters.Robo
{
	[RequireComponent(typeof(Waypoints.WaypointNavigator))]
	public class RoboPadron : Character, IHittable
	{
		#region StateMachine Setup

		//Must Start Off in a State
		[SerializeField] private RoboPadronState startingState;

		//Optional. Use StateActionComposite to run multiple actions on create
		[SerializeField] private RoboPadronStateActions initActionsBatch;

		public StateMachine<RoboPadron> stateMachine;

		private new void Start()
		{
			base.Start();
			stateMachine = new StateMachine<RoboPadron>(this, startingState, initActionsBatch);

		}
		//Run State Machine Logic
		private void FixedUpdate()
		{
			stateMachine.FixedTick();
		}
		private void Update()
		{
			stateMachine.Tick();
			delta = stateMachine.delta;
		}
		public override void ChangeState(int transitionIndex)
		{
			var newState = stateMachine.currentState.transitions[transitionIndex].targetState;
			stateMachine.currentState = newState;
			stateMachine.currentState.OnEnter(this);
		}

		#endregion

		[HideInInspector] public Transform headBone;
		[HideInInspector] public Transform gunModel;
		[HideInInspector] public Transform gunTip;
		[HideInInspector] public ParticleSystem bulletSystem;
		[HideInInspector] public ParticleProjectileOnHit projectileOnHit;
		[SerializeField] AudioClip onHitSound;
		[SerializeField] AudioClip foostepSound;
		[SerializeField] SO.FloatVariable maxHealth;
		public bool isAiming = false;
		public bool canSeePlayer = false;
		[HideInInspector] public bool transitionToWander = false;

		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;
		[HideInInspector] public Vision vision;
		[HideInInspector] public float visionTimer;
		[HideInInspector] public float shootTimer;

		#region Last Known Position
		[System.Serializable]
		public struct LastKnownLocation
		{
			public Vector3 position;
			public float timeSeen;
		}
		#endregion
		#region Death Fall Variables
		[System.Serializable]
		public struct DeathFallProgress
		{
			public Quaternion originalRotation;
			public Vector3 fallTowards;
			public float timer;
		}
		#endregion

		//Serializable Fields
		public LastKnownLocation playerLastKnownLocation;
		public DeathFallProgress deathFallProgress;

		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			waypointNavigator = GetComponent<Waypoints.WaypointNavigator>();
			vision = GetComponent<Vision>();
			headBone = mTransform.FindDeepChild("Head");
			gunModel = mTransform.FindDeepChild("Gun");
			bulletSystem = gunModel.GetComponentInChildren<ParticleSystem>();
			projectileOnHit = bulletSystem.GetComponent<ParticleProjectileOnHit>();
			health = maxHealth.value;

			if (headBone == null) Debug.LogWarning("Could not find Head bone on RoboPadron");
		}

		public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
		{
			if (health == 0)
				return;
			health -= baseDamage;
			if (health < 0) health = 0;
			if (onHitDelegate != null)
				onHitDelegate();
			rigid.AddForceAtPosition(dir * 2, pos);
			audioSource.PlayOneShot(onHitSound);

			playerLastKnownLocation.position = shooter.mTransform.position;
			playerLastKnownLocation.timeSeen = Time.frameCount;

			canSeePlayer = true;
			//NOTE: Death Is Handled by Transitions
		}
		//Allows for game controller to mark enemy as dead
		public bool IsDead()
		{
			return health == 0;
		}

		public int GetScore()
		{
			return 30;
		}

		public void PlaySound(AudioClip audio)
		{
			audioSource.PlayOneShot(audio);
		}

		public void OnFootstep()
		{
			if (rigid.velocity.magnitude > 0.7f)
				audioSource.PlayOneShot(foostepSound, 0.05f);

		}


		public void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(playerLastKnownLocation.position, 0.3f);
		}

		//Called from DeathFall
		IEnumerator Die()
		{
			yield return new WaitForSeconds(5f);

			float time = 1f;
			Vector3 targetPos = mTransform.position;
			targetPos.y = mTransform.position.y - 1;
			GetComponent<Collider>().enabled = false;
			while (time > 0)
			{
				time -= delta;
				mTransform.position = Vector3.Lerp(mTransform.position, targetPos, 0.5f * delta);
				yield return null;
			}
			Destroy(gameObject);
		}

		
	}
}