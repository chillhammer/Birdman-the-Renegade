using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States
{
	[DisallowMultipleComponent]
    public abstract class StateMachine : MonoBehaviour
    {
        
        public State<StateMachine> currentState;


        [HideInInspector]
        public float delta;
        [HideInInspector]
        public Transform mTransform;
		[HideInInspector]
		public Rigidbody rigid;
		[HideInInspector]
		public Animator anim;

		public StateActions<StateMachine> initActionBatch;

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
