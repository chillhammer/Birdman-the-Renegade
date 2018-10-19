using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;


namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammySetAnimBool")]
    public class HammySetAnimBool : HammyBTAction
    {
		public string BoolName;
		public bool ActivateBool;

        // Use this for initialization
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.rigid.velocity = Vector3.zero; // shouldnt be here but...
			Debug.Log(BoolName + " " + ActivateBool);
			owner.anim.SetBool(BoolName, ActivateBool);
			return AivoTreeStatus.Success;
        }
    }
}