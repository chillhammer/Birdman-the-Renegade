using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
	[DisallowMultipleComponent]
    public abstract class StateMachine<C> : MonoBehaviour where C : Character
    {
        public State<c> currentState;


        [HideInInspector]
        public float delta;
        [HideInInspector]
        public Transform mTransform;
		[HideInInspector]
		public Rigidbody rigid;
		[HideInInspector]
		public Animator anim;

		public StateActions<C> initActionBatch;

        protected void Start()
        {
            mTransform = this.transform;
			rigid = GetComponent<Rigidbody>();
			anim = GetComponentInChildren<Animator>();

			initActionBatch.Execute(this);

			
			
        }

		private void FixedUpdate()
		{
			delta = Time.fixedDeltaTime;
			if (currentState != null)
			{
				currentState.FixedTick(this);
			}
		}

		private void Update()
        {
			delta = Time.deltaTime;
			if (currentState != null)
            {
                currentState.Tick(this);
            }
        }

		public void PlayAnimation(string targetAnim)
		{
			anim.CrossFade(targetAnim, 0.2f);
		}
    }
}
