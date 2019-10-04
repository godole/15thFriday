using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameSceneManager : MonoBehaviour
{
    public TextMeshPro[] m_NameText;
    public TextMeshPro[] m_ScoreText;
    public TextMeshPro m_ResultText;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(Player p in PhotonNetwork.PlayerList)
        {
            m_NameText[i].text = p.NickName;
            m_ScoreText[i].text = p.GetScore().ToString();
            i++;
        }

        var prop = PhotonNetwork.CurrentRoom.CustomProperties;

        if(prop.ContainsKey(GnG.WIN_TEAM))
        {
            GnG.TeamType t = (GnG.TeamType)prop[GnG.WIN_TEAM];

            m_ResultText.text = t == GnG.TeamType.Ghost ? "Ghost Win!" : "Reaper Win!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
