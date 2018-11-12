using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SIS.Menu
{
	public class ExitButtonFunctionality : MonoBehaviour
	{
		public void ExitGame()
		{
			Application.Quit();
		}
	}
}