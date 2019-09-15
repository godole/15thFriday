using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;//MonoBehaviourPunCallbacks를 사용하기위해 부름
using Photon.Realtime;
using UnityEngine.UI;
//InputField 쓰려고 UI 씀.

public class NetworkManagerServer : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    //닉네임을 받기위한 인풋 필드
    public GameObject DisConnectPanel;
    //연결 할때 쓸 패널.
    public GameObject RespawnPanel;
    //다시 연결해줄때 쓸 패널.

    public bool SpawnP = true;
    //스폰 확인용

    private void Awake()
    //최초로 네트워크를 바로 연결 하도록함
    {
        Screen.SetResolution(960, 540, false);
        //가로 960, 세로 540로 맞춤
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        //서로 주고받는게 있어서 이렇게 높이면 동기화가 더 빨리된다함.
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings(); //유싱 세팅을 부름

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        //InputField에서 쓴 닉네임을 바로 보냄.
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
        //불러지면 조인 크리에이트룸으로 바로 방과, 최대 인원 6명
    }

    public override void OnJoinedRoom() //방에 들어가게되면.
    {
        DisConnectPanel.SetActive(false);
        //접속 패널을 false 상태로 만들어줌
        Spawn();
        //플레이어를 넣어줌.
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();
        //포톤네트워크가 연결된 상태에서 ESC 버튼을 누르게 되면 포톤 네트워크를 연결해제를 해줌

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        DisConnectPanel.SetActive(true);
        //연결패널 활성화
        RespawnPanel.SetActive(false);
        //재연결패널 비활성화
    }

    public void Spawn()
    {
        PhotonNetwork.Instantiate("ReaperServer", new Vector3(Random.Range(-6f,20f),4,0), Quaternion.identity);
        RespawnPanel.SetActive(false);
        SpawnP = false;
        //스폰이 될때 Reaper프리벱을 가져온다. 그리고 연결 패널을 지운다.
    }
}
