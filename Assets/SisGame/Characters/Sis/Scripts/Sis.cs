using UnityEngine;
using System.Collections;
using SIS.States;
using SIS.Items;

namespace SIS.Characters.Sis
{
	public class Sis : StateMachine
	{
		public float health;
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

		[HideInInspector]
		public LayerMask ignoreLayers;
		[HideInInspector]
		public IKAiming aiming;

		private new void Start()
		{
			base.Start();

			ignoreLayers = ~(1 << 10 | 1 << 9 | 1 << 8 | 1 << 3); //ignore player
		}
	}
}