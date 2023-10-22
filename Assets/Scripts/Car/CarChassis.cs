using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [FormerlySerializedAs("m_wheelAxles")] [SerializeField] private WheelAxle[] _wheelAxles;
    [FormerlySerializedAs("wheelBaseLength")] [SerializeField] private float _wheelBaseLength;

    [FormerlySerializedAs("centerOfMass")] [SerializeField] private Transform _centerOfMass;

    [FormerlySerializedAs("m_downForceMin")]
    [Header("Down Force")]
    [SerializeField] private float _downForceMin;
    [FormerlySerializedAs("m_downForceMax")] [SerializeField] private float _downForceMax;
    [FormerlySerializedAs("m_downForceFactor")] [SerializeField] private float _downForceFactor;
    
    [FormerlySerializedAs("m_angularDragMin")]
    [Header("AngularDrag")]
    [SerializeField] private float _angularDragMin;
    [FormerlySerializedAs("m_angularDragMax")] [SerializeField] private float _angularDragMax;
    [FormerlySerializedAs("m_angularDragFactor")] [SerializeField] private float _angularDragFactor;
     

    //debug
    [FormerlySerializedAs("m_EngineTorque")] public float _EngineTorque;
    [FormerlySerializedAs("m_BrakeTorque")] public float _BrakeTorque;
    [FormerlySerializedAs("m_SteerAnngel")] public float _SteerAnngel;



    public float LinearVelocity => _rigidbody.velocity.magnitude * 3.6f;
    
    public Rigidbody Rigidbody => _rigidbody;

    [SerializeField] private new Rigidbody _rigidbody;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (_centerOfMass != null)
            _rigidbody.centerOfMass = _centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();
        
        UpdateWheelAxles();

        UpdateDownForce();
    }

    private void UpdateAngularDrag()
    {
        _rigidbody.angularDrag = Mathf.Clamp(_angularDragFactor * LinearVelocity, _angularDragMin, _angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(_downForceFactor * LinearVelocity, _downForceMin, _downForceMax);
        _rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        
        int amountMotorWheel = 0;

        for (int i = 0; i < _wheelAxles.Length; i++)
        {
            if (_wheelAxles[i].IsMotor == true)
                amountMotorWheel += 2;
        }
        
        for (int i = 0; i < _wheelAxles.Length; i++)
        {
            _wheelAxles[i].Update();

            _wheelAxles[i].ApplyMotorTorque(_EngineTorque / amountMotorWheel);
            _wheelAxles[i].ApplySteerAngel(_SteerAnngel, _wheelBaseLength);
            _wheelAxles[i].ApplyBreakTorque(_BrakeTorque);
        }
    }
}
