using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Waypoints
{
	public class WaypointSystem : MonoBehaviour
	{

		public List<List<Waypoint>> waypointsByRoom;

		private void Awake()
		{
			waypointsByRoom = new List<List<Waypoint>>();
		}

		// Update is called once per frame
		void Update()
		{

		}

		public void AddWaypointsByRoom(Rect room)
		{
			int pad = 1;
			int x1 = (int)(room.x + pad);
			int y1 = (int)(room.y + pad);
			int x2 = (int)(room.x + room.width - pad);
			int y2 = (int)(room.y + room.height - pad);
			List<Waypoint> roomWaypoints = new List<Waypoint>();
			roomWaypoints.Add(new Waypoint((x1 + x2) / 2, (y1 + y2) / 2));
			roomWaypoints.Add(new Waypoint(x1, y1));
			roomWaypoints.Add(new Waypoint(x2, y1));
			roomWaypoints.Add(new Waypoint(x2, y2));
			roomWaypoints.Add(new Waypoint(x1, y2));
			for (int i = 0; i < roomWaypoints.Count - 1; ++i)
			{
				for (int o = i + 1; o < roomWaypoints.Count; o++)
				{
					ConnectWaypoints(roomWaypoints[i], roomWaypoints[o]);
				}
			}
			waypointsByRoom.Add(roomWaypoints);
		}

		public void AddWaypointsByHall(Vector2Int oldRoomPos, int iOldRoom, Vector2Int newRoomPos, int iNewRoom)
		{
			if (iOldRoom == -1)
			{
				Debug.LogWarning("Index Of Old Room is Negative");
				return;
			}
			if (iNewRoom == -1)
			{
				Debug.LogWarning("Index Of New Room is Negative");
				return;
			}
			//Connect Entrance Beginning to all Waypoints
			Waypoint oldWp = new Waypoint(oldRoomPos);
			foreach (Waypoint wp in waypointsByRoom[iOldRoom])
			{
				ConnectWaypoints(wp, oldWp);
			}
			waypointsByRoom[iOldRoom].Add(oldWp);

			//Connect Entrance End to all Waypoints
			Waypoint newWp = new Waypoint(newRoomPos);
			foreach (Waypoint wp in waypointsByRoom[iNewRoom])
			{
				ConnectWaypoints(wp, newWp);
			}
			waypointsByRoom[iNewRoom].Add(newWp);

			ConnectWaypoints(oldWp, newWp);
		}

		void ConnectWaypoints(Waypoint w1, Waypoint w2)
		{
			w1.connected.Add(w2);
			w2.connected.Add(w1);
		}

		private void OnDrawGizmosSelected()
		{
			foreach (List<Waypoint> room in waypointsByRoom)
			{
				foreach (Waypoint wp in room)
				{
					Gizmos.DrawWireSphere(new Vector3(wp.X, 0, wp.Y), 0.5f);
					foreach (Waypoint connected in wp.connected)
					{
						Gizmos.DrawLine(new Vector3(wp.X, 0, wp.Y), new Vector3(connected.X, 0, connected.Y));
					}
				}
			}
		}
	}
}