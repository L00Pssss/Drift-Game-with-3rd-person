using System;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{


    [SerializeField] private float _maxMotorTorque;
    [SerializeField] private float _maxSteerAngel;
    [SerializeField] private float _maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve m_engineTorqueCurve;
    

    [SerializeField] private float m_maxSpeed;
 
    private float currentMotorTorque;



    private CarChassis _chassis;


    //debug
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;


    public float LinearVelocity => _chassis.LinearVelocity;

    public Rigidbody Rigidbody => _chassis.Rigidbody;

    public float speed;
    private void Awake()
    {
        _chassis = GetComponent<CarChassis>();
    }
    
    
    private void Update()
    {
        speed = LinearVelocity;

        var engineTorque = m_engineTorqueCurve.Evaluate(LinearVelocity / m_maxSpeed) * _maxMotorTorque;

        if (LinearVelocity >= m_maxSpeed)
        {
            engineTorque = 0;
        }
        
        if (ThrottleControl > 0)
        {
            currentMotorTorque = Mathf.Lerp(currentMotorTorque, _maxMotorTorque, Time.deltaTime);
        }
        else
        {
            currentMotorTorque = Mathf.Lerp(currentMotorTorque, 0, Time.deltaTime);
        }

        _chassis._EngineTorque = engineTorque  * ThrottleControl;
        _chassis._SteerAnngel = SteerControl * _maxSteerAngel;
        _chassis._BrakeTorque = BrakeControl *  _maxBrakeTorque;
    }
}
