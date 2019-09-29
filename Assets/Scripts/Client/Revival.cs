using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Revival : MonoBehaviour
{
    GameSceneManager m_GameManager;
    int m_RevivalGhostCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_GameManager = FindObjectOfType<GameSceneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ghost")
        {
            m_RevivalGhostCount++;
            other.GetComponent<Ghost>().EnterRevival();

            Hashtable props = new Hashtable
                    {
                        {GnG.REVIVAL_GHOST_COUNT, (int) m_RevivalGhostCount}
                    };

            PhotonNetwork.CurrentRoom.SetCustomProperties(props);
        }
    }
}
