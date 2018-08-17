using UnityEngine;
using SIS.Characters;

namespace SIS.Items.Weapons
{
	public abstract class Ballistics : ScriptableObject
	{
		public LayerMask ignoreLayers;

		public abstract void Execute(Character owner, Weapon weapon, Vector3 intendedDirection);
	}
}
