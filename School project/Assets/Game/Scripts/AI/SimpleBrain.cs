using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Steering {
    [RequireComponent(typeof(Steering))]
    public class SimpleBrain : MonoBehaviour {
        public enum BehaviourEnum { Wander, Seek , NotSet };

        [Header("Manual")]
        public BehaviourEnum _behaviour;

        [HideInInspector] public GameObject _target;
        [HideInInspector] public GameObject _player;

        [Header("Private")]
        private Steering _steering;


        public SimpleBrain() {
            _behaviour = BehaviourEnum.NotSet;
            _target = null;
        }

        private void Start() {
            _steering = GetComponent<Steering>();
            List<IBehaviour> behaviours = new List<IBehaviour>();
            switch (_behaviour) {
                case BehaviourEnum.Wander:
                    behaviours.Add(new Wander());
                    behaviours.Add(new Evade());
                    _steering.SetBehaviours(behaviours, "Wander");
                    break;  
                case BehaviourEnum.Seek:
                    behaviours.Add(new Pursue());
                    _steering.SetBehaviours(behaviours, "Persue");

                  
                    break;
                default:
                    Debug.LogError($"Behaviour of type {_behaviour} not implemented yet");
                    break;
            }

        }


    }
}
