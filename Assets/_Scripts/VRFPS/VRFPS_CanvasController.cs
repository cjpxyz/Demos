using NetBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VRFPS_CanvasController : MonoBehaviour
{
    public static VRFPS_CanvasController instance;

    public GameObject mainCanvas;
    public GameObject camCanvas;

    [Header("Main Screens")]
    [Space(2)]
    public GameObject initialMenuScreen;
    public GameObject gameplayScreen;
    public GameObject loadingScreen;
    public GameObject deathScreen;
    public GameObject loseScreen;
    public GameObject winScreen;
    public GameObject versionContainer;
    public GameObject mainBG;

    [Header("Gameplay")]
    [Space(2)]
    public GameObject ammoContainer;
    public GameObject ammoCount;

    public GameObject healthContainer;
    public GameObject healthBar;

    public GameObject countScreen;
    public GameObject countText;

    public GameObject mapContainer;
    public GameObject mapImage;

    public GameObject roundInfosContainer;
    public GameObject roundTimeCount;
    public GameObject roundCount;
    public GameObject roundTeam1Points;
    public GameObject roundTeam2Points;

    [HideInInspector]
    public int currentTeam1Points = 0;
    public int currentTeam2Points = 0;

    

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
        DisableAllScreens();
        initialMenuScreen.SetActive(true);
        versionContainer.SetActive(true);
    }

    void Update()
    {
        if (VRFPS_GameController.instance.canCountStartTime)
        {
            VRFPS_GameController.instance.startMatchTime -= Time.deltaTime;
            int seconds = (int)(VRFPS_GameController.instance.startMatchTime % 60);

            countText.GetComponent<Text>().text = "" + seconds;
        }

        if (VRFPS_NetworkController.instance != null)
        {
            if (VRFPS_GameController.instance.playerName == "Player 1")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player1.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 2")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player2.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 3")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player3.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 4")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player4.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 5")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player5.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 6")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player6.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 7")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player7.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 8")
            {
                healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player8.GetComponent<NetworkObject>().playerHealth / 100;
            }
        }
    }

    public void SemiSetInitialGameInfos()
    {
        gameplayScreen.SetActive(true);
        countScreen.SetActive(true);
        countText.SetActive(true);

        ammoContainer.SetActive(false);
        healthContainer.SetActive(false);
        mapContainer.SetActive(false);
        roundInfosContainer.SetActive(false);

        healthBar.GetComponent<Image>().fillAmount = 1;
    }

    public void SetInitialGameInfos()
    {
        countScreen.SetActive(false);
        countText.SetActive(false);

        gameplayScreen.SetActive(true);

        ammoContainer.SetActive(true);
        healthContainer.SetActive(true);
        mapContainer.SetActive(true);
        roundInfosContainer.SetActive(true);

        healthBar.GetComponent<Image>().fillAmount = 1;

        ammoCount.GetComponent<Text>().text = "00" + VRFPS_GameController.instance.initialAmmoCount;
        roundTimeCount.GetComponent<Text>().text = "0" + VRFPS_GameController.instance.matchTime + ":00";
        roundCount.GetComponent<Text>().text = "" + (VRFPS_GameController.instance.initialTotalRounds - VRFPS_GameController.instance.initialTotalRounds);
    }

    public void UpdateBulletsNumber()
    {
        ammoCount.GetComponent<Text>().text = "00" + VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets;
    }

    public void GetHit(int team, string playerName)
    {
        /*if((VRFPS_GameController.instance.initialHealth - VRFPS_GameController.instance.initialHitDemage) >= 0)
        {
            VRFPS_GameController.instance.initialHealth -= VRFPS_GameController.instance.initialHitDemage;
            healthBar.GetComponent<Image>().fillAmount  m= VRFPS_GameController.instance.initialHealth / 100;
        }
        else
        {
            AddPointToTeam(team);
        }*/
    }

    public void AddPointToTeam(int team)
    {
        if(team == 1)
        {
            currentTeam1Points++;
            roundTeam1Points.GetComponent<Text>().text = "" + currentTeam1Points;
        }
        else
        {
            currentTeam2Points++;
            roundTeam2Points.GetComponent<Text>().text = "" + currentTeam2Points;
        }
    }

    public void CallDeathScreen()
    {
        DisableAllScreens();
        deathScreen.SetActive(true);
    }

    public void CallWinScreen()
    {
        DisableAllScreens();
        winScreen.SetActive(true);
    }

    public void CallLoseScreen()
    {
        DisableAllScreens();
        loseScreen.SetActive(true);
    }

    public void DisableAllScreens()
    {
        initialMenuScreen.SetActive(false);
        gameplayScreen.SetActive(false);
        loadingScreen.SetActive(false);
        deathScreen.SetActive(false);
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        versionContainer.SetActive(false);
        mainBG.SetActive(false);
    }
}
