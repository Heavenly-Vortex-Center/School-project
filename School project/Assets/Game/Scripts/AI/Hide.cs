using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {
    using ColliderList = List<Collider>;
    using HideList = List<Vector3>;

    public class Hide : Behaviour {
        readonly private GameObject _target;

        private ColliderList _colliders;
        private HideList _hidingPlaces;
        private Vector3 _hidingPlace;

        public Hide( GameObject target ) {

            _target = target;
        }

        public override void start( BehaviourContext context ) {
            base.start(context);
            Debug.Log(FindColliderWithLayer(context._settings._hideLayer));

            _colliders = FindColliderWithLayer(context._settings._hideLayer);
        }

        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {
            _positionTarget = CalculateHidingPlace(context, _target.transform.position);
            _velocityDesired = (_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;

        }

        static public List<Collider> FindColliderWithLayer( string layerName ) {
            int colliderLayer = LayerMask.NameToLayer(layerName);


            Collider[] allColliders = GameObject.FindObjectsOfType(typeof(Collider)) as Collider[];
            List<Collider> colliders = new List<Collider>();
            foreach (Collider gameObject in allColliders) {
                if (gameObject.gameObject.layer == colliderLayer) {
                    colliders.Add(gameObject);
                }
            }
            return colliders;
        }
        public Vector3 CalculateHidingPlace( BehaviourContext context, Vector3 enemy_Position ) {

            float closestDistanceSqr = float.MaxValue;
            _hidingPlace = context._position;
            _hidingPlaces = new HideList();

            for (int i = 0; i < _colliders.Count; i++) {
                Vector3 hidingPlace = CalculateHidingPlace(context, _colliders[i], enemy_Position);
                _hidingPlaces.Add(hidingPlace);

                float distanceToHidingPlace = (context._position - hidingPlace).sqrMagnitude;
                if (distanceToHidingPlace < closestDistanceSqr) {
                    closestDistanceSqr = distanceToHidingPlace;
                    _hidingPlace = hidingPlace;
                }
            }
            return _hidingPlace;



        }
        public Vector3 CalculateHidingPlace( BehaviourContext context, Collider collider, Vector3 enemy_Position ) {
            Vector3 obstacleDirection = (collider.transform.position - enemy_Position).normalized;
            Vector3 pointOtherSide = collider.transform.position + obstacleDirection;
            Vector3 hidingPlace = collider.ClosestPoint(pointOtherSide) + (obstacleDirection * context._settings._hideOffset);

            return hidingPlace;
        }
        public override void OnDrawGizmos( BehaviourContext context ) {
            base.OnDrawGizmos(context);

            foreach (Vector3 hidingPlace in _hidingPlaces) {
                Support.DrawSolidSphere(hidingPlace, Color.blue, 0.25f);
            }
            Support.DrawWireSphere(_hidingPlace, Color.blue, 0.35f);
        }

    }
}
