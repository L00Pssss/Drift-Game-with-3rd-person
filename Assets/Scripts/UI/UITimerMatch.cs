using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimerMatch : MonoBehaviour
{
    [SerializeField] private RaceStateTracker m_raceStateTracker;
    [SerializeField] private TextMeshProUGUI m_textMeshProUGUI;

    private bool iStart = false;
    
    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;
    }
    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;
    }
    private void OnRaceStarted()
    {
        iStart = true;
    }
    
    private void Update()
    {
        if(iStart)
            m_textMeshProUGUI.text = m_raceStateTracker.RaceTimer.Value.ToString("F0");

        if (m_textMeshProUGUI.text == "0")
        {
            m_raceStateTracker.CompleteRace();
        }
    }
}
