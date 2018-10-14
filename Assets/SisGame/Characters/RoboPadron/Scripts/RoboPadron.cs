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

		#endregion

		public float health = 1;

		[HideInInspector] public Waypoints.WaypointNavigator waypointNavigator;

		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			waypointNavigator = GetComponent<Waypoints.WaypointNavigator>();
		}

		public void OnHit(Character shooter, Weapon weapon, Vector3 dir, Vector3 pos)
		{
			health--;
			rigid.AddForceAtPosition(dir, pos);

			if (health <= 0)
			{
				Destroy(gameObject);
			}
		}
	}
}