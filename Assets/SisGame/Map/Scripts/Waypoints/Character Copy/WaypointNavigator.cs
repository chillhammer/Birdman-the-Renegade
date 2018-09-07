using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Map;

//Must Be Created Dynamically By Dungeon Generator

namespace SIS.Waypoints
{
	//API that allows for characters to move through waypoint system
	//Use CurrentWaypoint to find what to navigate to
	public class WaypointNavigator : MonoBehaviour
	{

		public Dungeon dungeon;
		public float nextWaypointDistCheck = 1.2f;
		public bool automaticWaypointUpdate = true; //updates current waypoint

		WaypointGraph waypointGraph;
		List<Waypoint> path;
		[SerializeField] int pathIndex = -1; //current waypoint index

		public Waypoint CurrentWaypoint
		{
			get
			{
				if (path == null || pathIndex == -1 || pathIndex >= path.Count)
					return new Waypoint();
				return path[pathIndex];
			}
		}

		public Vector3 DirectionToWaypoint
		{
			get
			{
				Vector3 target = new Vector3(CurrentWaypoint.X, 0, CurrentWaypoint.Y);
				return (target - transform.position).normalized;
			}
		}

		public bool PathFinished
		{
			get
			{
				return pathIndex == -1;
			}
		}

		private void Start()
		{
			if (waypointGraph == null) {
				waypointGraph = new WaypointGraph(dungeon.waypointSystem, dungeon);
				pathIndex = -1;
			}
		}

		private void Update()
		{
			if (automaticWaypointUpdate)
				CheckNextWaypoint();

			//Debug
			if (Input.GetKeyDown(KeyCode.C))
			{
				int roomIndex = Random.Range(0, dungeon.RoomCount);
				StartNavigation(roomIndex);
			}

			//Draw Debug Path Lines
			if (path != null)
			{
				for (int i = 0; i < path.Count - 1; ++i)
				{
					Debug.DrawLine(new Vector3(path[i].X, 0f, path[i].Y),
						new Vector3(path[i + 1].X, 0f, path[i + 1].Y), Color.red);
				}
			}
		}

		//Move Towards Current Waypoint
		public void CheckNextWaypoint()
		{
			if (path == null) return;
			if (pathIndex < 0 || pathIndex >= path.Count) return;

			//Progressing Waypoints
			Vector3 target = new Vector3(CurrentWaypoint.X, 0, CurrentWaypoint.Y);
			if (Vector3.Distance(transform.position, target) < nextWaypointDistCheck)
			{
				++pathIndex;
				if (pathIndex >= path.Count)
				{
					pathIndex = -1;
				}
			}
		}

		//Custom Waypoint Graph
		public void SetWaypointGraph(WaypointSystem waypointSystem, Dungeon dg)
		{
			waypointGraph = new WaypointGraph(waypointSystem, dg);
		}
		public void SetWaypointGraph()
		{
			waypointGraph = new WaypointGraph(dungeon.waypointSystem, dungeon);
		}

		//Actually sets path and initiates path index
		public void StartNavigation(int goalX, int goalY)
		{
			if (waypointGraph == null)
			{
				Debug.LogWarning("Cant Start Navigation, due to Waypoint Graph not being initialized yet");
				return;
			}
			Waypoint start = waypointGraph.FindClosestWaypoint(transform.position);
			Waypoint goal = waypointGraph.FindClosestWaypoint(new Vector3(goalX, 0f, goalY));

			path = waypointGraph.AStar(start, goal);
			pathIndex = 0;
		}
		#region StartNavigation Overloads

		public void StartNavigation(Vector3 goalPos)
		{
			StartNavigation((int)goalPos.x, (int)goalPos.z);
		}

		public void StartNavigation(Vector2Int goalPos)
		{
			StartNavigation(goalPos.x, goalPos.y);
		}

		public void StartNavigation(Waypoint wp)
		{
			StartNavigation(wp.X, wp.Y);
		}

		public void StartNavigation(int roomIndex)
		{
			if (roomIndex < 0 || roomIndex >= dungeon.RoomCount)
			{
				Debug.LogWarning("Room Index Out of Bound");
				return;
			}
			if (waypointGraph == null)
			{
				Debug.LogWarning("Cant Start Navigation, due to Waypoint Graph not being initialized yet");
				return;
			}

			StartNavigation(waypointGraph.GetCenterRoomWaypoint(roomIndex));
			
		}
		#endregion


	}
}