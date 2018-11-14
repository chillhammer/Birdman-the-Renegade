using UnityEngine;
using SIS.Characters;
using SIS.Items.Weapons;

//Allows for things to be hit
namespace SIS
{
	//General
	public interface IHittable
	{
		void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos);
		bool IsDead();
		void PlaySound(AudioClip audio);
		int GetScore();
	}

	//Generic
	public interface IHittable<C> where C : Character
	{
		void OnHit(C shooter, float baseDamage, Vector3 dir, Vector3 pos);
		bool IsDead();
		void PlaySound(AudioClip audio);
		int GetScore();
	}
}