using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Managers
{
	public class InitManagers : MonoBehaviour
	{
		private void Awake()
		{
			AudioManager a = GameManagers.AudioManager;
			Destroy(gameObject);
		}
	}
}