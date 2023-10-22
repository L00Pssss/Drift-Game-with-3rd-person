using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private  ScoreManager _scoreManagerComponent;
    [SerializeField] private TextMeshProUGUI _text;

    
    
    private PlayerReferences _playerReferences;

    public void Initialize(PlayerReferences playerReferences)
    {
        _playerReferences = playerReferences;
        _scoreManagerComponent = _playerReferences.ScoreManager;
        
        _playerReferences.Player.PlayerInitialized += PlayerOnPlayerInitialized;
    }

    private void PlayerOnPlayerInitialized()
    {
        _scoreManagerComponent.UpdateScore += ScoreManagerComponentOnUpdateScore;
    }

    public TextMeshProUGUI Text => _text;
    

    private void OnDestroy()
    {
        _scoreManagerComponent.UpdateScore -= ScoreManagerComponentOnUpdateScore;
        
        _playerReferences.Player.PlayerInitialized -= PlayerOnPlayerInitialized;

    }

    private void ScoreManagerComponentOnUpdateScore(float score)
    {
        _text.text = score.ToString("F1");
    }
}
