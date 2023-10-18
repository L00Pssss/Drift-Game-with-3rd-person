using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private  ScoreManager _scoreManagerComponent;
    [SerializeField] private TextMeshProUGUI _text;

    public TextMeshProUGUI Text => _text;

    private void Start()
    {
        _scoreManagerComponent.UpdateScore += ScoreManagerComponentOnUpdateScore;
    }

    private void OnDestroy()
    {
        _scoreManagerComponent.UpdateScore -= ScoreManagerComponentOnUpdateScore;

    }

    private void ScoreManagerComponentOnUpdateScore(float score)
    {
        _text.text = score.ToString("F1");
    }
}
