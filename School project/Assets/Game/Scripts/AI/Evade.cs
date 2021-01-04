using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering {

    public class Evade : Behaviour
    {
        public override Vector3 CalculateSteeringForce( float dt, BehaviourContext context ) {

            var hitColliders = Physics.OverlapSphere(context._position, context._settings._AvoidRange, context._settings._SeekingObject);
            Support.GetClosestEnemy(context._position, hitColliders);

          
            _positionTarget = Support.GetClosestEnemy(context._position, hitColliders).gameObject.transform.position;

            _velocityDesired = -(_positionTarget - context._position).normalized * context._settings._maxDesiredVelocity;
            return _velocityDesired - context._velocity;
        }

        public override void OnDrawGizmos( BehaviourContext context ) {
            Support.DrawWireSphere(context._position, Color.red, context._settings._AvoidRange);
        }

    }
}



// maak het hier zo dat ze elkaar ontwijken en elkaar niet in de weg zitten met de physics overlap en de NPC ai Evade script