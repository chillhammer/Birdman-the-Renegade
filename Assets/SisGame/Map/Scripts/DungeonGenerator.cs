using System.Collections.Generic;
using UnityEngine;

public enum Tile { None, Floor, Wall }
public enum Direction { North, East, South, West, NumOfDirections }

public class DungeonGenerator : MonoBehaviour
{
	public WaypointSystem waypointSystem;
	public GameObject dungeonParent;
	public GameObject block;
	public GameObject floor;
	public GameObject player;
	public GameObject enemy;

	public const int WIDTH = 40;
	public const int HEIGHT = 40;

	public int roomMin = 3;
	public int roomMax = 12;

	Tile[] _tiles;
	Dictionary<Tile, GameObject> tileObjects;
	List<Rect> potentialExits;
	List<Rect> rooms;

	// Use this for initialization
	void Start()
	{
		SetupObjects();
		Generate();
		SpawnObjects();
	}

	private void Generate()
	{
		_tiles = new Tile[WIDTH * HEIGHT];
		potentialExits = new List<Rect>();
		rooms = new List<Rect>();
		//Fill
		for (int i = 0; i < WIDTH * HEIGHT; ++i)
		{
			_tiles[i] = Tile.Wall;
		}

		GenerateRoom(true);
		for (int i = 0; i < 16; ++i)
		{
			GenerateRoom();
		}
	}

	private void GenerateRoom(bool isFirstRoom = false)
	{
		Rect room;
		Vector2Int oldRoomEnt = Vector2Int.zero;
		Vector2Int newRoomEnt = Vector2Int.zero;
		Direction? dir = null;
		if (isFirstRoom)
		{
			//First Room
			room = new Rect(new Vector2(WIDTH * 0.5f, HEIGHT * 0.5f), new Vector2(10, 10));

			//Player
			PlaceObject((int)(WIDTH * 0.5f) + 2, (int)(HEIGHT * 0.5f) + 2, player, 1f);
		}
		else
		{
			//Find Appropriate Spot for Room
			int attempts = 0;
			int ex = 0, ey = 0;
			do
			{
				//Find Exit
				int iExit = Random.Range(0, potentialExits.Count - 1);
				Rect exit = potentialExits[iExit];
				ex = (int)exit.x + Random.Range(0, (int)exit.width - 1);
				ey = (int)exit.y + Random.Range(0, (int)exit.height - 1);

				//Generate Room Properties
				Vector2 dimensions = new Vector2(Random.Range(roomMin, roomMax), Random.Range(roomMin, roomMax));
				dir = (Direction)Random.Range(0, (int)Direction.NumOfDirections);

				//Calculate Room Coordinates.
				int rx = 0, ry = 0;
				switch (dir)
				{
					case Direction.North:
						rx = ex - (int)(dimensions.x * 0.5);
						ry = ey - (int)dimensions.y;
						oldRoomEnt = new Vector2Int(ex, ey + 1);
						newRoomEnt = new Vector2Int(ex, ey - 1);
						break;
					case Direction.East:
						rx = ex + 1;
						ry = ey - (int)(dimensions.y * 0.5);
						oldRoomEnt = new Vector2Int(ex - 1, ey);
						newRoomEnt = new Vector2Int(ex + 1, ey);
						break;
					case Direction.South:
						rx = ex - (int)(dimensions.x * 0.5);
						ry = ey + 1;
						oldRoomEnt = new Vector2Int(ex, ey - 1);
						newRoomEnt = new Vector2Int(ex, ey + 1);
						break;
					case Direction.West:
						rx = ex - (int)dimensions.x;
						ry = ey - (int)(dimensions.y * 0.5);
						oldRoomEnt = new Vector2Int(ex + 1, ey);
						newRoomEnt = new Vector2Int(ex - 1, ey);
						break;
				}

				room = new Rect(new Vector2(rx, ry), dimensions);

			} while (!RectFilled(room) && ++attempts < 100);
			if (attempts == 100)
			{
				Debug.LogWarning("Cannot Generate Room");
				return;
			}
			//Mark Exit
			SetTile(ex, ey, Tile.Floor);
			//Largen Exit
			int dif = (int)((Random.Range(0, 2) - 0.5f) * 2f); // -1 or 1
			dif = -1;
			if (dir == Direction.North || dir == Direction.South)
				SetTile(ex + dif, ey, Tile.Floor);
			else
				SetTile(ex, ey + dif, Tile.Floor);
		}
		rooms.Add(room);
		waypointSystem.AddWaypointsByRoom(room);
		FillRect(room);

		//Connect Room Waypoints
		if (!isFirstRoom)
		{
			int iOldRoom = GetRoomIndex(oldRoomEnt);
			int iNewRoom = GetRoomIndex(newRoomEnt);
			waypointSystem.AddWaypointsByHall(oldRoomEnt, iOldRoom, newRoomEnt, iNewRoom);
		}

		AddPotentialExits(room, dir);
	}

	private void AddPotentialExits(Rect room, Direction? dir)
	{
		int pad = 2;
		Rect northSide = new Rect(room.xMin + pad, room.yMin - 1, room.width - pad, 1);
		Rect eastSide = new Rect(room.xMax, room.yMin + pad, 1, room.height - pad);
		Rect southSide = new Rect(room.xMin + pad, room.yMax, room.width - pad, 1);
		Rect westSide = new Rect(room.xMin - 1, room.yMin + pad, 1, room.height - pad);

		if (dir != Direction.South) potentialExits.Add(northSide);
		if (dir != Direction.West) potentialExits.Add(eastSide);
		if (dir != Direction.North) potentialExits.Add(southSide);
		if (dir != Direction.East) potentialExits.Add(westSide);
	}

