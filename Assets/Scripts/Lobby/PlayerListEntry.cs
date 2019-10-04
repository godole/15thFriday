using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayerListEntry : MonoBehaviour
{
    [Header("UI 자료")]
    public Text PlayerNameText;

    public Image ClientReadyImage;
    public Image PlayerHostImage;
    public Button PlayerReadyButton;


    private int ownerId;
    private bool isPlayerReady;


    public void Start()
    {
        GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        if (PhotonNetwork.LocalPlayer.ActorNumber != ownerId)
        {
            PlayerReadyButton.gameObject.SetActive(false);
        }
        else
        {
            Hashtable initialProps = new Hashtable() { {GameSceneManager.PLAYER_READY, isPlayerReady }};
            PhotonNetwork.LocalPlayer.SetCustomProperties(initialProps);
            PhotonNetwork.LocalPlayer.SetScore(0);
            if(PhotonNetwork.IsMasterClient)
            {
                PlayerHostImage.color = new Color(PlayerHostImage.color.r, PlayerHostImage.color.g, PlayerHostImage.color.b, 1.0f);
            }

            PlayerReadyButton.onClick.AddListener(() =>
            {
                isPlayerReady = !isPlayerReady;
                SetPlayerReady(isPlayerReady);

                Hashtable props = new Hashtable() { { GameSceneManager.PLAYER_READY, isPlayerReady } };
                PhotonNetwork.LocalPlayer.SetCustomProperties(props);

                if (PhotonNetwork.IsMasterClient)
                {
                    FindObjectOfType<Lobby>().LocalPlayerPropertiesUpdated();
                }
            });
        }
    }

    public void Initialize(int playerId, string playerName)
    {
        ownerId = playerId;
        PlayerNameText.text = playerName;
    }


    public void SetPlayerReady(bool playerReady)
    {
        PlayerReadyButton.GetComponentInChildren<Text>().text = playerReady ? "OK" : "Ready";
        ClientReadyImage.GetComponentInChildren<Text>().text = playerReady ? "OK" : "Ready";
    }
}
