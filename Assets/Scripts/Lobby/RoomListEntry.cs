using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RoomListEntry : MonoBehaviour
{
    public Text RoomNameText;
    public Text RoomPlayersText;
    public Button JoinRoomButton;
    public string roomName;

    public void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        JoinRoomButton.onClick.AddListener(() =>
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.LeaveLobby();
            }
            PhotonNetwork.JoinRoom(roomName);
        });
    }

    public void Initialize(string name, byte currentPlayers, byte maxPlayers)
    {
        roomName = name;
        RoomNameText.text = name;
        RoomPlayersText.text = currentPlayers + " / " + maxPlayers;
    }

}
