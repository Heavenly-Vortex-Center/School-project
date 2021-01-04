using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class Idle : Behaviour {
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            _positionTarget = context._position + dt * context._velocity;
            _velocityDesired = Vector3.zero;
            return _velocityDesired - context._velocity;
        }
    }

}