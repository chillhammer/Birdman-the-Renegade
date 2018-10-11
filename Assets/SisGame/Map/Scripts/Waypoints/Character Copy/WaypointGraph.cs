using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SIS.Map;

namespace SIS.Waypoints
{
	//Used to Replicate Waypoint System
	public class WaypointGraph
	{
		List<List<WaypointNode>> nodesByRoom;
		WaypointSystem waypointSystem;
		Dungeon dungeon;

		private WaypointGraph() { }

		//Creates Shallow Copy Based Off Unique WaypointSystem
		//WaypointNode allows for unique meta data to help with AStar
		public WaypointGraph(WaypointSystem system, Dungeon dg)
		{
			dungeon = dg;
			waypointSystem = system;
			nodesByRoom = new List<List<WaypointNode>>();
			foreach (List<Waypoint> roomWaypoints in system.waypointsByRoom)
			{
				List<WaypointNode> roomNodes = new List<WaypointNode>();
				foreach (Waypoint wp in roomWaypoints)
				{
					roomNodes.Add(new WaypointNode(wp));
				}
				nodesByRoom.Add(roomNodes);
			}
		}

		//Maybe List<Waypoint> ?
		public List<Waypoint> AStar(Waypoint start, Waypoint goal)
		{
			WaypointNode goalNode = FindNode(goal);

			List<WaypointNode> open = new List<WaypointNode> { FindNode(start) };
			List<WaypointNode> visited = new List<WaypointNode>();

			int o = 0;
			while (open.Count > 0 && o++ < 100)
			{
				//Get Current Node By Smallest F. Possible heap optimization
				WaypointNode current = open[0];
				float smallestF = current.f;
				for (int i = 1; i < open.Count; ++i)
				{
					if (open[i].f < smallestF)
					{
						current = open[i];
						smallestF = open[i].f;
					}
				}
				open.Remove(current);
				visited.Add(current);

				//Termination Condition Check
				if (current == goalNode)
					return RetracePath(goalNode);

				//Add Neighbors
				foreach (Waypoint neighbor in current.Waypoint.connected)
				{
					WaypointNode neighborNode = FindNode(neighbor);

					float newGCost = current.g + WaypointNode.Distance(current, neighborNode);
					neighborNode.g = newGCost;

					if (visited.Contains(neighborNode))
						continue;

					//Update Best Path and Add Neighbors to Open List

					if (!open.Contains(neighborNode) || neighborNode.g < newGCost)
					{
						neighborNode.g = newGCost;
						neighborNode.h = WaypointNode.Distance(neighborNode, goalNode);
						neighborNode.cameFrom = current;

						if (!open.Contains(neighborNode))
							open.Add(neighborNode);
					}
				}
			}

			Debug.LogWarning("Failed to use AStar to find path");
			return new List<Waypoint>();
		}

		//Allows for AStar to pseudo access the WaypointSystem
		public Waypoint FindClosestWaypoint(Vector3 position)
		{
			return waypointSystem.FindClosestWaypoint(position);
		}

		//Allows for navigation to rooms
		public Waypoint GetCenterRoomWaypoint(int roomIndex)
		{
			return nodesByRoom[roomIndex][0].Waypoint;
		}



		//Helper Functions
		//Converts waypoint to node equivalent
		private WaypointNode FindNode(Waypoint wp)
		{
			int roomIndex = dungeon.GetRoomIndex(wp.X, wp.Y);
			foreach (WaypointNode node in nodesByRoom[roomIndex])
			{
				if (node.Waypoint.X == wp.X && node.Waypoint.Y == wp.Y)
					return node;
			}
			Debug.LogWarning("Could not find WaypointNode!");
			return null;
		}

		//Retrace from goal once finished AStar
		private List<Waypoint> RetracePath(WaypointNode goalNode)
		{
			List<Waypoint> path = new List<Waypoint>();
			WaypointNode current = goalNode;
			while (current != null)
			{
				path.Insert(0, current.Waypoint);
				WaypointNode next = current.cameFrom;
				current.cameFrom = null;
				current = next;
			}
			if (path.Count > 1)
				path.RemoveAt(0); //Removing First One -- test
			return path;
		}
	}
}