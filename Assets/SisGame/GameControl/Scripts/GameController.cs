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
		public SO.GameEvent lostGameEvent;
		//public SO.GameEvent wonGameEvent;
		public StageListing stageListing;
		public StageDifficultyTuning stageDifficultyTuning;
		public SO.BoolVariable isPaused;
		public SO.BoolVariable inputPause;
		public SO.BoolVariable inGame;
		public Characters.Sis.SisVariable sis;
		public SO.IntVariable endlessScore;

		public enum State { InGame, Interlude, Won, Lost }
		public State gameState;

		private void Start()
		{
			stageIndexVar.value = 0;
			isPaused.value = false;
			gameState = State.InGame;
			
			StartCoroutine("StartTimer");
 		}

		private void Update()
		{
			if (enemyController.LastEnemyDied /*&& !stageListing.IsLastStage()*/)
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

			inGame.value = InGame();

			//Transitioning States
			if (sis.value != null)
			{
				if (sis.value.IsDead() && gameState != State.Lost)
				{
					gameState = State.Lost;
					lostGameEvent.Raise();
				}
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
			Debug.Log("Toggled pause");
			isPaused.value = !isPaused.value;
			Time.timeScale = (isPaused.value ? 0 : 1);
			Cursor.visible = isPaused.value;
			if (isPaused.value)
			{
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
			}
			else
			{
				if (!InGame())
				{
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;
				} else
				{
					Cursor.lockState = CursorLockMode.Locked;
					Cursor.visible = false;
				}
			}
		}

		public void TurnOffPaused()
		{
			Debug.Log("Turned off pause");
			isPaused.value = false;
			Time.timeScale = 1;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			/*
			if (InGame())
			{
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = true;
			}
			*/
		}

		public bool InGame()
		{
			if (gameState == State.InGame || gameState == State.Interlude)
				return true;
			return false;
		}

		public void StageReset()
		{
			if (stageListing.IsEndlessMode())
			{
				endlessScore.value = 0;
			}
			gameState = State.InGame;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		//Called by StageText HUD
		public void GameWon()
		{
			gameState = State.Won;
		}

		public void EnterEndlessMode()
		{
			endlessScore.value = 0;
			gameState = State.InGame;
			stageIndexVar.value++;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			startStageEvent.Raise();
		}
	}
}