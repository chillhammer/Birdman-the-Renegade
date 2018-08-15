using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	//[CreateAssetMenu(menuName = "Actions/State Actions/Set Animation Bool")]
	public class SetAnimBool<C, SA> where C : Character
									where SA : StateActions<C>
	{
		public string targetBool;
		public bool status;

		public void Execute(C owner)
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
