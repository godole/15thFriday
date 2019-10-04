using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public GameObject m_EffectObj;
    public Ghost m_Ghost;
    public MeshRenderer m_Rend;

    bool m_IsActive = true;

    public bool IsActive { get => m_IsActive; set => m_IsActive = value; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_IsActive)
            return;

        if(other.tag == "Ghost" && !PhotonNetwork.IsMasterClient)
            m_EffectObj.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!m_IsActive)
            return;

        if (other.tag == "Ghost" && !PhotonNetwork.IsMasterClient)
            m_EffectObj.SetActive(false);
    }

    public void Return()
    {
        m_IsActive = true;
        m_Rend.enabled = true;
    }

    public void Eaten()
    {
        m_IsActive = false;
        m_Rend.enabled = false;
        m_EffectObj.SetActive(false);
    }
}
