using UnityEngine;
using SIS.Characters;

namespace SIS.Items.Weapons
{
	public abstract class Ballistics : ScriptableObject
	{
		public LayerMask ignoreLayers;

		public abstract void Init(Weapon weapon);

		public abstract void Execute(Character owner, float baseDamage, Vector3 intendedDirection, Vector3 origin);
	}
}
