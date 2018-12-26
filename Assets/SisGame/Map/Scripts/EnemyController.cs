using UnityEngine;
using System.Collections.Generic;
using SIS.Characters;

namespace SIS.Map.Enemy {
	public class EnemyController : MonoBehaviour {

		public Dungeon dungeon;
		public Transform enemiesParent;
		public EnemyType[] enemies;
		public GameControl.StageListing stageListing;
		public bool useStageListing = true;
		public SO.IntVariable stageIndexVar;
		public bool LastEnemyDied { get; private set; }
		public AudioClip enemyKilledSound;
		public SO.TransformVariable playerTransform;
		public SO.IntVariable score;
		int enemiesKilled = 0;
		public int EnemiesKilled {
			get
			{
				return enemiesKilled;
				//return stageListing.TotalEnemies - toBeSpawnedEnemies.Count;
			}
		}

		List<IHittable> aliveEnemies;

		List<GameObject> toBeSpawnedEnemies;
		int maxAlive = 1;

		void Start() {
			aliveEnemies = new List<IHittable>();
			//SpawnEnemies();
		}

		//Debug Spawn
		void Update() {
			if (Input.GetKeyDown(KeyCode.V))
			{
				SpawnEnemies();
			}

			//Remove Dead Ones
			LastEnemyDied = false;
			for (int i = aliveEnemies.Count - 1; i >= 0; --i)
			{
				IHittable enemy = aliveEnemies[i];
				if (enemy.IsDead())
				{
					enemiesKilled++;
					if (stageListing.IsEndlessMode())
					{
						score.value += enemy.GetScore();
					}
					enemy.PlaySound(enemyKilledSound);
					aliveEnemies.RemoveAt(i);
					if (NoEnemiesToSpawn())
						LastEnemyDied = true;
				}
			}

			//Spawn From Stage Listing
			if (useStageListing)
			{
				if (toBeSpawnedEnemies != null)
				{
					if (toBeSpawnedEnemies.Count > 0 && aliveEnemies.Count < maxAlive)
					{
						if (!stageListing.IsEndlessMode())
						{
							//Regular Spawning
							SpawnEnemy(toBeSpawnedEnemies[toBeSpawnedEnemies.Count - 1]);
							toBeSpawnedEnemies.RemoveAt(toBeSpawnedEnemies.Count - 1);
						} else
						{
							//Endless Spawning
							SpawnEnemy(toBeSpawnedEnemies[Random.Range(0, toBeSpawnedEnemies.Count)]);
						}
					}
				}
			}
		}



		#region Debug Method
		void SpawnEnemies()
		{
			foreach (EnemyType enemyType in enemies)
			{
				if (enemyType.enemy == null)
				{
					Debug.LogWarning("Enemy Type cannot be null when spawning");
					continue;
				}
				Room room = dungeon.GetRandomRoomNotAdjacent(playerTransform.value.position);
				Vector3 spawnPos = new Vector3(room.rect.center.x, enemyType.elevation, room.rect.center.y);
				for (int i = 0; i < enemyType.number; ++i)
				{
					GameObject enemy = Instantiate(enemyType.enemy, spawnPos, Quaternion.identity, enemiesParent);
					IHittable enemyHittable = enemy.GetComponent<IHittable>();
					if (enemyHittable != null)
						aliveEnemies.Add(enemyHittable);

					if (!enemyType.sameRoom)
					{
						room = dungeon.GetRandomRoom();
						spawnPos = new Vector3(room.rect.center.x, enemyType.elevation, room.rect.center.y);
					}
				}
			}
		}
		#endregion

		//Used to spawn in random room
		public void SpawnEnemy(GameObject enemyObject)
		{
			Room room = dungeon.GetRandomRoomNotAdjacent(playerTransform.value.position);
			Vector3 spawnPos = new Vector3(room.rect.center.x, .2f, room.rect.center.y);
			GameObject enemy = Instantiate(enemyObject, spawnPos, Quaternion.identity, enemiesParent);
			IHittable enemyHittable = enemy.GetComponent<IHittable>();
			if (enemyHittable != null)
				aliveEnemies.Add(enemyHittable);
		}

		bool NoEnemiesToSpawn()
		{
			return aliveEnemies.Count == 0 && toBeSpawnedEnemies.Count == 0;
		}

		public void StageStarted()
		{
			if (aliveEnemies.Count > 0)
			{
				Debug.LogWarning("Stage started but more than 0 alive enemies.");
				aliveEnemies.Clear(); //Just to be safe
			}
			enemiesKilled = 0;
			toBeSpawnedEnemies = stageListing.GetStageEnemiesReversed();
			maxAlive = stageListing.GetStageMaxAlive();
		}

		[System.Serializable]
		public struct EnemyType
		{
			public GameObject enemy;
			[Range(1, 20)]
			public int number;
			public bool sameRoom;
			[Range(0,2)]
			public float elevation;
		}
	}
}