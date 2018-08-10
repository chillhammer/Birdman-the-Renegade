using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions
{
    public class ActionHook : MonoBehaviour
    {
		public Action[] fixedUpdateActions;
		public Action[] updateActions;
		public Action[] lateActions;

		void FixedUpdate() {
			RunAllActions(fixedUpdateActions);
		}

		void Update() {
			RunAllActions(updateActions);
        }

		void LateUpdate() {
			RunAllActions(lateActions);
		}


		void RunAllActions(Action[] actions)
		{
			if (actions == null)
				return;

			for (int i = 0; i < actions.Length; i++)
			{
				actions[i].Execute();
			}
		}
	}
}
