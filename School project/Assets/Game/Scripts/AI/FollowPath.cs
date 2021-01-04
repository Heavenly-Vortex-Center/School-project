using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {
    //public enum FPM { Forwards, Backwards, PingPong, Random };
    public class FollowPath : Behaviour
    {
        int waypointIndex = 0;
        float waypointDistance;
        GameObject[] waypointPositions;

        public override void start( BehaviourContext context ) {
            base.start(context);
            waypointIndex = 0;
            waypointPositions = GameObject.FindGameObjectsWithTag("Waypoint");
            Debug.Log(waypointPositions.Length);
        }
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
           
            //////////-----------------------------------------------------------------------------------------------------------
            
            if (context._settings._followPathMode == SteeringSettings.FPM.Forwards) {

                if (waypointDistance < 2f) {
                    waypointIndex++;
                }
                if (waypointIndex == waypointPositions.Length ) {
                    waypointIndex = 0;
                }
                _positionTarget = waypointPositions[waypointIndex].transform.position;
            }
            
            //////////-----------------------------------------------------------------------------------------------------------
            if (context._settings._followPathMode == SteeringSettings.FPM.Backwards) {
                if (waypointDistance < 2f) {
                    
                    if (waypointIndex <= 0) {
                        waypointIndex = waypointPositions.Length - 1;
                    } else {
                        waypointIndex--;
                    }
                    
                }
                _positionTarget = waypointPositions[waypointIndex].transform.position;
            }
            //////////-----------------------------------------------------------------------------------------------------------
                        
            if (context._settings._followPathMode == SteeringSettings.FPM.Random){
                if (waypointDistance < 2f) {
                    waypointIndex = GetRandomWaypoint();
                }
                _positionTarget = waypointPositions[waypointIndex].transform.position;
            }

            waypointDistance = (waypointPositions[waypointIndex].transform.position - context._position).magnitude;
            
            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        } 
        // pakt een random Index van de hoeveelheid waypoints
        public int GetRandomWaypoint() {
            int randomIndex = Random.Range(0, waypointPositions.Length);
            return randomIndex;
        }
      

    }

}