using System;
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    [SerializeField] private float _maxMotorTorque;
    [SerializeField] private float _maxSteerAngel;
    [SerializeField] private float _maxBrakeTorque;
 //   [SerializeField] private float _maxHandBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve m_engineTorqueCurve;
    
    [SerializeField] private float m_engineMaxTorque;

    [SerializeField] private float m_engineMinRpm;
    [SerializeField] private float m_engineMaxRpm;

    [SerializeField] private float m_maxSpeed;
 
    private float currentMotorTorque;

 
 
    private CarChassis _chassis;
    
    
    //debug
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;
 //   public float HandBrakeControl;


    public float LinearVelocity => _chassis.LinearVelocity;

    public float speed;
    private void Start()
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
            // Увеличивайте момент сцепления двигателя, когда газ нажат
            currentMotorTorque = Mathf.Lerp(currentMotorTorque, _maxMotorTorque, Time.deltaTime);
        }
        else
        {
            // Уменьшайте момент сцепления двигателя, когда газ не нажат
            currentMotorTorque = Mathf.Lerp(currentMotorTorque, 0, Time.deltaTime);
        }

        _chassis.m_EngineTorque = engineTorque  * ThrottleControl;
        _chassis.m_SteerAnngel = SteerControl * _maxSteerAngel;
        _chassis.m_BrakeTorque = BrakeControl *  _maxBrakeTorque;
      //  _chassis.m_HandBrakeControl = HandBrakeControl * _maxHandBrakeTorque;
        
    }
}
