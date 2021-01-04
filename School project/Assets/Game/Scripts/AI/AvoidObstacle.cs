using UnityEngine;
  

namespace Steering {

    public class AvoidObstacle : Behaviour {
        public LayerMask _obstacleLayer;
        public bool _doAvoidObject;
        Vector3 _hitPoint;

        public override void start( BehaviourContext context ) {
            base.start(context);
            _obstacleLayer = LayerMask.GetMask(context._settings._avoidLayer);

        }

        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            Ray ray = new Ray(context._position, context._velocity);
            _doAvoidObject = Physics.Raycast(ray, out RaycastHit hit, context._settings._avoidDistance, _obstacleLayer, QueryTriggerInteraction.Collide);

            if (!_doAvoidObject) {
                return Vector3.zero;
            }

            _hitPoint = hit.point;

            _velocityDesired = (_hitPoint - hit.collider.transform.position).normalized * context._settings._avoidMaxForce;

            float angle = Vector3.Angle(_velocityDesired, context._velocity);
            if (angle > 179) {
                _velocityDesired = Vector3.Cross(Vector3.up, context._velocity);

            }

            _positionTarget = context._position + _velocityDesired;
            return _velocityDesired - context._velocity;
        }
    }
}
