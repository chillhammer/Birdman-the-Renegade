using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SIS.Characters.Sis;

namespace SIS.GameControl
{
	//When You lose the game, reset game
	public class RetryStage : MonoBehaviour
	{

		[SerializeField] LostGameFX lostGameFX;
		[SerializeField] GameController gameController;

		[SerializeField] GameObject enemiesParent;
		[SerializeField] Map.DungeonGenerator dungeonGenerator;
		[SerializeField] Characters.Sis.SisVariable sis;
		[SerializeField] SO.GameEvent stageStartEvent;
		[SerializeField] Characters.CharacterHealthBarController healthbar;


		RectTransform lostTransform;
		
		
		public void ResetStage()
		{
			lostGameFX.ResetVariables();
			for (int i = 0; i < enemiesParent.transform.childCount; ++i)
			{
				Transform enemy = enemiesParent.transform.GetChild(i);
				//Hot fix
				Characters.Ham.Hammy hammy = enemy.GetComponent<Characters.Ham.Hammy>();
				if (hammy != null)
				{
					hammy.DestroyTarget();
				}
				Destroy(enemy.gameObject);
			}
			Destroy(sis.value.gameObject);


			gameController.StageReset();
			Sis player = dungeonGenerator.SpawnPlayer().GetComponent<Sis>();
			healthbar.SetCharacter(player);
			healthbar.UpdateWidth();
			stageStartEvent.Raise();
		}

		

	}
}