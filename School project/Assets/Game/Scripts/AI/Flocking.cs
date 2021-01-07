using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {


    class FlockAlignment {
        private float _sqrRadius;
        private int _neighborCount = 0;
        public Vector3 _total = Vector3.zero;

        public FlockAlignment(float radius ) {
            _sqrRadius = radius * radius;
        }

        public void AddVelocity(float sqrDistance, Vector3 neighborVelocity ) {
            if (sqrDistance <= _sqrRadius) {
                _total += neighborVelocity;
                _neighborCount++;
            }
        }

        public Vector3 DesiredVelocity() {
            if (_neighborCount > 0)
                return (_total / (float)_neighborCount).normalized;
            else
                return Vector3.zero;
        }

    }

    class FlockCohesion {
        private float _sqrRadius;
        private int _neighborCount = 0;
        public Vector3 _total = Vector3.zero;

        public FlockCohesion( float radius ) {
            _sqrRadius = radius * radius;
        }

        public void AddPosition( float sqrDistance, Vector3 neighborPosition ) {
            if (sqrDistance <= _sqrRadius) {
                _total += neighborPosition;
                _neighborCount++;
            }
        }

        public Vector3 DesiredVelocity(Vector3 position) {
            if (_neighborCount > 0) {
                Vector3 average = _total / (float)_neighborCount;
                return (average - position).normalized;
            } else
                return Vector3.zero;
        }
    }

    class FlockSeperation {

        private float _sqrRadius;
        private int _neighborCount = 0;
        public Vector3 _total = Vector3.zero;

        public FlockSeperation( float radius ) {
            _sqrRadius = radius * radius;
        }

        public void AddDirection( float sqrDistance, Vector3 neighborDirection ) {
            if (sqrDistance <= _sqrRadius) {
                _total += neighborDirection.normalized;
                _neighborCount++;
            }
        }
        public Vector3 DesiredVelocity() {
            if (_neighborCount > 0)
                return -(_total / (float)_neighborCount).normalized;
            else
                return Vector3.zero; 
            
        }

    }


    public class Flocking : Behaviour {

        private readonly Collider _myCollider;

        private int _flockLayer;
        private float _largestRadius;

        private Flocking(Collider myCollider ) {
            _myCollider = myCollider;
        }

        public override void start( BehaviourContext context ) {

            base.start(context);

            _flockLayer = LayerMask.GetMask(context._settings._flocklayer);
            if (context._settings._flockAlignmentWeight > 0.0f)
                _largestRadius = Mathf.Max(_largestRadius, context._settings._flockAlignmentRadius);
            if (context._settings._flockAlignmentWeight > 0.0f)
                _largestRadius = Mathf.Max(_largestRadius, context._settings._flockCohesionRadius);
            if (context._settings._flockAlignmentWeight > 0.0f)
                _largestRadius = Mathf.Max(_largestRadius, context._settings._flockSeparationRadius);

        }

        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

            _velocityDesired = CalculateDesiredVelocity(context);
            _positionTarget = context._position + _velocityDesired * dt;
            return _velocityDesired - context._velocity;
        }
        public Vector3 CalculateDesiredVelocity (BehaviourContext context ) {
            Collider[] neighbors = Physics.OverlapSphere(context._position, _largestRadius, _flockLayer, QueryTriggerInteraction.Collide);
            if (neighbors.Length == 0)
                return Vector3.zero;

            FlockAlignment alignment = new FlockAlignment(context._settings._flockAlignmentRadius);
            FlockCohesion cohesion = new FlockCohesion(context._settings._flockCohesionRadius);
            FlockSeperation seperation = new FlockSeperation(context._settings._flockSeparationRadius);

            foreach (Collider neighbor in neighbors) {

                if (neighbor == _myCollider)
                    continue;

                Steering neighborSteering = neighbor.gameObject.GetComponent<Steering>();
                if (neighborSteering == null) {
                    Debug.LogError($"ERROR: Flock behaviour found neighbor in layer {context._settings._flocklayer} without steering script!");
                    continue;
                }

                Vector3 neighborDirection = neighborSteering._position - context._position;
                float sqrDistance = neighborDirection.sqrMagnitude;

                if (Vector3.Angle(_myCollider.transform.forward, neighborDirection) > context._settings._flockVisibilityAngle)
                    continue;

                alignment.AddVelocity(sqrDistance, neighborSteering._velocity);
                cohesion.AddPosition(sqrDistance, neighborSteering._position);
                seperation.AddDirection(sqrDistance, neighborDirection);
            }

            Vector3 desiredVelocity =   alignment.DesiredVelocity() * context._settings._flockAlignmentWeight + 
                                        cohesion.DesiredVelocity(context._position) * context._settings._flockCohesionWeight + 
                                        seperation.DesiredVelocity() * context._settings._flockSeparationWeight;

            return desiredVelocity.normalized * context._settings._maxDesiredVelocity;

        }

        //public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

        //}

    }
     
}

