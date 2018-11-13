using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AivoTree;

namespace SIS.Characters.Ham
{
    [CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySpawnTarget")]
    public class HammySpawnTarget : HammyBTAction
    {
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
            Vector3 position = owner.playerTransform.value.position;
			position.y = 0;
			owner.SpawnTargetAtPos(position);
			owner.PlayUnburrow(); //unburrow sound
			return AivoTree.AivoTreeStatus.Success;
        }
    }
}
