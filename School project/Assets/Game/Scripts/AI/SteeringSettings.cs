using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SteeringSettings : ScriptableObject {
    public enum FPM { Forwards, Backwards, PingPong, Random };

    [Header("Steering settings")]
    public float _mass = 70.0f;               //mass in kg
    public float _maxDesiredVelocity = 2.0f;  //max desired velocity in m/s
    public float _maxSteeringForce = 2.0f;    //max steering 'force' in m/s
    public float _maxSpeed = 5.0f;            //max vehicle speed in m/s

    [Header("Seek Settings")]
    public float _lookRange = 16.0f;
    public LayerMask _SeekingObject;

    [Header("Arrive")]
    public float _arriveDistance = 1.0f;
    public float _slowingDistance = 2.0f;

    [Header("Follow Path")]
    public FPM _followPathMode;

    [Header("Avoid obstacle")]

    public float _avoidMaxForce = 10f;
    public float _avoidDistance = 15f;
    public string _avoidLayer = "Obstacle";

    [Header("Evade and persue")]
    public float _lookAheadTime = 5f;
    public float _AvoidRange = 5f;

    [Header("Wander")]
    public float _wanderCirlceDistance = 9f;
    public float _wanderCircleRadius = 20f;
    public float _wanderNoiseAngle = 18.5f;

    //[Header("Hide")]
    public float _hideOffset = 1f;
    public string _hideLayer = "Target";     
}