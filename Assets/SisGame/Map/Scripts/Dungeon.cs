using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using SIS.Waypoints;

namespace SIS.Map
{
	[CreateAssetMenu(menuName = "Dungeon")]
	public class Dungeon : ScriptableObject
	{
		[HideInInspector]
		public WaypointSystem waypointSystem;

		[HideInInspector] public int WIDTH;
		[HideInInspector] public int HEIGHT;

		Tile[] tiles;
		[SerializeField] List<Room> rooms;
		public List<List<Waypoint>> waypointsByRoomCache; //only to keep waypoint data

		public int RoomCount { get { return rooms.Count; } }
		public Tile[] Tiles { get { return tiles; } }

		public List<Room> Rooms { get { return rooms; } }

		//Override values from DungeonGenerator
		public void SetFromGeneration(Tile[] tiles, List<Room> rooms, WaypointSystem waypointSystem,
			int width, int height)
		{
			this.tiles = tiles;
			this.rooms = rooms;
			this.waypointSystem = waypointSystem;
			waypointsByRoomCache = waypointSystem.waypointsByRoom;
			WIDTH = width;
			HEIGHT = height;
		}

		#region Public Accessors
		public Tile GetTile(int x, int y)
		{
			int index = x + y * WIDTH;
			if (x >= WIDTH || x < 0 || y >= HEIGHT || y < 0) return Tile.None;
			return tiles[index];
		}

		public Room GetRoom(int x, int y)
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

		public int GetRoomIndex(int x, int y)
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

		public int GetRoomIndex(Vector2Int pos)
		{
			return GetRoomIndex(pos.x, pos.y);
		}

		//If you are in hall or outside map
		public int GetClosestRoomIndex(int x, int y)
		{
			int index = 0;
			int closestRoomIndex = 0;
			Vector2 pos = new Vector2(x, y);
			float bestDist = float.MaxValue;
			foreach (Room room in rooms)
			{
				Vector2 center = room.rect.center;
				float dist = Vector2.Distance(center, pos);
				if (dist < bestDist)
				{
					bestDist = dist;
					closestRoomIndex = index;
				}
				++index;
			}
			return closestRoomIndex;
		}

		public int GetClosestRoomIndex(Vector3 transformPosition)
		{
			return GetClosestRoomIndex((int)transformPosition.x, (int)transformPosition.z);
		}

			public int GetClosestRoomIndex(Vector2Int pos)
		{
			return GetClosestRoomIndex(pos.x, pos.y);
		}

		public Room GetRandomRoom()
		{
			int roomIndex = Random.Range(0, rooms.Count);
			return rooms[roomIndex];
		}

		public Room GetRandomRoomNotAdjacent(Vector3 transformPosition)
		{
			Room room = GetRoom((int)transformPosition.x, (int)transformPosition.z);
			if (room == null)
				return GetRandomRoom();

			int tries = 15;
			int tryIndex = 0;
			while (tryIndex < tries)
			{
				++tryIndex;
				Room randomRoom = GetRandomRoom();
				if (randomRoom != room)
				{
					foreach (Room adjacent in room.connected)
					{
						if (adjacent == randomRoom)
							continue;
					}
					return randomRoom;
				}
			}
			Debug.LogWarning("Failed to find not adjacent room");
			return GetRandomRoom();
		}

		#endregion
	}
}