using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    [RequireComponent(typeof(Steering))]
    public class HunterBrain : MonoBehaviour
    {
        public enum HunterState { Idle, Approach, Pursue};

        [Header("Target to state")]
        public Vector3 _target;
        public HunterState _state;
        public float _pursueRadius = 7;
        public float _approachRadius = 10;

        [Header("Steering setting")]
        public SteeringSettings _idleSettings;
        public SteeringSettings _approachSettings;
        public SteeringSettings _pursueSettings;
        [Header("Private")]
        private Steering _steering;
        
        private void Start() {
            _steering = GetComponent<Steering>();


            ToIdle();
        }
        private void FixedUpdate() {
           
            float distanceToTarget = (_target - transform.position).magnitude;

            switch (_state) {
                case HunterState.Idle:
                    if (distanceToTarget < _approachRadius) {
                        ToApproach();
                    }
                    break;
                case HunterState.Approach:
                    if (distanceToTarget > _approachRadius) {
                        ToIdle();
                    }
                    break;
                case HunterState.Pursue:
                    if (distanceToTarget > _pursueRadius) {
                        ToPursue();
                    }
                    break;
            }
        }
        private void ToIdle() {
            _state = HunterState.Idle;
            _steering._settings = _idleSettings;

            List<IBehaviour> behaviours = new List<IBehaviour>();
            behaviours.Add(new Idle());
            _steering.SetBehaviours(behaviours, "Idle");

        }
        private void ToApproach() {
            _state = HunterState.Approach;
            _steering._settings = _approachSettings;

            List<IBehaviour> behaviours = new List<IBehaviour>();
            behaviours.Add(new Pursue());
            _steering.SetBehaviours(behaviours, "Approach");
        }
        private void ToPursue() {
            _state = HunterState.Pursue;
            _steering._settings = _pursueSettings;

            List<IBehaviour> behaviours = new List<IBehaviour>();
            behaviours.Add(new Pursue());
            _steering.SetBehaviours(behaviours, "pursue");
            

        }
        public Vector3 GetTarget(Vector3 target) {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _pursueRadius, _pursueSettings._SeekingObject);
            target  = Support.GetClosestEnemy(transform.position, hitColliders).transform.position;
            return target;
        }
        private void OnDrawGizmos() {
            Support.DrawWireSphere(transform.position, Color.cyan, _pursueRadius);
        }


    }
}
