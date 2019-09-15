using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class UserInputServer : MonoBehaviourPunCallbacks,IPunObservable
{
    public CharacterBaseServer m_Client;
    public PhotonView PV;
    public Text NickNameText;
    public ThirdPersonCameraServer TPC;

    Vector3 curPos;

    // Start is called before the first frame update
    void Awake()
    {
        PV = FindObjectOfType<PhotonView>();
        //닉네임
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        //포톤뷰가 내꺼면 내이름 넣고 아니면 그 포톤뷰 주인의 이름을 넣음
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
        //마찬가지로 포톤뷰가 내꺼면 초록색 아니면 빨간색.
        TPC = FindObjectOfType<ThirdPersonCameraServer>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            m_Client.Attack();
        }
    }

    private void FixedUpdate()
    {
        if (PV.IsMine)
        {
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            m_Client.Move(horizontal, vertical);
            TPC.m_LookAt = m_Client.transform;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}
