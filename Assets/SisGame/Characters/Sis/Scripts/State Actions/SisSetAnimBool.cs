using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Set Animation Bool")]
	public class SisSetAnimBool : SetAnimBool<Sis> { }
}