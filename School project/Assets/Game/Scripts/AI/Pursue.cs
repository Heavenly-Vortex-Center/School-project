using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class Pursue : Behaviour {

        private Vector3 _previousTargetPosition;
        private Vector3 _currentTargetPosition;


        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            
            var hitColliders = Physics.OverlapSphere(context._position, context._settings._lookRange, context._settings._SeekingObject);
            var _closestTarget = Support.GetClosestEnemy(context._position  ,hitColliders);
            if (hitColliders != null && _closestTarget != null) {
                _positionTarget = _closestTarget.transform.position;
            } else {
                _positionTarget = context._position;
            }

            _positionTarget = GetFuturePosition(dt, context);
            _positionTarget.y = context._position.y;

            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;

        }
        public Vector3 GetFuturePosition( float dt, BehaviourContext context ) {
            // hier calculate je steeds de positions met een waarde van 1 of meer ertussen als ik het goed heb
            _previousTargetPosition = _currentTargetPosition;
            _currentTargetPosition = _positionTarget;

            // hier reken je de directie van de Target uit
            Vector3 targetVelocity = (_currentTargetPosition - _previousTargetPosition) / dt;

            // hier bereken en geef je de waarde terug van waar hij heen moet
            return _currentTargetPosition + targetVelocity * context._settings._lookAheadTime;
        }

        public override void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawCurrentDestiny(_positionTarget, Color.blue);
            Support.DrawCircle(context._position, Color.white, context._settings._lookRange);
        }
    }
}



        //public Pursue( GameObject target ) {
        //    _target = target;
        //    _previousTargetPosition = target.transform.position;
        //    _currentTargetPosition = target.transform.position;

        //}
        //public override void start( BehaviourContext context ) {
        //    base.start(context);
        //    _previousTargetPosition = _target;
        //    _currentTargetPosition = _target;

        //}

        //public Vector3 GetFuturePosition( float dt, BehaviourContext context ) {
        //    var hitColliders = Physics.OverlapSphere(context._position, context._settings._lookRange, context._settings._SeekingObject);
        //    var _closestTarget = GetClosestEnemy(context._position ,hitColliders);
        //    if (hitColliders != null && _closestTarget != null) {
        //        _target = _closestTarget.transform.position;
        //    } else {
        //        _target = context._position;
        //    }

        //    _previousTargetPosition = _currentTargetPosition;
        //    _currentTargetPosition = _target;

        //    Vector3 targetVelocity = (_currentTargetPosition - _previousTargetPosition) / dt;

        //    return _currentTargetPosition + targetVelocity * context._settings._lookAheadTime;
        //}

        //public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

        //    _positionTarget = GetFuturePosition(dt, context);

        //    _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
        //    return _velocityDesired - context._velocity;
        //}

        //public static Collider GetClosestEnemy( Vector3 CurrentPosition, Collider[] enemies ) {
        //    Collider tMin = null;
        //    float minDist = Mathf.Infinity;
        //    Vector3 currentPos = CurrentPosition;
        //    foreach (Collider t in enemies) {
        //        float dist = Vector3.Distance(t.transform.position, currentPos);
        //        if (dist < minDist) {
        //            tMin = t;
        //            minDist = dist;
        //        }
        //    }
        //    return tMin;