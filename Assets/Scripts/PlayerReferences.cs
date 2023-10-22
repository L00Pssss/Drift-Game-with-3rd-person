using UnityEngine;


public class PlayerReferences : MonoBehaviour
{

    [SerializeField] private Player _player;
    [SerializeField] private RaceStateTracker _raceStateTracker;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private UIResult _uiResult;
    [SerializeField] private UITimerMatch _uiTimerMatch;
    [SerializeField] private UICountDownTimer _uiCountDownTimer;
    [SerializeField] private UIScore _uIScore;
    
    [SerializeField] private PointerClickHold _throttleButton;
    [SerializeField] private PointerClickHold _steerLeftButton;
    [SerializeField] private PointerClickHold _steerRightButton;
    [SerializeField] private PointerClickHold _backButton;
    [SerializeField] private PointerClickHold _startButton;



    [SerializeField] private Color _color;
    
    public Player Player { get; private set; }
    public RaceStateTracker RaceStateTracker { get; private set; }
    public ScoreManager ScoreManager { get; private set; }

    public UIResult UIResult => _uiResult;

    public UITimerMatch UITimerMatch => _uiTimerMatch;

    public UICountDownTimer UICountDownTimer => _uiCountDownTimer;

    public UIScore UIScore => _uIScore;
    
    public PointerClickHold ThrottleButton => _throttleButton;
    
    public PointerClickHold SteerLeftButton => _steerLeftButton;
    
    public PointerClickHold SteerRightButton => _steerRightButton;
    
    public PointerClickHold StartButton => _startButton;

    public PointerClickHold BackButton => _backButton;



    public void Initialize(Color color)
    {
        _color = color;
    }

    public Color Color()
    {
        return _color;
    }
    

    public void Initialize(Player player)
    {
        Player = player;

        _player = player;  //debug
    }
    
    public void Initialize(RaceStateTracker raceStateTracker)
    {
        RaceStateTracker = raceStateTracker;

        _raceStateTracker = raceStateTracker;  //debug
    }
    
    public void Initialize(ScoreManager scoreManager)
    {
        ScoreManager = scoreManager;


        _scoreManager = scoreManager;  //debug
    }
    
    
}
