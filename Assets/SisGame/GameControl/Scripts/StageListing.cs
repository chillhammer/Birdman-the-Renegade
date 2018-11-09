using UnityEngine;
using System.Collections.Generic;

namespace SIS.GameControl
{
	[CreateAssetMenu(menuName = "Game Control/Stage Listing")]
	public class StageListing : ScriptableObject
	{
		[SerializeField] StageListingDetail[] stages;
		public SO.IntVariable stageIndexVar;

		public bool IsLastStage()
		{
			return (stages.Length - 1 == stageIndexVar.value);
		}

		//First Enemy is At The End. Allows for them to be spawned in order
		public List<GameObject> GetStageEnemiesReversed()
		{
			if (stageIndexVar.value >= stages.Length || stageIndexVar.value < 0)
			{
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
			if (stageIndexVar.value >= stages.Length)
			{
				Debug.LogWarning("StageListing Trying to Access Out of Bounds!");
				return 1;
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