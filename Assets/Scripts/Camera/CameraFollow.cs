using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Offset")] 
    [SerializeField] private float m_viewHight;
    [SerializeField] private float m_hight;
    [SerializeField] private float m_distance;

    [Header("Damping")]
    [SerializeField] private float m_rotationDamping;
    [SerializeField] private float m_heightDamping;
    [SerializeField] private float m_speedThreshold;

    [SerializeField] private Transform m_target;
    [SerializeField] private Rigidbody m_rigidbody;


    private void FixedUpdate()
    {
        Vector3 velocity = m_rigidbody.velocity;
        Vector3 targetRotation = m_target.eulerAngles;

        if (velocity.magnitude > m_speedThreshold)
        {
            targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
        }
        
        float currentAngel = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y,
            m_rotationDamping * Time.fixedDeltaTime);
        
        float currentHeight = Mathf.Lerp(transform.position.y, m_target.position.y + m_hight,
            m_heightDamping * Time.fixedDeltaTime);
        
        Vector3
            positionOffset =
                Quaternion.Euler(0, currentAngel, 0) * Vector3.forward *
                m_distance; 
        transform.position = m_target.position + positionOffset;
        
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // Rotation
        transform.LookAt(m_target.position + new Vector3(0, m_viewHight, 0));
    }
}
