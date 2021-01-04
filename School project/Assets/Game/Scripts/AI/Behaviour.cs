using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public abstract class Behaviour : IBehaviour {
        [Header("behaviour Runtime")]
        public Vector3 _positionTarget = Vector3.zero;
        public Vector3 _velocityDesired = Vector3.zero;

        public virtual void start( BehaviourContext context ) {
            _positionTarget = context._position;
        }

        public abstract Vector3 CalculateSteeringForce( float dt, BehaviourContext context );

        public virtual void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawRay(context._position, _velocityDesired, Color.red);
        }

    }
}
