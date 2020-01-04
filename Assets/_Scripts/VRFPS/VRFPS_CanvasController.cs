using UnityEngine;

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

    public GameObject respawnBtn;
    public GameObject backToMenuLoseBtn;
    public GameObject backToMenuWinBtn;

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
