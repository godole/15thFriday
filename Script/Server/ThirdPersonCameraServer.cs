using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ThirdPersonCameraServer : MonoBehaviourPunCallbacks
{
    public GameObject Player;
    public NetworkManagerServer NM;

    public Transform m_LookAt;
    public Transform m_CamTransform;

    public float m_MaxY;
    public float m_MinY;

    private Camera m_Cam;

    public float m_SensivityX = 4.0f;
    public float m_SensivityY = 1.0f;
    public float distance = 10.0f;

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    // Start is called before the first frame update
    private void Awake()
    {
        NM = FindObjectOfType<NetworkManagerServer>();
    }
    void Start()
    {
        m_CamTransform = transform;
        m_Cam = Camera.main;

    }

    private void Update()
    {
        if (NM.SpawnP == false)
        {
            
            //Player = FindObjectOfType<Reaper>().gameObject;
            //m_LookAt = Player.transform;
            //Missing 우려로 자동으로 잡아줌.
            currentX += Input.GetAxis("Mouse X");
            currentY += Input.GetAxis("Mouse Y");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (NM.SpawnP == false)
        {
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
            m_CamTransform.position = m_LookAt.position + rot * dir;
            m_CamTransform.LookAt(m_LookAt.position);
        }
        /*Vector3 r = m_CamTransform.rotation.eulerAngles;
        r.x = Mathf.Clamp(r.x, m_MinY, m_MaxY);
        m_CamTransform.rotation = new Quaternion(r.x, r.y, r.z, 1);*/
    }
}
