using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Set Animation Bool")]
	public class SisSetAnimBool : SisStateActions
	{
		public SisAnimBool action;

		public override void Execute(Sis owner)
		{
			action.Execute(owner);
		}

		[System.Serializable]
		public class SisAnimBool : SetAnimBool<Sis, SisStateActions> { }
	}
}