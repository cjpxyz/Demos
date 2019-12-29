using NetBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_NetworkController : Photon.PunBehaviour
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


    public void GetDemage(string name, float demage)
    {
        photonView.RPC("DoDemageInOthers", PhotonTargets.AllBuffered, name, demage);
    }

    [PunRPC]
    void DoDemageInOthers(string name, float demage)
    {
        if (VRFPS_GameController.instance.playerName == name)
        {
            if ((player1.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                VRFPS_CanvasController.instance.CallDeathScreen();
            }

            if (name == "Player 1")
            {
                player1.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 2")
            {
                player2.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 3")
            {
                player3.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 4")
            {
                player4.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 5")
            {
                player5.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 6")
            {
                player6.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 7")
            {
                player7.GetComponent<NetworkObject>().playerHealth -= demage;
            }
            else if (name == "Player 8")
            {
                player8.GetComponent<NetworkObject>().playerHealth -= demage;
            }

            Debug.Log("Remove demage: " + demage + " / from: " + name);
        }
    }
}
