using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTestScript : MonoBehaviour
{
    public GameSceneManager m_GM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            m_GM.AddEnergy(1);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            m_GM.EndGame(GnG.TeamType.Ghost);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            m_GM.EndGame(GnG.TeamType.Reaper);
        }
    }
}
