using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Waypoints;
namespace SIS.Characters.Bill
{
    public class Bill : MonoBehaviour
    {
        WaypointNavigator waypointNavigator;
        CharacterController charactercontroller;
        // Use this for initialization
        void Start()
        {
            waypointNavigator = GetComponent<WaypointNavigator>();
            waypointNavigator.StartNavigation(5);
            charactercontroller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!waypointNavigator.PathFinished)
            {
                Vector3 dir = waypointNavigator.DirectionToWaypoint;
                charactercontroller.Move(dir * 3*Time.deltaTime);

            
            }

        }
    }
}