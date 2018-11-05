using UnityEngine;
using System.Collections.Generic;

namespace SIS.GameControl
{
	[CreateAssetMenu(menuName = "Game Control/Stage Difficulty Tuning")]
	public class StageDifficultyTuning : ScriptableObject
	{
		[SerializeField] StageDifficultyDetail[] stages;
		public SO.IntVariable stageIndexVar;
		public SO.FloatVariable roboFireRateVar;
		public SO.FloatVariable roboChaseSpeedVar;
		public SO.FloatVariable roboMaxHealthVar;
		public SO.FloatVariable roboShootRangeVar;
		public SO.FloatVariable roboShootRangeMaxVar;


		public void SetDifficultyTuning()
		{
			if (stageIndexVar.value < 0 || stageIndexVar.value >= stages.Length)
			{
				Debug.LogWarning("StageDifficultyTuning accessing stage out of bounds!");
				return;
			}
			StageDifficultyDetail current = stages[stageIndexVar.value];
			roboFireRateVar.value = current.roboFireRate;
			roboChaseSpeedVar.value = current.roboChaseSpeed;
			roboMaxHealthVar.value = current.roboMaxHealth;
			roboShootRangeVar.value = current.roboShootRange;
			roboShootRangeMaxVar.value = current.roboShootRangeMax;
		}

		[System.Serializable]
		public struct StageDifficultyDetail
		{
			public float roboFireRate;
			public float roboChaseSpeed;
			public float roboMaxHealth;
			public float roboShootRange;
			public float roboShootRangeMax;
		}
	}
}