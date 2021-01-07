using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 namespace Steering {

    public class Arrive : Behaviour
    {
        public override void start( BehaviourContext context ) {
            base.start(context);
        }
        
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

            _positionTarget = GameObject.Find("Priority Target").transform.position;
            _positionTarget.y = context._position.y;
           
            Vector3 stopVector = (context._position - _positionTarget).normalized * context._settings._arriveDistance;
            Vector3 stopPosition = _positionTarget + stopVector;

            Vector3 targetOffset = stopPosition - context._position;
            float distance = targetOffset.magnitude;

            float rampedSpeed = context._settings._maxDesiredVelocity * (distance / context._settings._slowingDistance);
            float clippedSpeed = Mathf.Min(rampedSpeed, context._settings._maxDesiredVelocity);

            if (distance > 0.001f)
                _velocityDesired = (clippedSpeed / distance) * targetOffset;
             else 
                _velocityDesired = Vector3.zero;
            
            return _velocityDesired - context._velocity;

           
        }
        public override void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawSolidSphere(_positionTarget, Color.black, 0.25f);
        }
    }
}
