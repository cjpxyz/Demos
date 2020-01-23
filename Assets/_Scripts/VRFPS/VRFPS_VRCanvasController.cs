using UnityEngine;

public class VRFPS_VRCanvasController : MonoBehaviour
{
    public static VRFPS_VRCanvasController instance;

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
    public GameObject drawScreen;
    public GameObject versionContainer;
    public GameObject mainBG;
    public GameObject playBtn;

    [Header("Configs")]
    [Space(2)]
    public GameObject roomConfigsContainer;
    public GameObject chooseMode;
    public GameObject chooseTeam;
    public GameObject chooseName;
    public GameObject chooseBtn;

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

    public GameObject respawnBtn;
    public GameObject backToMenuLoseBtn;
    public GameObject backToMenuWinBtn;
    public GameObject backToMenuDrawBtn;

    public GameObject countMS;
    public Color niceMsColor;
    public Color mediumMsColor;
    public Color badMsColor;

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
}
