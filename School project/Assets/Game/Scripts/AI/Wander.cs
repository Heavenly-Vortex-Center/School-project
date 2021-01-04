using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class Wander : Behaviour {
        float _wanderAngle = 0;

        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

            _wanderAngle += Random.Range(-0.5f * context._settings._wanderNoiseAngle * Mathf.Deg2Rad, 0.5f * context._settings._wanderNoiseAngle * Mathf.Deg2Rad);

            Vector3 centerOfCircle = context._position + context._velocity.normalized * context._settings._wanderNoiseAngle;

            Vector3 offset = new Vector3(context._settings._wanderCircleRadius * Mathf.Cos(_wanderAngle), 0.0f, context._settings._wanderCircleRadius * Mathf.Sin(_wanderAngle));

            _positionTarget = centerOfCircle + offset;

            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        }
    }
}

