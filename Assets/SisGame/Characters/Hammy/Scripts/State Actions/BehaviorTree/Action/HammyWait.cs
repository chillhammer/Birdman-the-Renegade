using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	//StateAction that waits
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyWait")]
	public class HammyWait : HammyBTAction
	{
		public float SecondsToWait = 1f;
		float timer = 0;

		public override AivoTree.AivoTreeStatus Act(float timeTick, Hammy owner)
		{
			timer += Time.deltaTime;
			if (timer > SecondsToWait)
			{
				timer = 0;
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Running;
		}
	}
}