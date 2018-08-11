using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Managers {
	public class CharacterStateManager
	{
		public List<StateType> StateTypes { get; private set; }

		CharacterStateManager()
		{
			StateTypes = new List<StateType>();
		}

		void AddCharacterState(Type state, string stateName)
		{
			StateTypes.Add(new StateType(stateName, state));
		}

		public struct StateType {
			string name;
			Type type;

			public StateType(string name, Type type)
			{
				this.name = name;
				this.type = type;
			}
		}
	}
}