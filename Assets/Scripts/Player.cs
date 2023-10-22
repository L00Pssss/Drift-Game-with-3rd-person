using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviourPunCallbacks
{ 
    
    public event UnityAction PlayerInitialized;

    
    [SerializeField] private Car _CarPrefab;

    [SerializeField] private Spawner _spawner;
    
    [SerializeField] private UIResult _uiResult;  // debug
    [SerializeField] private UITimerMatch _uITimerMatch;  // debug
    
    [SerializeField] private UICountDownTimer _uICountDownTimer;  // debug

    [SerializeField] private UIScore _uiScore;  // debug

    [SerializeField] private int currentSpawnIndex = 0;


    [SerializeField] private ColorReference _carMaterial;
    

    public Car ActiveVehicle {get;set;}
    public RaceStateTracker _RaceStateTracker => _raceStateTracker;

    public PlayerReferences PlayerReferences => _playerReferences;

    private PhotonView _photonView;
    
    private RaceStateTracker _raceStateTracker;
    
    [SerializeField] private PlayerReferences _playerReferences;

    private ScoreManager _scoreManager;
    
    
    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        _raceStateTracker = GetComponent<RaceStateTracker>();
        
        _playerReferences = FindObjectOfType<PlayerReferences>();
        
        if (_playerReferences != null)
        {
            _uiResult = _playerReferences.UIResult;
            _uITimerMatch = _playerReferences.UITimerMatch;
            _uICountDownTimer = _playerReferences.UICountDownTimer;
            _uiScore = _playerReferences.UIScore;
        }

        if (photonView.IsMine)
        {
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("currentSpawnIndex", out object indexObj))
            {
                // Если свойство уже существует, используем его значение
                currentSpawnIndex = (int)indexObj;
            }
            CmdSpawnVehicle();
        }
    }
    

    private void UpdateCustomRoomProperty()
    {
        // Обновляем кастомное свойство "currentSpawnIndex" на сервере
        ExitGames.Client.Photon.Hashtable customProperties = new ExitGames.Client.Photon.Hashtable();
        customProperties["currentSpawnIndex"] = currentSpawnIndex;
        PhotonNetwork.CurrentRoom.SetCustomProperties(customProperties);
    }
    private void CreateUIResult()
    {
        _uiResult.Initialize(_playerReferences);
        _uITimerMatch.Initialize(_playerReferences);
        _uICountDownTimer.Initialize(_playerReferences);
        _uiScore.Initialize(_playerReferences);
    }
    private void InitializeComponents()
    {
        _playerReferences.Initialize(this);
        
        _playerReferences.Initialize(_raceStateTracker);

        _playerReferences.Initialize(_scoreManager);

        CreateUIResult();
    }
    
    [PunRPC]
    private void CmdSpawnVehicle()
    {
        SvSpwanClintVehicle();
    }
    
    private void SvSpwanClintVehicle()
    {
        if (ActiveVehicle != null) return;
        
        

        Transform Postion = _spawner.Position[currentSpawnIndex];

        GameObject playerCar = PhotonNetwork.Instantiate(_CarPrefab.name, Postion.position, Postion.rotation);

        _carMaterial = playerCar.GetComponentInParent<ColorReference>();
        
        _carMaterial.SetMaterial(_playerReferences.Color());

        ActiveVehicle = playerCar.GetComponentInParent<Car>();
        
        _scoreManager = playerCar.GetComponentInParent<ScoreManager>();

        currentSpawnIndex = (currentSpawnIndex + 1) % _spawner.Position.Length;
        
        InitializeComponents();
        
        PlayerInitialized?.Invoke();

        UpdateCustomRoomProperty();

        PhotonView photonView = ActiveVehicle.GetComponent<PhotonView>();

        RpcSetVehicle(photonView);
    }

    
    
    [PunRPC]
    private void RpcSetVehicle(PhotonView viewID)
    {
        ActiveVehicle = viewID.GetComponent<Car>();

        if (CameraFollow.Instance != null && ActiveVehicle != null && viewID.IsMine)
        {
            CameraFollow.Instance.SetTarget(ActiveVehicle.transform, ActiveVehicle.Rigidbody); // передаем камеру.
        }
    }
}
