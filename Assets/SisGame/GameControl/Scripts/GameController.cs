using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIS.GameControl
{
	public class GameController : MonoBehaviour
	{
		public SO.IntVariable stageIndexVar;
		public Map.Enemy.EnemyController enemyController;
		public SO.GameEvent endStageEvent;
		public SO.GameEvent startStageEvent;
		public StageListing stageListing;

		private void Start()
		{
			stageIndexVar.value = 0;

			StartCoroutine("StartTimer");
		}

		private void Update()
		{
			if (enemyController.LastEnemyDied && !stageListing.IsLastStage())
			{
				endStageEvent.Raise();
			}

			if (Input.GetKeyDown(KeyCode.F12))
			{
				SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			}
		}

		public void StageEnded()
		{

		}

		IEnumerator StartTimer()
		{
			yield return new WaitForSeconds(3f);
			startStageEvent.Raise(); //Makes Enemy Controller Spawn Enemies
		}

	}
}