using TMPro;
using UnityEngine;

public class UICountDownTimer : MonoBehaviour
{
    [SerializeField] private RaceStateTracker m_raceStateTracker;
    [SerializeField] private TextMeshProUGUI m_textMeshProUGUI;
    private void Start()
    {
        m_raceStateTracker.Started += OnRaceStarted;
        m_raceStateTracker.PeparationStarted += OnPeparationStarted;
        m_textMeshProUGUI.enabled = false;
    }

    private void OnDestroy()
    {
        m_raceStateTracker.Started -= OnRaceStarted;
        m_raceStateTracker.PeparationStarted -= OnPeparationStarted;
    }

    private void OnPeparationStarted()
    {
        m_textMeshProUGUI.enabled = true;
        enabled = true;
    }

    private void OnRaceStarted()
    {
        m_textMeshProUGUI.enabled = false;
        enabled = false;
    }


    private void Update()
    {
        m_textMeshProUGUI.text = m_raceStateTracker.CountDownTimer.Value.ToString("F0"); // ��� �� �� ���� ���� ����� �������. 

        if (m_textMeshProUGUI.text == "0")
            m_textMeshProUGUI.text = "GO!";
    }


}