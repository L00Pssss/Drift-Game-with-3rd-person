using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIResult : MonoBehaviour
{
    [SerializeField] private RaceStateTracker _raceStateTracker;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private UIScore _uiScore;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _textCash;
    [SerializeField] private TextMeshProUGUI _textPoint;

    [SerializeField] private float _bonus = 2;

    private PlayerReferences _playerReferences;

    public void Initialize(PlayerReferences playerReferences)
    {
        _playerReferences = playerReferences;
        _scoreManager = _playerReferences.ScoreManager ;
        _raceStateTracker = _playerReferences.RaceStateTracker;
        
        _playerReferences.Player.PlayerInitialized += PlayerOnPlayerInitialized;
    }

    private void PlayerOnPlayerInitialized()
    {
        InterstitialIron.Instance.GrandUpdate += InstanceOnGrandUpdate;
        _raceStateTracker.Completed += RaceStateTrackerOnCompleted;
    }

    private void OnDestroy()
    {
        InterstitialIron.Instance.GrandUpdate -= InstanceOnGrandUpdate;
        _raceStateTracker.Completed -= RaceStateTrackerOnCompleted;
        _playerReferences.Player.PlayerInitialized -= PlayerOnPlayerInitialized;

    }

    
    private void RaceStateTrackerOnCompleted()
    {
        ShowPanel();
    }
    private void InstanceOnGrandUpdate()
    {
        _textCash.text = "Cash For Drifting: " + _scoreManager.CalculateCash() * _bonus;
    }

    public void ShowPanel()
    {
        _panel.GameObject().SetActive(true);
        _textCash.text = "Cash For Drifting: " + _scoreManager.CalculateCash();
        _textPoint.text = "Points: " + _uiScore.Text.text;
        InterstitialIron.Instance.LoadRewarded();
    }

    public void RewardedAdClicked()
    {
        InterstitialIron.Instance.ShowRewarded();
    }
}
