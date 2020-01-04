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

    [HideInInspector]
    public GameObject currentGO;

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

    void Start()
    {
        if (VRFPS_GameController.instance.playerName == "Player 1")
        {
            currentGO = player1;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 2")
        {
            currentGO = player2;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 3")
        {
            currentGO = player3;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 4")
        {
            currentGO = player4;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 5")
        {
            currentGO = player5;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 6")
        {
            currentGO = player6;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 7")
        {
            currentGO = player7;
        }
        else if (VRFPS_GameController.instance.playerName == "Player 8")
        {
            currentGO = player8;
        }
    }

    private void Update()
    {
        //Debug.Log("MS: " + PhotonNetwork.networkingPeer.RoundTripTime);
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

    public bool IsDead
    {
        get
        {
            return currentGO.GetComponent<NetworkObject>().playerHealth <= 0;
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
            if((currentGO.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                VRFPS_CanvasController.instance.CallDeathScreen();
            }

            currentGO.GetComponent<NetworkObject>().playerHealth -= demage;

            Debug.Log("Remove demage: " + demage + " / from: " + name);
        }

        if ((currentGO.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
        {
            currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetTrigger("death");
        }
    }
}
