using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SteeringExamples {

    using WPlist = List<UnityEngine.GameObject>;

    public class ExampleSettings : MonoBehaviour
    {

        public enum FPM {Forwards, Backwards, PingPong }

        [Header("Steering settings")]
        public float _mass = 70.0f;
        public float _maxDesiredVelocity = 2.0f;
        public float _maxSteeringForce = 2.0f;
        public float _maxSpeed = 5.0f/3.6f;

        [Header("Arrive")]
        public float _arriveDistance = 1.0f;

        [Header("Avoid Obstacle")]
        public float _avoidMaxForce = 5.0f;
        public float _avoidMaxDistance = 2.5f;
        public string _avoidLayer = "Obstacles";

        [Header("Hide")]
        public float _Hide_Offset = 1.0f;

        [Header("Pusuit and Evade")]

        public float _wanderCircleDistance = 5.0f;
        public float _wanderCircleRadius = 5.0f;
        public float _wanderNoiseAngle = 10.0f;

        [Header("Follow Path")]
        public FPM _followPathMode = FPM.Forwards;
        public bool _followPathLooping = true;
        public string _followPathTag = "";
        public float _followRadius = 2.5f;
        public WPlist _FollowPathWaypoints;

        public bool ArriveEnabled() { return _arriveDistance > 0.0f; }
        public bool AvoidObstacleEnabled() { return _avoidMaxForce > 0.0f; }

    }

}