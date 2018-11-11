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

		public SO.FloatVariable hammyMaxHealth;
		public SO.FloatVariable hammyRadius;
		public SO.FloatVariable hammyDamage;
		public SO.FloatVariable hammySpeed;
		public SO.FloatVariable hammyTurnSpeed;

		public SO.FloatVariable billAngrySpeed;

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
			hammyMaxHealth.value = current.hammyMaxHealth;
			hammyRadius.value = current.hammyRadius;
			hammyDamage.value = current.hammyDamage;
			hammySpeed.value = current.hammySpeed;
			hammyTurnSpeed.value = current.hammyTurnSpeed;
			billAngrySpeed.value = current.billAngrySpeed;
		}

		[System.Serializable]
		public struct StageDifficultyDetail
		{
			public float roboFireRate;
			public float roboChaseSpeed;
			public float roboMaxHealth;
			public float roboShootRange;
			public float roboShootRangeMax;

			public float hammySpeed;
			public float hammyTurnSpeed;
			public float hammyRadius;
			public float hammyDamage;
			public float hammyMaxHealth;

			public float billAngrySpeed;
		}
	}
}