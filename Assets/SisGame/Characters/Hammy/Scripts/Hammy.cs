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
			stateMachine.FixedTick();
		}
		private void Update()
		{
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
		[HideInInspector] public Transform FinTransform;

		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;
		[HideInInspector] public List<int> patrolRoute = new List<int>();
		[HideInInspector] public int patrolIndex = 0;
		[HideInInspector] public bool sameRoomAsPlayer;
		//Must atleast override since it is abstract
		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			//Hammy: TrajectorySystem.Init()
			waypointNavigator = GetComponent<Waypoints.WaypointNavigator>();
			FinTransform = mTransform.FindDeepChild("Fin Reference");
		}

		public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
		{
			health -= baseDamage;
			rigid.AddForceAtPosition(dir, pos);

			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}

		public int GetNextPatrolRoomIndex()
		{
			patrolIndex++;
			if (patrolIndex >= patrolRoute.Count) return -1;
			return patrolRoute[patrolIndex];
		}
	}
}