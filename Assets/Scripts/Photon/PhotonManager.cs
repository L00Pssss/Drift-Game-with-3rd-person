using System;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.Serialization;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string _region;

    [SerializeField] private TMP_InputField  _roomName;

    [SerializeField] private ListItem _itemPrefab;
    [SerializeField] private Transform _content;

    [SerializeField] private GameObject _PanelPhoton;
    [SerializeField] private GameObject _Buttnos;
    

    [SerializeField] private Player _player;

    private List<RoomInfo> allRoomsInfo = new List<RoomInfo>();
    
    
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_region);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Is connected to region:  "  + PhotonNetwork.CloudRegion);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("You are disconnected");
    }

    public void CreateRoomButton()
    {
        if(!PhotonNetwork.IsConnected) return;
        
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        OnJoinedLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Create room is complete: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Error - RoomFailed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            for (int i = 0; i < allRoomsInfo.Count; i++)
            {
                if(allRoomsInfo[i].masterClientId == info.masterClientId)
                    return;
            }
            
            ListItem listItem = Instantiate(_itemPrefab, _content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
                allRoomsInfo.Add(info);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        _PanelPhoton.SetActive(false);
        _Buttnos.SetActive(true);
        PhotonNetwork.Instantiate(_player.name, transform.position, Quaternion.identity);
    }

    public void JoinButton()
    {
        PhotonNetwork.JoinRoom(_roomName.text);


    }
}
