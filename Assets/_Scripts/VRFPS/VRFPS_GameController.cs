using DG.Tweening;
using NetBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRFPS_GameController : MonoBehaviour
{
    public static VRFPS_GameController instance;

    [Header("Player Initial Values")]
    [Space(2)]
    public int initialAmmoCount;
    public int initialHealth;
    public int initialHitDemage;
    public int initialTotalRounds;

    public bool canStartMacth;
    public bool canCountStartTime;
    public bool isInMatch;
    public bool macthEnd = false;
    public float startMatchTime;

    public float matchTime;
    public bool isConnectedInPhoton;

    public GameObject playerVRPrefab;
    public GameObject vRScriptsPrefab;

    public bool inGameScene;
    public GameObject myAvatarObject;
    public string playerName;

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
        if (isInMatch && !macthEnd)
        {
            matchTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(matchTime / 60F);
            int seconds = Mathf.FloorToInt(matchTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            VRFPS_CanvasController.instance.roundTimeCount.GetComponent<Text>().text = niceTime;
            //VRFPS_CanvasController.instance.ammoCount.GetComponent<Text>().text = "00" + initialAmmoCount;
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartSequence());
    }

    public void Respawn()
    {

    }

    public void BackToMenu()
    {
        VRFPS_SceneManager.instance.BackToMenu();
    }

    private IEnumerator StartSequence()
    {
        yield return new WaitUntil(() => isConnectedInPhoton);
        VRFPS_CanvasActions.instance.SemiSetInitialGameInfos();

        canStartMacth = true;

        yield return new WaitUntil(() => canStartMacth);
        VRFPS_CanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.9f, 2f);
        VRFPS_VRCanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.9f, 2f);

        canCountStartTime = true;

        yield return new WaitForSeconds(startMatchTime);
        VRFPS_CanvasActions.instance.SetInitialGameInfos();
        isInMatch = true;
    }

}
