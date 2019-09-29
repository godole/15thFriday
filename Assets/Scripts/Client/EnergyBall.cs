using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public GameObject m_EffectObj;
    public Ghost m_Ghost;

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
        if(other.tag == "Ghost")
        {
            m_EffectObj.SetActive(true);
            m_Ghost = other.GetComponent<Ghost>();
            m_Ghost.RangedEnergyBall(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ghost")
        {
            m_EffectObj.SetActive(false);
            m_Ghost.OutOfRangeBall();
            m_Ghost = null;
        }
    }
}
