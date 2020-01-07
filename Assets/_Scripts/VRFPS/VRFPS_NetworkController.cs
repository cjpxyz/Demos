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

    public int currentRound;
    public int playersDeathTeam1;
    public int playersDeathTeam2;
    public GameObject currentGO;
    public bool hasGO;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    public GameObject player5;
    public GameObject player6;
    public GameObject player7;
    public GameObject player8;

    public bool player1IsDead;
    public bool player2IsDead;
    public bool player3IsDead;
    public bool player4IsDead;
    public bool player5IsDead;
    public bool player6IsDead;
    public bool player7IsDead;
    public bool player8IsDead;

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
            stream.SendNext(currentRound);

            stream.SendNext(player1IsDead);
            stream.SendNext(player2IsDead);
            stream.SendNext(player3IsDead);
            stream.SendNext(player4IsDead);
            stream.SendNext(player5IsDead);
            stream.SendNext(player6IsDead);
            stream.SendNext(player7IsDead);
            stream.SendNext(player8IsDead);
        }
        else
        {
            playersInRoom = (int)stream.ReceiveNext();
            team1Points = (int)stream.ReceiveNext();
            team2Points = (int)stream.ReceiveNext();
            currentRound = (int)stream.ReceiveNext();

            player1IsDead = (bool)stream.ReceiveNext();
            player2IsDead = (bool)stream.ReceiveNext();
            player3IsDead = (bool)stream.ReceiveNext();
            player4IsDead = (bool)stream.ReceiveNext();
            player5IsDead = (bool)stream.ReceiveNext();
            player6IsDead = (bool)stream.ReceiveNext();
            player7IsDead = (bool)stream.ReceiveNext();
            player8IsDead = (bool)stream.ReceiveNext();
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
            currentGO.GetComponent<NetworkObject>().playerHealth -= demage;

            if ((currentGO.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                VRFPS_CanvasActions.instance.CallDeathScreen();
            }

            //Debug.Log("Remove demage: " + demage + " / current demage: " + currentGO.GetComponent<NetworkObject>().playerHealth + " / from: " + name);
        }

        if ((currentGO.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
        {
            currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetTrigger("death");
        }

        int playerTeam = 0;
        bool playerDead = false;

        if (name == "Player 1")
        {
            playerTeam = player1.GetComponent<NetworkObject>().playerTeam;

            if ((player1.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player1IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 2")
        {
            playerTeam = player2.GetComponent<NetworkObject>().playerTeam;

            if ((player2.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player2IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 3")
        {
            playerTeam = player3.GetComponent<NetworkObject>().playerTeam;

            if ((player3.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player3IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 4")
        {
            playerTeam = player4.GetComponent<NetworkObject>().playerTeam;

            if ((player4.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player4IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 5")
        {
            playerTeam = player5.GetComponent<NetworkObject>().playerTeam;

            if ((player5.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player5IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 6")
        {
            playerTeam = player6.GetComponent<NetworkObject>().playerTeam;

            if ((player6.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player6IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 7")
        {
            playerTeam = player7.GetComponent<NetworkObject>().playerTeam;

            if ((player7.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player7IsDead = true;
                playerDead = true;
            }
        }
        else if (name == "Player 8")
        {
            playerTeam = player8.GetComponent<NetworkObject>().playerTeam;

            if ((player8.GetComponent<NetworkObject>().playerHealth - demage) <= 0)
            {
                player8IsDead = true;
                playerDead = true;
            }
        }

        Debug.Log("playerDead: " + playerDead + " / playerName: " + name + " / player1Heath: " + player1.GetComponent<NetworkObject>().playerHealth + " / player2Heath: " + player2.GetComponent<NetworkObject>().playerHealth);

        if (playerDead)
        {
            playerDead = false;
            //VRFPS_CanvasActions.instance.AddPointToTeam(playerTeam);
            StartCoroutine(DoDemageInOthersSequence(name));
        }
    }

    private IEnumerator DoDemageInOthersSequence(string name)
    {
        Debug.Log("1 DoDemageInOthersSequence: " + name);
        yield return new WaitForSeconds(3f);
        Debug.Log("2 DoDemageInOthersSequence: " + name);
    }

    
    public void CallResetRoundInfos()
    {
        photonView.RPC("CallResetRoundInfosToOthers", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void CallResetRoundInfosToOthers()
    {
        StartCoroutine(CallResetRoundInfosSequence());
    }

    private IEnumerator CallResetRoundInfosSequence()
    {
        player1IsDead = false;
        player2IsDead = false;
        player3IsDead = false;
        player4IsDead = false;
        player5IsDead = false;
        player6IsDead = false;
        player7IsDead = false;
        player8IsDead = false;

        VRFPS_CanvasActions.instance.isDeath = false;

        player1.GetComponent<NetworkObject>().playerHealth = 100;
        player2.GetComponent<NetworkObject>().playerHealth = 100;

        yield return new WaitForSeconds(0.5f);
        VRFPS_CanvasActions.instance.CallRespawn();
    }
}
