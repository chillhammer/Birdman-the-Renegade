using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Set Animation Bool")]
	public class SetAnimBool : StateActions<StateMachine>
	{
		public string targetBool;
		public bool status;

		public override void Execute(StateMachine owner)
		{
			if (owner.anim == null)
			{
				Debug.Log("No Animator Found in SetAnimBool");
				return;
			}
			owner.anim.SetBool(targetBool, status);

		}
	}
}
