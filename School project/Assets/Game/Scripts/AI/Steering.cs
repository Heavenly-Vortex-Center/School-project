using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    using BehaviourList = List<IBehaviour>;

    public class Steering : MonoBehaviour {
        [Header("Steering Settings")]
        public string _label;
        public SteeringSettings _settings; // the steering settings for all behaviours

        [Header("Steering Runtime")]
        public Vector3 _position = Vector3.zero;
        public Vector3 _velocity = Vector3.zero;
        public Vector3 _steering = Vector3.zero;
        public BehaviourList _behaviours = new BehaviourList();


        private void Awake() {
            _position = transform.position;
        }

        private void Update() {
            _steering = Vector3.zero;
            foreach (IBehaviour behaviour in _behaviours) {
                _steering += behaviour.CalculateSteeringForce(Time.deltaTime, new BehaviourContext(_position, _velocity, _settings));
            }
        }

        private void FixedUpdate() {
            // calculate steering force

            _steering.y = 0.0f;

            // clamp steering force to maximum steering force and apply mass
            _steering = Vector3.ClampMagnitude(_steering, _settings._maxSteeringForce);
            _steering /= _settings._mass;

            //update velocity with steering force, and update position
            _velocity = Vector3.ClampMagnitude(_velocity + _steering, _settings._maxSpeed);
            _position += _velocity * Time.fixedDeltaTime;

            // update object with new position
            transform.position = _position;
            transform.LookAt(_position + Time.fixedDeltaTime * _velocity);
        }
        private void OnDrawGizmos() {
            Support.DrawRay(transform.position, _velocity, Color.red);
            Support.DrawLabel(transform.position, _label, Color.white);

            foreach (IBehaviour behaviour in _behaviours) {
                behaviour.OnDrawGizmos(new BehaviourContext(_position, _velocity, _settings));
            }
        }
        public void SetBehaviours( BehaviourList behaviours, string label = "" ) {
            _label = label;
            _behaviours = behaviours;

            foreach (IBehaviour behaviour in _behaviours) {
                behaviour.start(new BehaviourContext(_position, _velocity, _settings));
            }
        }

    }
}
