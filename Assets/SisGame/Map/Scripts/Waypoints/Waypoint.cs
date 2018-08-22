using UnityEngine;
using System.Collections.Generic;

namespace SIS.Waypoints
{
	public struct Waypoint
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public List<Waypoint> connected;

		public Waypoint(int x, int y)
		{
			X = x;
			Y = y;
			connected = new List<Waypoint>();
		}

		public Waypoint(Vector2Int pos) : this(pos.x, pos.y)
		{
		}


	}
}