using System.Collections.Generic;
using UnityEngine;
using SIS.Waypoints;
using System.Linq;

public enum Tile { None, Floor, Wall, Doorframe}
public enum Direction { North, East, South, West, NumOfDirections }

namespace SIS.Map
{
	public class DungeonGenerator : MonoBehaviour
	{
		public Dungeon dungeon;
		public GameObject dungeonParent;
		public GameObject wall;
		public GameObject corner;
		public GameObject floor;
		public GameObject player;

		#region Settings
		[SerializeField] private bool generateNewDungeon = true;
		[SerializeField] private int WIDTH = 40;
		[SerializeField] private int HEIGHT = 40;

		[SerializeField] private int roomMin = 3;
		[SerializeField] private int roomMax = 12;
		[SerializeField] private int roomAmount = 16;
		[SerializeField] private int firstRoomWidth = 12;
		[SerializeField] private int firstRoomHeight =12;
		#endregion

		Tile[] tiles;
		List<Room> rooms;
		WaypointSystem waypointSystem;
		List<Rect> potentialExits;

		// Use this for initialization
		private void Awake()
		{
			SetupObjects();
			Generate();
			SpawnObjects();
		}

		#region Generation Helpers
		private void Generate()
		{
			if (!generateNewDungeon)
			{
				tiles = dungeon.Tiles;
				return;
			}
			tiles = new Tile[WIDTH * HEIGHT];
			potentialExits = new List<Rect>();
			rooms = new List<Room>();
			//Fill
			for (int i = 0; i < WIDTH * HEIGHT; ++i)
			{
				tiles[i] = Tile.Wall;
			}

			GenerateRoom(true);
			for (int i = 0; i < roomAmount - 1; ++i)
			{
				GenerateRoom();
			}

			//Transfer to Dungeon ScriptableObject
			dungeon.SetFromGeneration(tiles, rooms, waypointSystem, WIDTH, HEIGHT);
		}

		private void GenerateRoom(bool isFirstRoom = false)
		{
			Room room;
			Vector2Int oldRoomEnt = Vector2Int.zero;
			Vector2Int newRoomEnt = Vector2Int.zero;
			Direction? dir = null;
			if (isFirstRoom)
			{
				//First Room
				room = new Room(WIDTH / 2, HEIGHT / 2, firstRoomWidth, firstRoomHeight);

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
							oldRoomEnt = new Vector2Int(ex, ey - 1);
							newRoomEnt = new Vector2Int(ex, ey + 2);
							break;
						case Direction.East:
							rx = ex + 1;
							ry = ey - (int)(dimensions.y * 0.5);
							oldRoomEnt = new Vector2Int(ex - 1, ey);
							newRoomEnt = new Vector2Int(ex + 2, ey);
							break;
						case Direction.South:
							rx = ex - (int)(dimensions.x * 0.5);
							ry = ey + 1;
							oldRoomEnt = new Vector2Int(ex, ey + 2);
							newRoomEnt = new Vector2Int(ex, ey - 1);
							break;
						case Direction.West:
							rx = ex - (int)dimensions.x;
							ry = ey - (int)(dimensions.y * 0.5);
							oldRoomEnt = new Vector2Int(ex + 2, ey);
							newRoomEnt = new Vector2Int(ex - 1, ey);
							break;
					}

					Room sourceRoom = GetRoom(oldRoomEnt.x, oldRoomEnt.y);
					room = new Room(new Vector2(rx, ry), dimensions, sourceRoom);

				} while (!RectFilled(room.rect) && ++attempts < 100);
				if (attempts == 100)
				{
					Debug.LogWarning("Cannot Generate Room");
					return;
				}
				//Mark Exit
				SetTile(ex, ey, Tile.Doorframe);
				//Largen Exit
				int dif = -1;
				if (dir == Direction.North || dir == Direction.South)
					SetTile(ex + dif, ey, Tile.Doorframe);
				else
					SetTile(ex, ey + dif, Tile.Doorframe);
			}
			rooms.Add(room);
			waypointSystem.AddWaypointsByRoom(room.rect);
			FillRect(room.rect);

			//Connect Room Waypoints
			if (!isFirstRoom)
			{
				int iOldRoom = GetRoomIndex(oldRoomEnt);
				int iNewRoom = GetRoomIndex(newRoomEnt);
				waypointSystem.AddWaypointsByHall(oldRoomEnt, iOldRoom, newRoomEnt, iNewRoom);
			}

