using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Gameplay")]
    [Space(2)]
    public GameObject ammoContainer;
    public GameObject ammoCount;

    public GameObject healthContainer;
    public GameObject healthBar;

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

    public void SetInitialGameInfos()
    {
        gameplayScreen.SetActive(true);

        ammoContainer.SetActive(true);
        healthContainer.SetActive(true);
        mapContainer.SetActive(true);
        roundInfosContainer.SetActive(true);

        ammoCount.GetComponent<Text>().text = "00" + VRFPS_GameController.instance.initialAmmoCount;
        roundTimeCount.GetComponent<Text>().text = "0" + VRFPS_GameController.instance.matchTime + ":00";
        roundCount.GetComponent<Text>().text = "" + (VRFPS_GameController.instance.initialTotalRounds - VRFPS_GameController.instance.initialTotalRounds);
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
}
