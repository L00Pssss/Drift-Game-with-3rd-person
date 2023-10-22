using Photon.Pun;
using UnityEngine;
public class CarInputControl : MonoBehaviour
{
    private Player _player;

    private PhotonView _photonView;

    public PlayerReferences _playerReferences;

    [SerializeField] private PointerClickHold throttleButton;
    [SerializeField] private PointerClickHold steerLeftButton;
    [SerializeField] private PointerClickHold steerRightButton;
    [SerializeField] private PointerClickHold startButton;
    [SerializeField] private PointerClickHold throttleBackButton;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        _playerReferences = _player.PlayerReferences;

        throttleButton = _player.PlayerReferences.ThrottleButton;
        steerLeftButton = _player.PlayerReferences.SteerLeftButton;
        steerRightButton = _player.PlayerReferences.SteerRightButton;
        startButton = _player.PlayerReferences.StartButton;
        throttleBackButton = _player.PlayerReferences.BackButton;

    }


    private void Update()
    {
        if (_photonView.IsMine)
        {
            UpdateControlKeyboard();
        }
    }

    private bool IsStart = true;

    private void UpdateControlKeyboard()
    {
        if (_player.ActiveVehicle == null) return;

        if (_player._RaceStateTracker.State == RaceState.Race)
        {
            if (throttleButton.IsHold && throttleBackButton.IsHold)
            {
                _player.ActiveVehicle.ThrottleControl = 0;
            }
            else if (throttleButton.IsHold)
            {
                _player.ActiveVehicle.ThrottleControl = 1;
            }
            else if (throttleBackButton.IsHold)
            {
                _player.ActiveVehicle.ThrottleControl = -1;
            }


            _player.ActiveVehicle.SteerControl = steerLeftButton.IsHold ? -1 : steerRightButton.IsHold ? 1 : 0;
        }
        
        if (_player._RaceStateTracker.State == RaceState.Passed)
        {
            _player.ActiveVehicle.BrakeControl = 1;
        }
        
        if (startButton.IsHold == true && IsStart == true)
        {
            startButton.gameObject.SetActive(false);
            _player._RaceStateTracker.StartRaceTimer();
            IsStart = false;
        }
    }
}