			AddPotentialExits(room.rect, dir);
			AddEmergentRoomConnections(room); //Connects emergent rooms and their waypoints
		}

		//After adding room, keep track of edges to add more rooms
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

		//After adding room, look at edges to connect rooms, then connect if possible. also their waypoints
		private void AddEmergentRoomConnections(Room room)
		{
			int x1 = (int)room.rect.x;
			int y1 = (int)room.rect.y;
			int x2 = (int)room.rect.x + (int)room.rect.width;
			int y2 = (int)room.rect.y + (int)room.rect.height;
			for (int r = y1; r < y2; ++r)
			{
				CheckPotentialConnection(room, r, x1 - 1);

				CheckPotentialConnection(room, r, x2);
			}

			for (int c = x1; c < x2; ++c)
			{
				CheckPotentialConnection(room, y1 - 1, c);

				CheckPotentialConnection(room, y2, c);
			}
		}

		//Submethod of AddEmergentRoomConections, checks at specific location
		private void CheckPotentialConnection(Room room, int r, int c)
		{
			if (GetTile(r, c) == Tile.Floor)
			{
				int potentialRoomIndex = GetRoomIndex(c, r);

				if (potentialRoomIndex != -1)
				{
					Room potentialRoom = rooms[potentialRoomIndex];

					if (!room.connected.Contains(potentialRoom)) {
						ConnectRooms(room, potentialRoom);
						waypointSystem.ConnectWaypointsByOpening(GetRoomIndex(room), potentialRoomIndex, c, r);
					}
				}
			}
		}
		#endregion

		private void SpawnObjects()
		{
			SpawnFloor();
			GameObject wallParent = new GameObject("Walls");
			wallParent.transform.parent = dungeonParent.transform;

			//Loop Through Tiles
			for (int r = 0; r < HEIGHT; ++r)
			{
				for (int c = 0; c < WIDTH; ++c)
				{
					if (GetTile(c, r) == Tile.Wall)
					{
						bool cornerSpawned = SpawnCornerIfPossible(c, r, wallParent.transform);
						if (!cornerSpawned)
						{
							SpawnWall(c, r, wallParent.transform);
						}
					}
				}
			}

			SpawnOuterEdges(wallParent.transform);

			wallParent.AddComponent<CombineChildren>();

			SpawnPlayer();

		}

		//Spawns Player. Useful for respawning from GameControl
		public GameObject SpawnPlayer()
		{
			//Player
			return PlaceObject((int)(WIDTH * 0.5f) + 2, (int)(HEIGHT * 0.5f) + 2, player, 0.5f);
		}

		private void SpawnWall(int c, int r, Transform parent)
		{
			Vector3 objPos = new Vector3(c, 0, r);
			GameObject obj = Instantiate(wall, objPos, Quaternion.identity, parent);
			obj.layer = LayerMask.NameToLayer("Map");
		}

		///<summary>
		///Spawns corner with given rotation and x, z displacement
		///x, z displacement required to fit the corner into grid as origin of corner is at its corner
		///</summary>
		private void SpawnCorner(int c, int r, Transform parent, Quaternion rotation, int xOffset, int zOffset)
		{
			Vector3 objPos = new Vector3(c + xOffset, 0, r + zOffset);
			GameObject obj = Instantiate(corner, objPos, rotation, parent);
			obj.layer = LayerMask.NameToLayer("Map");
		}

		///<summary>
		///Check if a given tile at c, r is a corner and spawns if it is
		///Returns if corner was spawned
		///Assumes that given tile is a Tile.Wall
		///</summary>
		private bool SpawnCornerIfPossible(int c, int r, Transform parent)
		{
			Tile[] neighbors = GetTileNeighbors(c, r);
			int walls = 0;
			int wallType = neighbors.Aggregate(0, (b, tile) => {
				if (tile == Tile.Wall) {
					b = b | 1;
					walls++;
				}
				return b << 1;
			});
			wallType /= 2;

			if (walls != 2)
			{
				// not a corner
				return false;
			}

			if (c == 0 || c == HEIGHT - 1 || r == 0 || r == WIDTH - 1)
			{
				// can't decide if corner if tile is at edge of Dungeon due to outerwall
				// TODO: account for the outer wall
				return false;
			}

			// flags to determine orientation of corner
			const int northEastCorner = 3, northWestCorner = 6, southEastCorner = 9, southWestCorner = 12;

			switch (wallType)
			{
				case northEastCorner:
					SpawnCorner(c, r, parent, Quaternion.Euler(0, -90, 0), 1, 0);
					return true;
				case northWestCorner:
					SpawnCorner(c, r, parent, Quaternion.Euler(0, 180, 0), 1, 1);
					return true;
				case southEastCorner:
					SpawnCorner(c, r, parent, Quaternion.Euler(0, 0, 0), 0, 0);
					return true;
				case southWestCorner:
					SpawnCorner(c, r, parent, Quaternion.Euler(0, 90, 0), 0, 1);
					return true;
				default:
					return false;
			}
		}

		private void SpawnFloor()
		{
			Instantiate(floor, Vector3.zero, Quaternion.identity, dungeonParent.transform);
			//floorGO.transform.localScale = new Vector3(WIDTH, 0, HEIGHT);
		}

		private void SpawnOuterEdges(Transform parent)
		{
			for (int r = 0; r < HEIGHT; ++r)
			{
				Vector3 objPos = new Vector3(-1, 0, r);
				Instantiate(wall, objPos, Quaternion.identity, parent);

				objPos = new Vector3(WIDTH, 0, r);
				Instantiate(wall, objPos, Quaternion.identity, parent);
			}

			for (int c = 0; c < WIDTH; ++c)
			{
				Vector3 objPos = new Vector3(c, 0, -1);
				Instantiate(wall, objPos, Quaternion.identity, parent);

				objPos = new Vector3(c, 0, HEIGHT);
				Instantiate(wall, objPos, Quaternion.identity, parent);
			}
		}

		private void SetupObjects()
		{
			waypointSystem = new GameObject("Waypoint System").AddComponent<WaypointSystem>();
			if (generateNewDungeon)
				waypointSystem.Init(dungeon);
			else
				waypointSystem.Init(dungeon, dungeon.waypointsByRoomCache);

		}

		#region  Private Helpers
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
			return Instantiate(obj, new Vector3(x, elevation, y), Quaternion.identity);
		}

		private void SetTile(int x, int y, Tile tile)
		{
			int index = x + y * WIDTH;
			if (index >= tiles.Length || index < 0) return;
			tiles[index] = tile;
		}

		//Connect rooms. no worries about duplicates since connected is HashSet
		private void ConnectRooms(Room room1, Room room2)
		{
			room1.connected.Add(room2);
			room2.connected.Add(room1);
		}


		//Duplicate Code from Dungeon, Only needed for generation.
		//Should not be public or accessed
		private Tile GetTile(int x, int y)
		{
			int index = x + y * WIDTH;
			if (x >= WIDTH || x < 0 || y >= HEIGHT || y < 0) return Tile.None;
			return tiles[index];
		}

		///<summary>
		///Returns adjacent neighbors of a tile at position x, y
		///in clockwise order, starting from the north neighbor
		///</summary>
		private Tile[] GetTileNeighbors(int x, int y)
		{
			Tile[] neighbors = new Tile[] {
				GetTile(x, y + 1),
				GetTile(x + 1, y),
				GetTile(x, y - 1),
				GetTile(x - 1, y)
			};
			return neighbors;
		}

		private Room GetRoom(int x, int y)
		{
			int index = GetRoomIndex(x, y);
			if (index == -1) return null;
			return rooms[index];
		}

		//Converts room to its index
		public int GetRoomIndex(Room room)
		{
			for (int i = 0; i < rooms.Count; ++i)
			{
				if (rooms[i] == room)
					return i;
			}
			return -1;
		}

		private int GetRoomIndex(int x, int y)
		{
			if (x >= WIDTH || x < 0 || y >= HEIGHT || y < 0) return -1;
			int index = 0;
			Vector2 pos = new Vector2(x, y);
			foreach (Room room in rooms)
			{
				if (room.rect.Contains(pos))
				{
					return index;
				}
				++index;
			}
			return -1;
		}

		private int GetRoomIndex(Vector2Int pos)
		{
			return GetRoomIndex(pos.x, pos.y);
		}
		#endregion
	}
}