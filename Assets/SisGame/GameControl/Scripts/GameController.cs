using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIS.GameControl
{
	//Pause is set by Game Controller. This just animates and controls step by step
	public class GameController : MonoBehaviour
	{
		public SO.IntVariable stageIndexVar;
		public Map.Enemy.EnemyController enemyController;
		public SO.GameEvent endStageEvent;
		public SO.GameEvent startStageEvent;
		public StageListing stageListing;
		public StageDifficultyTuning stageDifficultyTuning;
		public SO.BoolVariable isPaused;
		public SO.BoolVariable inputPause;

		private void Start()
		{
			stageIndexVar.value = 0;
			isPaused.value = false;
			
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

			if (inputPause.value)
			{
				TogglePaused();
			}
		}
		//Called by EventListener
		public void StageStarted()
		{
			stageDifficultyTuning.SetDifficultyTuning();

		}
		//Called by EventListener
		public void StageEnded()
		{

		}

		IEnumerator StartTimer()
		{
			yield return new WaitForSeconds(3f);
			startStageEvent.Raise(); //Makes Enemy Controller Spawn Enemies
		}

		public void TogglePaused()
		{
			isPaused.value = !isPaused.value;
			Time.timeScale = (isPaused.value ? 0 : 1);
			Cursor.visible = isPaused.value;
			if (isPaused.value)
			{
				Cursor.lockState = CursorLockMode.None;
			}
			else
			{
				Cursor.lockState = CursorLockMode.Locked;
			}
		}

		public void TurnOffPaused()
		{
			isPaused.value = false;
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}