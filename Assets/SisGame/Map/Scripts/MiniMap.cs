using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SIS.Waypoints;
using UnityEngine.UI;
using SIS.GameControl;

namespace SIS.Map
{
	// GUI for MiniMap
	public class MiniMap : MonoBehaviour
	{
		[SerializeField]
		private Dungeon dungeon;

		[SerializeField]
		private SO.TransformVariable sisTransform;

		[SerializeField]
		private GameObject enemiesParent;
		//private Enemy.EnemyController enemyController;


		[SerializeField]
		private Sprite wallSprite;

		Image[] imageTiles;
		List<Image> enemyTiles;
		Image background;
		Image player;

		public void Start()
		{
			imageTiles = new Image[dungeon.WIDTH * dungeon.HEIGHT];

			background = gameObject.AddComponent<Image>();
			background.rectTransform.sizeDelta = new Vector2(dungeon.WIDTH, dungeon.HEIGHT);
			Color backgroundColor = Color.black; backgroundColor.a = 0.5f;
			background.color = backgroundColor;

			for (int i = 0; i < imageTiles.Length; ++i)
			{
				int tx = i % dungeon.WIDTH;
				int ty = i / dungeon.WIDTH;
				if (dungeon.GetTile(tx, ty) == Tile.Wall)
				{
					imageTiles[i] = CreateChildImage("MiniMap Wall Icon");
					imageTiles[i].sprite = wallSprite;
					imageTiles[i].rectTransform.localPosition = new Vector3(tx, ty);
					imageTiles[i].rectTransform.localScale = new Vector3(1, 1);
					imageTiles[i].rectTransform.sizeDelta = new Vector2(1, 1);
					imageTiles[i].rectTransform.anchorMin = Vector2.zero;
					imageTiles[i].rectTransform.anchorMax = Vector2.zero;
				}
			}

			//Player
			player = CreateChildImage("MiniMap Player Icon");
			player.sprite = wallSprite;
			player.color = Color.cyan;
			NeutralImage(ref player);

			//Enemies
			enemyTiles = new List<Image>(10);
		}

		private void LateUpdate()
		{
			if (sisTransform.value != null)
			{
				if (player != null)
					player.rectTransform.localPosition = new Vector3(sisTransform.value.localPosition.x - dungeon.WIDTH * 0.5f, 
						sisTransform.value.localPosition.z - dungeon.HEIGHT * 0.5f, 0);
			}
			else
				Debug.Log("No SisTransform for MiniMap");

			//Enemy
			for (int i = 0; i < enemiesParent.transform.childCount; ++i)
			{
				Transform enemy = enemiesParent.transform.GetChild(i);
				
				if (i >= enemyTiles.Count)
				{
					enemyTiles.Add(CreateChildImage("Mini Map Enemy Icon"));
					if (i == enemyTiles.Count)
					{
						Image image = enemyTiles[i];
						NeutralImage(ref image); //Assumming always i == enemyTiles.Count
						enemyTiles[i] = image;
					}
				}

				Image enemyTile = enemyTiles[i];
				NeutralImage(ref enemyTile);
				enemyTile.color = Color.red;
				enemyTile.rectTransform.localPosition = new Vector3(enemy.position.x - dungeon.WIDTH * 0.5f,
						enemy.position.z - dungeon.HEIGHT * 0.5f, 0);
				enemyTiles[i] = enemyTile;

				IHittable hittable = enemy.GetComponent<IHittable>();
				if (hittable != null && hittable.IsDead())
				{
					Debug.Log("Dead!");
					Color nothing = Color.white; nothing.a = 0;
					enemyTile.color = nothing;
				}
			}

			for (int i = enemiesParent.transform.childCount; i < enemyTiles.Count; ++i)
			{
				Image enemyTile = enemyTiles[i];
				Color nothing = Color.white; nothing.a = 0;
				enemyTile.color = nothing;
			}
		}

		// Creates child with image component
		Image CreateChildImage(string name)
		{
			GameObject child = new GameObject(name);
			child.transform.parent = transform;
			return child.AddComponent<Image>();
		}

		void NeutralImage(ref Image image)
		{
			image.rectTransform.localScale = new Vector3(1, 1);
			image.rectTransform.sizeDelta = new Vector2(1, 1);
			image.rectTransform.anchorMin = Vector2.zero;
			image.rectTransform.anchorMax = Vector2.zero;
		}
	}
}