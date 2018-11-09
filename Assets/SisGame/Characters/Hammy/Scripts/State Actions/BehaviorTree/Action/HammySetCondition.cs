using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySetCondition")]
	public class HammySetCondition : HammyBTAction
	{
		public bool SetValue;
		public HammySetableCondition condition;
		public override AivoTreeStatus Act(float timeTick, Hammy owner)
		{
			Debug.Log("condition set");
			condition.Value = SetValue;
			return AivoTree.AivoTreeStatus.Success;
		}
	}
}
