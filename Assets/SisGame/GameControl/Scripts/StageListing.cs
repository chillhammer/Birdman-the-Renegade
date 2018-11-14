using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace SIS.GameControl
{
	[CreateAssetMenu(menuName = "Game Control/Stage Listing")]
	public class StageListing : ScriptableObject
	{
		[SerializeField] StageListingDetail[] stages;
		public SO.IntVariable stageIndexVar;
		public StageListingDetail endlessModeDistribution;
		public int StagesLength { get { return stages.Length; } }
		public int TotalEnemies {
			get {
				if (IsEndlessMode() || stageIndexVar.value < 0)
					return 0;
				return stages[stageIndexVar.value].enemies.Length;
			}
		}


		public bool IsLastStage()
		{
			return (stages.Length - 1 == stageIndexVar.value);
		}

		public bool IsEndlessMode()
		{
			return (stageIndexVar.value >= stages.Length);
		}

		//First Enemy is At The End. Allows for them to be spawned in order for optimization
		public List<GameObject> GetStageEnemiesReversed()
		{
			if (stageIndexVar.value >= stages.Length || stageIndexVar.value < 0)
			{
				if (stageIndexVar.value >= stages.Length)
				{
					return endlessModeDistribution.enemies.OfType<GameObject>().ToList();
				}
				Debug.LogWarning("StageListing Trying to Access Out of Bounds!");
				return null;
			}
			List<GameObject> result = new List<GameObject>();
			StageListingDetail stage = stages[stageIndexVar.value];
			for (int i = stage.enemies.Length - 1; i >= 0; --i)
			{
				result.Add(stage.enemies[i]);
			}
			return result;
		}

		public int GetStageMaxAlive()
		{
			if (IsEndlessMode())
			{
				//Debug.LogWarning("StageListing Trying to Access Out of Bounds!");
				Debug.Log("Endless Mode!");
				return endlessModeDistribution.maxAlive;
			}
			return stages[stageIndexVar.value].maxAlive;
		}

		[System.Serializable]
		public struct StageListingDetail
		{
			public GameObject[] enemies;
			public int maxAlive;
		}
	}
}