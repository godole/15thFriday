using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class CurrentState : MonoBehaviour
{
    private readonly string connectionStatusMessage = "상태 : ";

    [Header("UI작용")]
    public Text ConnectionStatusText;

    public void Update()
    {
        ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
    }
}
