using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VRFPS_PhotonNetworkController : Photon.MonoBehaviour
{
    public static VRFPS_PhotonNetworkController instance;

    private string gameVersion;

    private int currentGameModeNumber;
    private bool hasTryAfter = false;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameVersion = "SuperBolaGol(0.1)";
    }

    public void ConnectToPhoton()
    {
        Debug.Log("Try ConnectToPhoton");
        PhotonNetwork.ConnectUsingSettings(gameVersion);
    }

    public void OnConnectedToPhoton()
    {
        Debug.Log("OnConnectedToPhoton");
    }

    public void OnConnectedToMaster()
    {
        AfterPhotonInLobby();
        Debug.Log("OnConnectedToMaster");
    }

    public void OnJoinedLobby()
    {
        AfterPhotonInLobby();
        Debug.Log("Entrou no Lobby do Photon!");
    }

    public void AfterPhotonInLobby()
    {
        VRFPS_PhotonFindRoomController.instance.TryJoinRoom(1, 1);
        Debug.Log("Entrou no lobby tentar criar sala!");
    }
}
