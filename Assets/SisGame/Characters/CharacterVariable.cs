using UnityEngine;
using System.Collections;

namespace SIS.Characters
{
	[CreateAssetMenu(menuName = "Characters/Character Variable")]
	public class CharacterVariable : ScriptableObject
	{
		public Character value;
	}
}