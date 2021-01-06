using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    [RequireComponent(typeof(Steering))]
    public class HunterBrain : MonoBehaviour
    {
        public enum HunterState { Idle, Approach, Pursue};

        [Header("Target to state")]
        public GameObject _target;
        public HunterState _state;
        public float _pursueRadius = 7;
        public float _approachRadius = 10;
        public float _avoidRadius = 2;

        [Header("Steering setting")]
        public SteeringSettings _idleSettings;
        public SteeringSettings _approachSettings;
        public SteeringSettings _pursueSettings;
        public SteeringSettings _avoidSettings;
        [Header("Private")]
        private Steering _steering;
        
        private void Start() {
            _steering = GetComponent<Steering>();
            _target = GameObject.Find("Target");

            ToIdle();
        }
        private void FixedUpdate() {
           
            float distanceToTarget = (_target.transform.position - transform.position).magnitude;
           // Debug.Log(distanceToTarget);

            switch (_state) {
                case HunterState.Idle:
                    if (distanceToTarget < _approachRadius) {
                        Debug.Log("approach");
                        ToApproach();
                    }
                    break;
                case HunterState.Approach:
                    if (distanceToTarget > _approachRadius) {
                        Debug.Log("idle");
                        ToIdle();
                    } else if (distanceToTarget < _pursueRadius) {
                        ToPursue();
                    }
                    break;
                case HunterState.Pursue:
                    if (distanceToTarget > _pursueRadius) {
                        Debug.Log("pursue");
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
            Support.DrawWireSphere(transform.position, Color.red, _approachRadius);
            Support.DrawWireSphere(transform.position, Color.cyan, _pursueRadius);              
            Support.DrawWireSphere(transform.position, Color.black, _avoidRadius);
        }


    }
}
