using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace SIS.Waypoints
{
	public class WaypointNode
	{
		public WaypointNode cameFrom;
		public float g, h;
		public float f { get { return g + h; } }

		Waypoint waypoint;
		public Waypoint Waypoint { get { return waypoint; } }

		public WaypointNode(Waypoint wp)
		{
			waypoint = wp;

			cameFrom = null;
			g = 0;
			h = 0;
		}

		public static float Distance(WaypointNode wp1, WaypointNode wp2)
		{
			return Vector2Int.Distance(new Vector2Int(wp1.waypoint.X, wp1.waypoint.Y),
				new Vector2Int(wp2.waypoint.X, wp2.waypoint.Y));
			//return Mathf.Abs(wp1.waypoint.X - wp2.waypoint.X) + Mathf.Abs(wp1.waypoint.Y - wp2.waypoint.Y);
		}
	}
}