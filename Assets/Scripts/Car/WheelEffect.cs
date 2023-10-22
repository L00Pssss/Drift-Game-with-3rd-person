using System;
using UnityEngine;
using UnityEngine.Events;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] m_wheels;
    [SerializeField] private ParticleSystem[] m_wheelsSmoke;
    [SerializeField] private int m_particleSystemEmit;

    [SerializeField] private float m_forwardSlipLimit;
    [SerializeField] private float m_sidewaySlipLimit;

    [SerializeField] private AudioSource m_audio;

    [SerializeField] private GameObject m_skidPrefab;
    
    [SerializeField] private float m_minSidewaySlipLimit;

    private WheelHit wheelHit;
    private Transform[] skidTrail;
    
    private float startTime;
    private float endTime;

    public event UnityAction<float> OnStartSliding;
    public event UnityAction<float> OnEndSliding;
    private void Start()
    {
        skidTrail = new Transform[m_wheels.Length];
    }
    
    
    
    private void Update()
    {
        DetectAndHandleSliding();
    }


    private void DetectAndHandleSliding()
    {
        bool isSlip = false;

        for (int i = 0; i < m_wheels.Length; i++)
        {
            m_wheels[i].GetGroundHit(out wheelHit);

            if (m_wheels[i].isGrounded == true)
            {
                if (wheelHit.forwardSlip > m_forwardSlipLimit || Mathf.Abs(wheelHit.sidewaysSlip) > m_sidewaySlipLimit)
                {
                    if (Mathf.Abs(wheelHit.sidewaysSlip) < m_minSidewaySlipLimit)
                    {
                        // Игнорируем незначительное скольжение
                        continue;
                    }

                    if (skidTrail[i] == null)
                    {
                        skidTrail[i] = Instantiate(m_skidPrefab).transform;
                        startTime = Time.time;
                        OnStartSliding?.Invoke(startTime);
                    }

                    if (m_audio.isPlaying == false)
                    {
                        m_audio.Play();
                    }

                    if (skidTrail[i] != null)
                    {
                        skidTrail[i].position = m_wheels[i].transform.position - wheelHit.normal * m_wheels[i].radius;
                        skidTrail[i].forward = -wheelHit.normal;

                        m_wheelsSmoke[i].transform.position = skidTrail[i].position;
                        m_wheelsSmoke[i].Emit(m_particleSystemEmit);
                        
                        endTime = Time.time;
                        OnEndSliding?.Invoke(endTime);
                    }

                    isSlip = true;

                    continue;
                }
            }

            skidTrail[i] = null;
            m_wheelsSmoke[i].Stop();
        }

        if(isSlip == false)
            m_audio.Stop();
    }
}
