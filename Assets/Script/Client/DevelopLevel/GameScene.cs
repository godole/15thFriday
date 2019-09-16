using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public GameObject m_Reaper;
    public GameObject m_ThirdPersonCamera;

    public GameObject m_Ghost;
    public GameObject m_FirstPersonCamera;

    public Vector3 m_StartPosition;

    public int m_ClientId;

    private void Awake()
    {
        CreateCharacter();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateCharacter()
    {
        GameObject character = null;
        var iptmgr = FindObjectOfType<UserInput>();

        if (m_ClientId == 1)
        {
            character = Instantiate(m_Reaper, m_StartPosition, new Quaternion());
            var cam = Instantiate(m_FirstPersonCamera);
            var camcom = cam.GetComponent<FirstPersonCamera>();
            camcom.m_Targer = character.transform;
            iptmgr.m_Client = character.GetComponent<Reaper>();
            iptmgr.m_Camera = camcom;
        }
        
        else
        {
            var cam = Instantiate(m_ThirdPersonCamera, m_StartPosition, new Quaternion());
            character = Instantiate(m_Ghost, m_StartPosition, new Quaternion());
            var camcom = cam.GetComponent<ThirdPersonCamera>();
            camcom.m_LookAt = character.transform;
            iptmgr.m_Client = character.GetComponent<Ghost>();
            iptmgr.m_Camera = camcom;
        }
    }
}
