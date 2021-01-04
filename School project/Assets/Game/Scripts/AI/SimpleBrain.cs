using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Steering {
    [RequireComponent(typeof(Steering))]
    public class SimpleBrain : MonoBehaviour {
        public enum BehaviourEnum { Keyboard, SeekClickPoint, Seek, Flee, Pursue, Evade, Wander, FollowPath, Hide, Arrive, avoidObject, NotSet };

        [Header("Manual")]
        public BehaviourEnum _behaviour;
        public GameObject _target;
        public GameObject _player;
        [Header("Private")]
        private Steering _steering;


        public SimpleBrain() {
            _behaviour = BehaviourEnum.NotSet;
            _target = null;
        }

        private void Start() {
            if (_behaviour == BehaviourEnum.Keyboard || _behaviour == BehaviourEnum.SeekClickPoint) {
                _target = null;
            } else {
                if (_player == null) {
                    _player = GameObject.Find("Player");
                }
                if (_target == null) {
                    _target = GameObject.Find("Priority Target");
                }

            }
            _steering = GetComponent<Steering>();
            List<IBehaviour> behaviours = new List<IBehaviour>();
            switch (_behaviour) {
                case BehaviourEnum.Keyboard:
                    behaviours.Add(new Keyboard());
                    _steering.SetBehaviours(behaviours, "Keyboard");
                    break;
                case BehaviourEnum.SeekClickPoint:
                    behaviours.Add(new SeekPointClick());
                    _steering.SetBehaviours(behaviours, "Point Click");
                    break;
                case BehaviourEnum.Seek:
                    behaviours.Add(new SeekClosestObject());
                    _steering.SetBehaviours(behaviours, "Seek");
                    break;
                case BehaviourEnum.FollowPath:
                    behaviours.Add(new FollowPath());
                    _steering.SetBehaviours(behaviours, "Follow path");
                    break;
                case BehaviourEnum.Arrive:
                    behaviours.Add(new Arrive());
                    _steering.SetBehaviours(behaviours, "Arrive");
                    break;
                case BehaviourEnum.Flee:
                    behaviours.Add(new Flee(_target));
                    _steering.SetBehaviours(behaviours, "Flee");
                    break;
                case BehaviourEnum.Wander:
                    behaviours.Add(new Wander());
                    _steering.SetBehaviours(behaviours, "Wander");
                    break;
                case BehaviourEnum.avoidObject:
                    behaviours.Add(new SeekObject(_target));
                    behaviours.Add(new AvoidObstacle());
                    behaviours.Add(new Idle());
                    _steering.SetBehaviours(behaviours, "Avoid");
                    break;
                case BehaviourEnum.Hide:
                    behaviours.Add(new Hide(_player));
                    _steering.SetBehaviours(behaviours, "Hide");
                    break;
                case BehaviourEnum.Evade:
                    behaviours.Add(new Evade());
                    _steering.SetBehaviours(behaviours, "Evade");
                    break;
                case BehaviourEnum.Pursue:
                    behaviours.Add(new Pursue());
                    _steering.SetBehaviours(behaviours, "Pursue");
                    break;
                default:
                    Debug.LogError($"Behaviour of type {_behaviour} not implemented yet");
                    break;
            }

        }


    }
}
