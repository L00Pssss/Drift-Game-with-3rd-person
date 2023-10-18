using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textName;
    [SerializeField] private TextMeshProUGUI _textPlayerCount;

    public void SetInfo(RoomInfo info)
    {
        _textName.text = info.Name;
        _textPlayerCount.text = info.PlayerCount + "/" + info.MaxPlayers;
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(_textName.text);
    }
}
