using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClocker : MonoBehaviour
{
    // Start is called before the first frame update
    public int CTime;
    public float TTime;
    public bool Trigger=false;
    public bool ATrigger = false;
    public GameObject Main;
    private void Update()
    {
        if (Trigger)
        {
            Debug.Log("ㄱㄱ");
            if (ATrigger == false)
            {
                TTime = TTime + (CTime * Time.deltaTime * 5);
                this.transform.rotation = Quaternion.Euler(0, 0, TTime);
                Debug.Log(TTime);
            }
            if(TTime>360.0f)
            {
                ATrigger = true;
                Debug.Log("ATrigger True");
            }

        }
        if(Trigger&&ATrigger)
        {
            Trigger = false;
            Debug.Log("ATrigger True2");
            Main.GetComponent<Lobby>().CStart();
        }
    }
    public void Clock()
    {
        Trigger = true;
        CTime = 30;
        Debug.Log("11");

    }

}
