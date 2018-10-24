using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using SIS.Items;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	public class Sis : Character, IHittable
	{
		#region StateMachine Setup
		[SerializeField] private SisState startingState;
		[SerializeField] private SisStateActions initActionsBatch;

		public StateMachine<Sis> stateMachine;

		private new void Start()
		{
			base.Start();
			stateMachine = new StateMachine<Sis>(this, startingState, initActionsBatch);

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

		//Serializable Properties
		public MovementValues movementValues;
		public Inventory inventory;

		#region MovementValues
		[System.Serializable]
		public class MovementValues
		{
			public float horizontal;
			public float vertical;
			public float moveAmount;
			public Vector3 moveDirection;
			public Vector3 lookDirection;
			public Vector3 aimPosition;
		}
		#endregion

		#region Flags and Toggles
		public bool isAiming;
		public bool isShooting;
		public bool isGunReady;
		public bool isCrouching;
		public bool isReloading;
		public bool isInteracting;

		public void ToggleCrouching()
		{
			isCrouching = !isCrouching;
		}
		public void SetReloading()
		{
			isReloading = true;
		}
		#endregion

		//Complex Components
		[HideInInspector]
		public IKAiming aiming;

		protected override void SetupComponents()
		{
		}

		//Public Functions
		public void PlayAnimation(string targetAnim)
		{
			anim.CrossFade(targetAnim, 0.2f);
		}

		public void OnHit(Character shooter, Weapon weapon, Vector3 dir, Vector3 pos)
		{
			rigid.AddForce(dir, ForceMode.Impulse);
		}
	}
}