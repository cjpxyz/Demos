using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_NetworkController : MonoBehaviour
{
    public static VRFPS_NetworkController instance;

    [Header("Team Infos")]
    [Space(2)]
    public int playersInRoom;

    public int team1Points;
    public int team2Points;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;

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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(playersInRoom);
            stream.SendNext(team1Points);
            stream.SendNext(team2Points);
        }
        else
        {
            playersInRoom = (int)stream.ReceiveNext();
            team1Points = (int)stream.ReceiveNext();
            team2Points = (int)stream.ReceiveNext();
        }
    }
}
