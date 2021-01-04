using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class Keyboard : Behaviour {
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

            Vector3 requested_Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            if (requested_Direction != Vector3.zero)
                _positionTarget = context._position + requested_Direction.normalized * context._settings._maxDesiredVelocity;
            else
                _positionTarget = context._position;

            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        }

    }
}
