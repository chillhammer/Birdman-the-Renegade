using UnityEngine;
using SIS.States;

namespace SIS.Items.Weapons
{
	public abstract class Ballistics : ScriptableObject
	{
		public LayerMask ignoreLayers;

		public abstract void Execute(StateMachine owner, Weapon weapon, Vector3 intendedDirection);
	}
}
