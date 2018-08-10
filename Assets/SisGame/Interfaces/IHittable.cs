using UnityEngine;
using SIS.States;
using SIS.Items.Weapons;

//Allows for 
namespace SIS
{
	//General
	interface IHittable
	{
		void OnHit(StateMachine shooter, Weapon weapon, Vector3 dir, Vector3 pos);
	}

	//Generic
	interface IHittable<M> where M : StateMachine
	{
		void OnHit(M shooter, Weapon weapon, Vector3 dir, Vector3 pos);
	}
}