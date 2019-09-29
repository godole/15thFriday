using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Pun;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviourPunCallbacks
{
    public UserInput m_UserInput;
    public GameObject m_ReaperStartPosition;
    public GameObject[] m_GhostStartPosition;

    public GameObject m_RevivalDoor;

    public Slider m_Slider;

    int m_CurrentEnergyCount;

    public override void OnEnable()
    {
        base.OnEnable();

        CountdownTimer.OnCountdownTimerHasExpired += OnCountdownTimerIsExpired;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        CountdownTimer.OnCountdownTimerHasExpired -= OnCountdownTimerIsExpired;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            var reaper = PhotonNetwork.Instantiate("Reaper", m_ReaperStartPosition.transform.position, m_ReaperStartPosition.transform.rotation, 0);

            var cam = Camera.main.gameObject.AddComponent<FirstPersonCamera>();
            cam.m_Targer = reaper.transform;

            m_UserInput.m_Camera = cam;
            m_UserInput.m_Client = reaper.GetComponent<Reaper>();
        }
        else
        {
            int id = PhotonNetwork.LocalPlayer.ActorNumber;
            var ghost = PhotonNetwork.Instantiate("Ghost", m_GhostStartPosition[id].transform.position, m_GhostStartPosition[id].transform.rotation, 0);

            var cam = Camera.main.gameObject.AddComponent<ThirdPersonCamera>();
            cam.m_LookAt = ghost.transform;

            m_UserInput.m_Camera = cam;
            m_UserInput.m_Client = ghost.GetComponent<Ghost>();
        }

        Hashtable props = new Hashtable
                    {
                        {CountdownTimer.CountdownStartTime, (float) PhotonNetwork.Time}
                    };
        PhotonNetwork.CurrentRoom.SetCustomProperties(props);

        Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
        Cursor.visible = false;//마우스 커서 보이기
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEnergy(int energy)
    {
        Hashtable props = new Hashtable
                    {
                        {GnG.EAT_ENERGY, m_CurrentEnergyCount + energy}
                    };

        PhotonNetwork.CurrentRoom.SetCustomProperties(props);
    }

    private void OnCountdownTimerIsExpired()
    {
        EndGame();
    }

    public void EndGame()
    {
        PhotonNetwork.LoadLevel("EndGame");
    }

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        if(propertiesThatChanged.ContainsKey(GnG.EAT_ENERGY))
        {
            int energy = (int)propertiesThatChanged[GnG.EAT_ENERGY];
            m_CurrentEnergyCount = energy;

            m_Slider.value = energy / GnG.MAX_ENERGY_COUNT;

            if (m_CurrentEnergyCount >= GnG.MAX_ENERGY_COUNT)
            {
                m_RevivalDoor.SetActive(true);
            }
        }

        if(propertiesThatChanged.ContainsKey(GnG.REVIVAL_GHOST_COUNT))
        {
            int revGhost = (int)propertiesThatChanged[GnG.REVIVAL_GHOST_COUNT];
            if (revGhost >= PhotonNetwork.CurrentRoom.PlayerCount - 1)
            {
                EndGame();
            }
        }
    }
}
