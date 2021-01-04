using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {
    
    public class BehaviourContext
    {
        public Vector3 _position;
        public Vector3 _velocity;
        public SteeringSettings _settings;

        public BehaviourContext(Vector3 position, Vector3 velocity, SteeringSettings settings ) {
           // _position = position;
            //_velocity = velocity;
            //_settings = settings; 
        }     
        
    }
}
