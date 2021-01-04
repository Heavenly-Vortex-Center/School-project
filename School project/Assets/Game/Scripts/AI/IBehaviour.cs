using UnityEngine;

namespace Steering {

    public interface IBehaviour {
        /// <summary>
        /// Allow the behaviour to initialize
        /// </summary>
        /// <param name="context"></param>
        void start( BehaviourContext context );

        /// <summary>
        /// Calculate  the steering force contributed by this Behaviour.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        Vector3 CalculateSteeringForce( float dt, BehaviourContext context );

        /// <summary>
        /// Draw the gizmos for this behaviour
        /// </summary>
        /// <param name="context"></param>
        void OnDrawGizmos( BehaviourContext context );
    }
}