	private void SpawnObjects()
	{
		Vector3 gridOffset = new Vector3(1f, 0f, 1f);
		//GameObject bigFloor = Instantiate(floor, Vector3.zero, Quaternion.identity, dungeonParent.transform);
		//bigFloor.transform.localScale = new Vector3(WIDTH, 0, HEIGHT);

		for (int r = 0; r < HEIGHT; ++r)
		{
			for (int c = 0; c < WIDTH; ++c)
			{
				GameObject obj;
				if (tileObjects.TryGetValue(GetTile(c, r), out obj))
				{
					Vector3 objPos = new Vector3(gridOffset.x * c, 0, gridOffset.z * r);
					Instantiate(obj, objPos, Quaternion.identity, dungeonParent.transform);
				}
			}
		}

		//Edges
		for (int r = 0; r < HEIGHT; ++r)
		{
			Vector3 objPos = new Vector3(gridOffset.x * -1, 0, gridOffset.z * r);
			Instantiate(tileObjects[Tile.Wall], objPos, Quaternion.identity, dungeonParent.transform);

			objPos = new Vector3(gridOffset.x * WIDTH, 0, gridOffset.z * r);
			Instantiate(tileObjects[Tile.Wall], objPos, Quaternion.identity, dungeonParent.transform);
		}

		for (int c = 0; c < WIDTH; ++c)
		{
			Vector3 objPos = new Vector3(gridOffset.x * c, 0, gridOffset.z * -1);
			Instantiate(tileObjects[Tile.Wall], objPos, Quaternion.identity, dungeonParent.transform);

			objPos = new Vector3(gridOffset.x * c, 0, gridOffset.z * HEIGHT);
			Instantiate(tileObjects[Tile.Wall], objPos, Quaternion.identity, dungeonParent.transform);
		}

		//Enemy
		GameObject enemyInst = PlaceObject((int)(WIDTH * 0.5f) + 3, (int)(HEIGHT * 0.5f) + 3, enemy, 1f);
		WaypointNavigator enemyNavigator = enemyInst.GetComponent<WaypointNavigator>();
		enemyNavigator.waypointGraph = new WaypointGraph(waypointSystem, this);
		enemyNavigator.dungeonGenerator = this;
		enemyNavigator.StartNavigation(14);


		enemyInst = PlaceObject((int)(WIDTH * 0.5f) + 2, (int)(HEIGHT * 0.5f) + 2, enemy, 1f);
		enemyNavigator = enemyInst.GetComponent<WaypointNavigator>();
		enemyNavigator.waypointGraph = new WaypointGraph(waypointSystem, this);
		enemyNavigator.dungeonGenerator = this;
		enemyNavigator.StartNavigation(14);

	}

	private void SetupObjects()
	{
		tileObjects = new Dictionary<Tile, GameObject>();
		tileObjects.Add(Tile.Floor, floor);
		tileObjects.Add(Tile.Wall, block);
	}

	//Private Helpers
	private void FillRect(Rect rect, Tile tile = Tile.Floor)
	{
		for (int r = 0; r < rect.height; ++r)
		{
			for (int c = 0; c < rect.width; ++c)
			{
				int tx = (int)rect.x + c;
				int ty = (int)rect.y + r;
				SetTile(tx, ty, tile);
			}
		}
	}

	//Check if Rect is filled with certain tile
	private bool RectFilled(Rect rect, Tile tile = Tile.Wall)
	{
		for (int r = 0; r < rect.height; ++r)
		{
			for (int c = 0; c < rect.width; ++c)
			{
				int tx = (int)rect.x + c;
				int ty = (int)rect.y + r;
				if (GetTile(tx, ty) != tile) return false;
			}
		}
		return true;
	}

	//Instantiate GameObject at Tile
	private GameObject PlaceObject(int x, int y, GameObject obj, float elevation = 0f)
	{
		Vector3 gridOffset = new Vector3(1f, 0f, 1f);
		return Instantiate(obj, new Vector3(gridOffset.x * x, elevation, gridOffset.z * y), Quaternion.identity);
	}

	private void SetTile(int x, int y, Tile tile)
	{
		int index = x + y * WIDTH;
		if (index >= _tiles.Length || index < 0) return;
		_tiles[index] = tile;
	}

	//Public Accessors
	public Tile GetTile(int x, int y)
	{
		int index = x + y * WIDTH;
		if (x >= WIDTH || x < 0 || y >= HEIGHT || y < 0) return Tile.None;
		return _tiles[index];
	}

	public Rect GetRoom(int x, int y)
	{
		int index = GetRoomIndex(x, y);
		if (index == -1) return Rect.zero;
		return rooms[index];
	}

	public int GetRoomIndex(int x, int y)
	{
		if (x >= WIDTH || x < 0 || y >= HEIGHT || y < 0) return -1;
		int index = 0;
		Vector2 pos = new Vector2(x, y);
		foreach (Rect room in rooms)
		{
			if (room.Contains(pos))
			{
				return index;
			}
			++index;
		}
		return -1;
	}

	public int GetRoomIndex(Vector2Int pos)
	{
		return GetRoomIndex(pos.x, pos.y);
	}
}
