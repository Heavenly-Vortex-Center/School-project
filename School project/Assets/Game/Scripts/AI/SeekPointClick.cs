using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class SeekPointClick : Behaviour {

        //----------------------------------------------------------------------------------------------------------------------//
        //de point seek doet het niet echt goed omdat hij in een fixedupdate zit
        //----------------------------------------------------------------------------------------------------------------------//
        public override void start( BehaviourContext context ) {
            base.start(context);
        }
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            float distance = (_positionTarget - context._position).magnitude;
            Debug.Log(distance);
            if (Input.GetMouseButtonDown(0)) {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit)) {
                    _positionTarget = hit.point;
                    _positionTarget.y = context._position.y;
                }
            } 
            if (distance < context._settings._arriveDistance) {
                _positionTarget = Vector3.zero;
                Debug.Log("has arrived");
            }
            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        }
        public override void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawRay(context._position, context._velocity, Color.red);
            Support.DrawRay(context._position, _velocityDesired, Color.blue);
            UnityEditor.Handles.color = Color.black;
            UnityEditor.Handles.DrawSolidDisc(_positionTarget, Vector3.up, 0.25f);
        }



    }
}
