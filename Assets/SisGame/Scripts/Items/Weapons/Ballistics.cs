using UnityEngine;
using SIS.States;

namespace SIS.Items.Weapons
{
	public abstract class Ballistics<M> : ScriptableObject where M : StateMachine
	{
		public abstract void Execute(M owner, Weapon weapon);
	}
}
