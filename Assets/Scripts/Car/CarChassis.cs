using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxle[] m_wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centerOfMass;

    [Header("Down Force")]
    [SerializeField] private float m_downForceMin;
    [SerializeField] private float m_downForceMax;
    [SerializeField] private float m_downForceFactor;
    
    [Header("AngularDrag")]
    [SerializeField] private float m_angularDragMin;
    [SerializeField] private float m_angularDragMax;
    [SerializeField] private float m_angularDragFactor;
     

    //debug
    public float m_EngineTorque;
    public float m_BrakeTorque;

    public float m_SteerAnngel;
    //   public float m_HandBrakeControl;


    public float LinearVelocity => _rigidbody.velocity.magnitude * 3.6f;


    private new Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
            _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();
        
        UpdateWheelAxles();

        UpdateDownForce();
    }

    private void UpdateAngularDrag()
    {
        _rigidbody.angularDrag = Mathf.Clamp(m_angularDragFactor * LinearVelocity, m_angularDragMin, m_angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(m_downForceFactor * LinearVelocity, m_downForceMin, m_downForceMax);
        _rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        
        int amountMotorWheel = 0;

        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            if (m_wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }
        
        for (int i = 0; i < m_wheelAxles.Length; i++)
        {
            m_wheelAxles[i].Update();

            m_wheelAxles[i].ApplyMotorTorque(m_EngineTorque / amountMotorWheel);
            m_wheelAxles[i].ApplySteerAngel(m_SteerAnngel, wheelBaseLength);
            m_wheelAxles[i].ApplyBreakTorque(m_BrakeTorque);
            //   m_wheelAxles[i].ApplyBreakTorque(m_HandBrakeControl); // if need. 
        }
    }
}
