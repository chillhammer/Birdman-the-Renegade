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
		public bool isAiming = false;
		public bool canSeePlayer = false;
		[HideInInspector] public bool transitionToWander = false;

		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;
		[HideInInspector] public Vision vision;

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

			if (headBone == null) Debug.LogWarning("Could not find Head bone on RoboPadron");
		}

		public void OnHit(Character shooter, Weapon weapon, Vector3 dir, Vector3 pos)
		{
			if (health == 0)
				return;
			health--;
			if (onHitDelegate != null)
				onHitDelegate();
			rigid.AddForceAtPosition(dir, pos);

			if (health <= 0)
			{
				//Destroy(gameObject);
			}
		}

		public void OnDrawGizmosSelected()
		{
			Gizmos.DrawWireSphere(playerLastKnownLocation.position, 0.3f);
		}
	}
}