using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions.Input;

namespace SIS.Managers
{
	[DisallowMultipleComponent]
	public class CursorManager : MonoBehaviour
	{
		[SerializeField] private InputAxis mouseX;
		[SerializeField] private InputAxis mouseY;


		// Use this for initialization
		void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}