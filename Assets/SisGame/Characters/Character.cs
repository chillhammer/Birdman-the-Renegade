using UnityEngine;
using System.Collections;
using SIS.States;

namespace SIS.Characters
{
	//Common 
	[DisallowMultipleComponent]
	public abstract class Character : MonoBehaviour
	{

		[HideInInspector]
		public Transform mTransform;
		[HideInInspector]
		public Rigidbody rigid;
		[HideInInspector]
		public Animator anim;
		[HideInInspector]
		public float delta;

		public float health;
		[HideInInspector] public delegate void OnHitDelegate();
		[HideInInspector] public OnHitDelegate onHitDelegate;

		protected virtual void Start()
		{
			mTransform = this.transform;
			rigid = GetComponent<Rigidbody>();
			anim = GetComponentInChildren<Animator>();
			SetupComponents();
		}

		protected abstract void SetupComponents();

		public abstract void ChangeState(int transitionIndex);
	}
}
