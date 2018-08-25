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
		List<Room> rooms;

		public int RoomCount { get { return rooms.Count; } }

		//Override values from DungeonGenerator
		public void SetFromGeneration(Tile[] tiles, List<Room> rooms, WaypointSystem waypointSystem, 
			int width, int height)
		{
			this.tiles = tiles;
			this.rooms = rooms;
			this.waypointSystem = waypointSystem;
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
		#endregion
	}
}