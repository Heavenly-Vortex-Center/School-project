using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class SeekClosestObject : Behaviour
    {
        private Vector3  CurrentPos;
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            CurrentPos = context._position;
            var hitColliders = Physics.OverlapSphere(context._position, context._settings._lookRange, context._settings._SeekingObject);
            var _closestTarget = GetClosestEnemy(hitColliders);
            if (hitColliders != null && _closestTarget != null) {
                _positionTarget = _closestTarget.transform.position;
            } else {
                _positionTarget = context._position;
            }

            _positionTarget.y = context._position.y;

            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;  

        }
        Collider GetClosestEnemy( Collider[] enemies ) {
            Collider tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = CurrentPos;
            foreach (Collider t in enemies) {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist) {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
        public override void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawCurrentDestiny(_positionTarget, Color.blue);
            Support.DrawCircle(context._position, Color.white, context._settings._lookRange);
        }
    }
}
