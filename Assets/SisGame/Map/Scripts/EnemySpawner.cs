using UnityEngine;

namespace SIS.Map.EnemySpawner {
	public class EnemySpawner : MonoBehaviour {

		public Dungeon dungeon;
		public Transform enemiesParent;
		public EnemyType[] enemies;

		void Start() {
			SpawnEnemies();
		}

		//Debug Spawn
		void Update() {
			if (Input.GetKeyDown(KeyCode.V))
			{
				SpawnEnemies();
			}
		}

		void SpawnEnemies()
		{
			foreach (EnemyType enemyType in enemies)
			{
				if (enemyType.enemy == null)
				{
					Debug.LogWarning("Enemy Type cannot be null when spawning");
					continue;
				}
				Room room = dungeon.GetRandomRoom();
				Vector3 spawnPos = new Vector3(room.rect.center.x, enemyType.elevation, room.rect.center.y);
				for (int i = 0; i < enemyType.number; ++i)
				{
					Instantiate(enemyType.enemy, spawnPos, Quaternion.identity, enemiesParent);

					if (!enemyType.sameRoom)
					{
						room = dungeon.GetRandomRoom();
						spawnPos = new Vector3(room.rect.center.x, enemyType.elevation, room.rect.center.y);
					}
				}
			}
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