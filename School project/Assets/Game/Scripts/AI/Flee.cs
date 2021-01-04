using UnityEngine;
namespace Steering {

    public class Flee : Behaviour
    {
        public GameObject _CurrentTarget;

        public Flee(GameObject target) {
            _CurrentTarget = target;
        }

        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            
            _positionTarget = _CurrentTarget.transform.position;
            
            _velocityDesired = -(_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        }
    }
}
