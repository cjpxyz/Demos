using UnityEngine;
using ExitGames.Client.Photon;

public class VRFPS_PhotonFindRoomController : Photon.MonoBehaviour
{
    public static VRFPS_PhotonFindRoomController instance;

    public const byte maxPlayerFromTable = 8;
    private object levelFilter;
    private object roomType;

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

    public void TryJoinRoom(object type, object level)
    {
        levelFilter = level;
        roomType = type;

        JoinRandom(type, level);
    }

    public void CreateRoom(object type, object level)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CustomRoomPropertiesForLobby = new string[2] { "t", "l" };
        roomOptions.MaxPlayers = maxPlayerFromTable;
        roomOptions.CustomRoomProperties = new Hashtable(2) { { "t", type }, { "l", level } }; // add this line
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public void JoinRandom(object type, object level)
    {
        Hashtable expectedCustomRoomProperties = new Hashtable { { "t", type }, { "l", level } };

        if ((int)type == 1)
        {
            Debug.Log("Try JoinRadom: " + type + " with level: " + level);
        }
        else if ((int)type == 2)
        {
            Debug.Log("Try JoinRadom: " + type + " with level: " + level);
        }

        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, maxPlayerFromTable);
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom() : Você criou a sala: " + PhotonNetwork.room.Name);
    }

    public void OnJoinedRoom()
    {
        VRFPS_GameController.instance.isConnectedInPhoton = true;
        Debug.Log("OnJoinedRoom() : Você entrou na sala: " + PhotonNetwork.room.Name);
    }

    public void OnPhotonRandomJoinFailed()
    {
        CreateRoom(roomType, levelFilter);
        Debug.Log("OnPhotonRandomJoinFailed() : Não existem salas...");
    }

    public void OnPhotonPlayerConnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerConnected() " + other.NickName); // Mostra quando outras contas são conectadas!
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.NickName); // Mostra quando outras contas são desconectadas!
        PhotonNetwork.Disconnect();
    }

    public void OnMasterClientSwitched()
    {
        Debug.Log("OnMasterClientSwitched()");

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("isMasterClient " + PhotonNetwork.isMasterClient); // Chamado antes do: OnPhotonPlayerDisconnected
        }
    }

    public void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnPhotonPlayerDisconnected");
    }

    void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        foreach (RoomInfo roomInfo in rooms)
        {
            Debug.Log("Nome da Sala: " + roomInfo.Name + " / Jogadores na sala: " + roomInfo.PlayerCount + " / Máximo de Players: " + roomInfo.MaxPlayers + " / Type: " + roomInfo.CustomProperties["t"] + " / Level: " + roomInfo.CustomProperties["l"]);
        }
    }

    public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey("startTime") && propertiesThatChanged.ContainsKey("duration"))
        {
            Debug.Log("CanStart");
        }
    }
}
