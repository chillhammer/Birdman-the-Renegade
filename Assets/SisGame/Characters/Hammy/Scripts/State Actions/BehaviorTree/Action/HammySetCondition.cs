using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AivoTree;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySetCondition")]
    public class HammySetCondtion : HammyBTAction
    {
		public bool SetTo;
		public HammySetableCondition setableCondition;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			setableCondition.Value = SetTo;
			return AivoTree.AivoTreeStatus.Success;
        }
    }
}
