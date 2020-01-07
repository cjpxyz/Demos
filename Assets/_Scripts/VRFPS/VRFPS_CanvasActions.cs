using DG.Tweening;
using NetBase;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class VRFPS_CanvasActions : MonoBehaviour
{
    public static VRFPS_CanvasActions instance;

    public bool isDeath;

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

        //Normal Canvas
        VRFPS_CanvasController.instance.initialMenuScreen.SetActive(true);
        VRFPS_CanvasController.instance.versionContainer.SetActive(true);

        //VR Canvas
        if (VRFPS_VRCanvasController.instance != null)
        {
            VRFPS_VRCanvasController.instance.initialMenuScreen.SetActive(true);
            VRFPS_VRCanvasController.instance.versionContainer.SetActive(true);
        }
    }

    void Update()
    {
        if (VRFPS_GameController.instance.canCountStartTime)
        {
            VRFPS_GameController.instance.startMatchTime -= Time.deltaTime;
            int seconds = (int)(VRFPS_GameController.instance.startMatchTime % 60);

            //Normal Canvas
            VRFPS_CanvasController.instance.countText.GetComponent<Text>().text = "" + seconds;

            //VR Canvas
            if (VRFPS_VRCanvasController.instance != null)
            {
                VRFPS_VRCanvasController.instance.countText.GetComponent<Text>().text = "" + seconds;
            }

            if(seconds <= 0)
            {
                VRFPS_GameController.instance.startMatchTime = 10;
                VRFPS_GameController.instance.canCountStartTime = false;
            }
        }

        if (VRFPS_NetworkController.instance != null)
        {
            if (VRFPS_GameController.instance.playerName == "Player 1")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player1.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player1.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 2")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player2.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player2.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 3")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player3.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player3.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 4")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player4.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player4.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 5")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player5.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player5.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 6")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player6.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player6.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 7")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player7.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player6.GetComponent<NetworkObject>().playerHealth / 100;
            }
            else if (VRFPS_GameController.instance.playerName == "Player 8")
            {
                VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player8.GetComponent<NetworkObject>().playerHealth / 100;
                VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = VRFPS_NetworkController.instance.player6.GetComponent<NetworkObject>().playerHealth / 100;
            }
        }

        if (PhotonNetwork.connected)
        {
            if (PhotonNetwork.GetPing() < 50)
            {
                VRFPS_CanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_CanvasController.instance.niceMsColor;
            }
            else if (PhotonNetwork.GetPing() < 120)
            {
                VRFPS_CanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_CanvasController.instance.mediumMsColor;
            }
            else
            {
                VRFPS_CanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_CanvasController.instance.badMsColor;
            }

            VRFPS_CanvasController.instance.countMS.GetComponent<Text>().text = PhotonNetwork.GetPing() + "ms";

            if (VRFPS_VRCanvasController.instance != null)
            {
                if (PhotonNetwork.GetPing() < 50)
                {
                    VRFPS_VRCanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_VRCanvasController.instance.niceMsColor;
                }
                else if (PhotonNetwork.GetPing() < 120)
                {
                    VRFPS_VRCanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_VRCanvasController.instance.mediumMsColor;
                }
                else
                {
                    VRFPS_VRCanvasController.instance.countMS.GetComponent<Text>().color = VRFPS_VRCanvasController.instance.badMsColor;
                }

                VRFPS_VRCanvasController.instance.countMS.GetComponent<Text>().text = PhotonNetwork.GetPing() + "ms";
            }
        }
    }

    public void SemiSetInitialGameInfos()
    {
        //Normal Canvas
        VRFPS_CanvasController.instance.gameplayScreen.SetActive(true);
        VRFPS_CanvasController.instance.countScreen.SetActive(true);
        VRFPS_CanvasController.instance.countText.SetActive(true);

        VRFPS_CanvasController.instance.ammoContainer.SetActive(false);
        VRFPS_CanvasController.instance.healthContainer.SetActive(false);
        VRFPS_CanvasController.instance.mapContainer.SetActive(false);
        VRFPS_CanvasController.instance.roundInfosContainer.SetActive(false);

        VRFPS_CanvasController.instance.versionContainer.SetActive(false);
        VRFPS_CanvasController.instance.mainBG.GetComponent<Image>().DOFade(1f, 0f);
        VRFPS_CanvasController.instance.mainBG.SetActive(true);

        VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = 1;

        //VR Canvas
        VRFPS_VRCanvasController.instance.gameplayScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.countScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.countText.SetActive(true);

        VRFPS_VRCanvasController.instance.ammoContainer.SetActive(false);
        VRFPS_VRCanvasController.instance.healthContainer.SetActive(false);
        VRFPS_VRCanvasController.instance.mapContainer.SetActive(false);
        VRFPS_VRCanvasController.instance.roundInfosContainer.SetActive(false);

        VRFPS_VRCanvasController.instance.versionContainer.SetActive(false);
        VRFPS_VRCanvasController.instance.mainBG.GetComponent<Image>().DOFade(1f, 0f);
        VRFPS_VRCanvasController.instance.mainBG.SetActive(true);

        VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = 1;
    }

    public void SetInitialGameInfos()
    {
        //Normal Canvas
        VRFPS_CanvasController.instance.countScreen.SetActive(false);
        VRFPS_CanvasController.instance.countText.SetActive(false);
        VRFPS_CanvasController.instance.gameplayScreen.SetActive(true);
        VRFPS_CanvasController.instance.ammoContainer.SetActive(true);
        VRFPS_CanvasController.instance.healthContainer.SetActive(true);
        VRFPS_CanvasController.instance.mapContainer.SetActive(true);
        VRFPS_CanvasController.instance.roundInfosContainer.SetActive(true);
        VRFPS_CanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.0f, 2f);

        VRFPS_CanvasController.instance.healthBar.GetComponent<Image>().fillAmount = 1;

        VRFPS_CanvasController.instance.ammoCount.GetComponent<Text>().text = "" + VRFPS_GameController.instance.initialAmmoCount;
        VRFPS_CanvasController.instance.roundTimeCount.GetComponent<Text>().text = "0" + VRFPS_GameController.instance.matchTime + ":00";
        VRFPS_CanvasController.instance.roundCount.GetComponent<Text>().text = "" + (VRFPS_GameController.instance.initialTotalRounds - VRFPS_GameController.instance.initialTotalRounds);

        //VR Canvas
        VRFPS_VRCanvasController.instance.countScreen.SetActive(false);
        VRFPS_VRCanvasController.instance.countText.SetActive(false);
        VRFPS_VRCanvasController.instance.gameplayScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.ammoContainer.SetActive(true);
        VRFPS_VRCanvasController.instance.healthContainer.SetActive(true);
        VRFPS_VRCanvasController.instance.mapContainer.SetActive(true);
        VRFPS_VRCanvasController.instance.roundInfosContainer.SetActive(true);
        VRFPS_VRCanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.0f, 2f);

        VRFPS_VRCanvasController.instance.healthBar.GetComponent<Image>().fillAmount = 1;

        VRFPS_VRCanvasController.instance.ammoCount.GetComponent<Text>().text = "" + VRFPS_GameController.instance.initialAmmoCount;
        VRFPS_VRCanvasController.instance.roundTimeCount.GetComponent<Text>().text = "0" + VRFPS_GameController.instance.matchTime + ":00";
        VRFPS_VRCanvasController.instance.roundCount.GetComponent<Text>().text = "" + (VRFPS_GameController.instance.initialTotalRounds - VRFPS_GameController.instance.initialTotalRounds);

        if (VRTK_SDKManager.instance.loadedSetup.headsetSDKInfo.type.Name != "SDK_SimHeadset")
        {
            VRFPS_CanvasController.instance.mainCanvas.SetActive(false);
        }

        Debug.Log("HeadsetSDKInfo Name: " + VRTK_SDKManager.instance.loadedSetup.headsetSDKInfo.type.Name);
        DeathCamToPos2();
    }

    private void DeathCamToPos1()
    {
        Invoke("CallDeathCamToPos1", 2f);
    }

    private void CallDeathCamToPos1()
    {
        VRFPS_CanvasController.instance.camCanvas.transform.DOLocalMoveZ(-5f, 60f).OnComplete(DeathCamToPos2);
    }

    private void DeathCamToPos2()
    {
        VRFPS_CanvasController.instance.camCanvas.transform.DOLocalMoveZ(-20f, 60f).OnComplete(DeathCamToPos1);
    }

    public void UpdateBulletsNumber()
    {
        VRFPS_CanvasController.instance.ammoCount.GetComponent<Text>().text = "" + VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets;
        VRFPS_VRCanvasController.instance.ammoCount.GetComponent<Text>().text = "" + VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets;
    }

    public void AddPointToTeam(int team)
    {
        if (team == 2)
        {
            VRFPS_CanvasController.instance.currentTeam1Points++;
            VRFPS_CanvasController.instance.roundTeam1Points.GetComponent<Text>().text = "" + VRFPS_CanvasController.instance.currentTeam1Points;

            VRFPS_VRCanvasController.instance.currentTeam1Points++;
            VRFPS_VRCanvasController.instance.roundTeam1Points.GetComponent<Text>().text = "" + VRFPS_VRCanvasController.instance.currentTeam1Points;

            VRFPS_NetworkController.instance.team1Points++;
        }
        else
        {
            VRFPS_CanvasController.instance.currentTeam2Points++;
            VRFPS_CanvasController.instance.roundTeam2Points.GetComponent<Text>().text = "" + VRFPS_CanvasController.instance.currentTeam2Points;

            VRFPS_VRCanvasController.instance.currentTeam2Points++;
            VRFPS_VRCanvasController.instance.roundTeam2Points.GetComponent<Text>().text = "" + VRFPS_VRCanvasController.instance.currentTeam2Points;

            VRFPS_NetworkController.instance.team2Points++;
        }
    }

    public void CallDeathScreen()
    {
        StartCoroutine(CallDeathScreenRoutine());
    }

    private IEnumerator CallDeathScreenRoutine()
    {
        if (!isDeath)
        {
            isDeath = true;

            DisableAllScreens();
            EnableDeathScreen();

            Transform savePos = VRFPS_NetworkController.instance.currentGO.transform;

            yield return new WaitForSeconds(3);
            //VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.SetActive(false);
            VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().deathAnimationIsEnd = false;
            VRFPS_NetworkController.instance.currentGO.transform.DOLocalMoveY(-1f, 1f);

            yield return new WaitForSeconds(1.5f);
            PlayoVR.AvatarSpawnManager.instance.oculusVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;
            PlayoVR.AvatarSpawnManager.instance.simulatorVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;
            PlayoVR.AvatarSpawnManager.instance.steamVrVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;

            VRFPS_NetworkController.instance.currentGO.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;

            yield return new WaitForSeconds(0.5f);
            VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetTrigger("idle");
            //PlayoVR.AvatarSpawnManager.instance.SpawnAmmo(savePos, 2);

            if (PlayoVR.AvatarSpawnManager.instance.currentGameMode == PlayoVR.GameMode.Mode1x1)
            {
                AddPointToTeam(VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam);
                VRFPS_NetworkController.instance.CallResetRoundInfos();
            }

            //Debug.Log("Instanciar bullets!")
        }
    }

    public void EnableDeathScreen()
    {
        VRFPS_CanvasController.instance.respawnBtn.SetActive(false);
        VRFPS_VRCanvasController.instance.respawnBtn.SetActive(false);

        VRFPS_CanvasController.instance.deathScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.deathScreen.SetActive(true);

        PlayoVR.AvatarSpawnManager.instance.camType1.enabled = false;
        PlayoVR.AvatarSpawnManager.instance.camType2.enabled = false;
        PlayoVR.AvatarSpawnManager.instance.camType3.enabled = false;
    }

    public void CallRespawn()
    {
        StartCoroutine(CallRespawnRoutine());
    }

    private IEnumerator CallRespawnRoutine()
    {
        DisableAllScreens();
        EnableDeathScreen();

        yield return new WaitForSeconds(0.5f);
        PlayoVR.AvatarSpawnManager.instance.oculusVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;
        PlayoVR.AvatarSpawnManager.instance.simulatorVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;
        PlayoVR.AvatarSpawnManager.instance.steamVrVersion.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;

        VRFPS_NetworkController.instance.currentGO.transform.position = PlayoVR.AvatarSpawnManager.instance.killPoint1.position;

        Transform spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1a.transform;

        if (VRFPS_GameController.instance.playerName == "Player 1")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1a.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2a.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 2")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1a.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2a.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 3")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1b.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2b.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 4")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1b.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2b.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 5")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1c.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2c.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 6")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1c.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2c.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 7")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1d.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2d.transform;
            }
        }
        else if (VRFPS_GameController.instance.playerName == "Player 8")
        {
            if (VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerTeam == 1)
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam1d.transform;
            }
            else
            {
                spawn = PlayoVR.AvatarSpawnManager.instance.spawnTeam2d.transform;
            }
        }

        yield return new WaitForSeconds(0.5f);
        PlayoVR.AvatarSpawnManager.instance.oculusVersion.transform.position = spawn.position;
        PlayoVR.AvatarSpawnManager.instance.simulatorVersion.transform.position = spawn.position;
        PlayoVR.AvatarSpawnManager.instance.steamVrVersion.transform.position = spawn.position;

        VRFPS_NetworkController.instance.currentGO.transform.position = spawn.position;

        yield return new WaitForSeconds(0.5f);
        VRFPS_CanvasController.instance.deathScreen.SetActive(false);
        VRFPS_VRCanvasController.instance.deathScreen.SetActive(false);

        PlayoVR.AvatarSpawnManager.instance.camType1.enabled = true;
        PlayoVR.AvatarSpawnManager.instance.camType2.enabled = true;
        PlayoVR.AvatarSpawnManager.instance.camType3.enabled = true;

        VRFPS_GameController.instance.canCountStartTime = true;

        VRFPS_CanvasController.instance.mainBG.SetActive(true);
        VRFPS_VRCanvasController.instance.mainBG.SetActive(true);

        VRFPS_CanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.9f, 1f);
        VRFPS_VRCanvasController.instance.mainBG.GetComponent<Image>().DOFade(0.9f, 1f);

        VRFPS_CanvasController.instance.countScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.countScreen.SetActive(true);

        VRFPS_CanvasController.instance.roundTeam1Points.GetComponent<Text>().text = "" + VRFPS_NetworkController.instance.team1Points;
        VRFPS_VRCanvasController.instance.roundTeam1Points.GetComponent<Text>().text = "" + VRFPS_NetworkController.instance.team1Points;

        VRFPS_CanvasController.instance.roundTeam2Points.GetComponent<Text>().text = "" + VRFPS_NetworkController.instance.team2Points;
        VRFPS_VRCanvasController.instance.roundTeam2Points.GetComponent<Text>().text = "" + VRFPS_NetworkController.instance.team2Points;

        VRFPS_NetworkController.instance.currentGO.GetComponent<NetworkObject>().playerMesh.GetComponent<Animator>().SetTrigger("idle");

        yield return new WaitForSeconds(10.0f);
        VRFPS_CanvasController.instance.mainBG.GetComponent<Image>().DOFade(0f, 2f);
        VRFPS_VRCanvasController.instance.mainBG.GetComponent<Image>().DOFade(0f, 2f);

        VRFPS_CanvasController.instance.gameplayScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.gameplayScreen.SetActive(true);

        VRFPS_CanvasController.instance.countScreen.SetActive(false);
        VRFPS_VRCanvasController.instance.countScreen.SetActive(false);
    }

    public void CheckMatchResult(int myTeam)
    {
        bool team1Win = true;
        bool draw = false;

        if (VRFPS_NetworkController.instance.team1Points > VRFPS_NetworkController.instance.team2Points)
        {
            team1Win = true;
        }
        else if (VRFPS_NetworkController.instance.team1Points < VRFPS_NetworkController.instance.team2Points)
        {
            team1Win = false;
        }
        else
        {
            draw = true;
            team1Win = false;
        }

        if (myTeam == 1)
        {
            if (team1Win)
            {
                CallWinScreen();
            }
            else if (draw)
            {
                CallDrawScreen();
            }
            else
            {
                CallLoseScreen();
            }
        }
        else
        {
            if (!team1Win)
            {
                CallWinScreen();
            }
            else if (draw)
            {
                CallDrawScreen();
            }
            else
            {
                CallLoseScreen();
            }
        }
    }

    public void CallWinScreen()
    {
        DisableAllScreens();
        VRFPS_CanvasController.instance.winScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.winScreen.SetActive(true);
    }

    public void CallLoseScreen()
    {
        DisableAllScreens();
        VRFPS_CanvasController.instance.loseScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.loseScreen.SetActive(true);
    }

    public void CallDrawScreen()
    {
        DisableAllScreens();
        VRFPS_CanvasController.instance.drawScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.drawScreen.SetActive(true);
    }

    public void CallBackToMenu()
    {
        DisableAllScreens();

        VRFPS_CanvasController.instance.initialMenuScreen.SetActive(true);
        VRFPS_VRCanvasController.instance.initialMenuScreen.SetActive(true);
    }

    public void DisableAllScreens()
    {
        VRFPS_CanvasController.instance.initialMenuScreen.SetActive(false);
        VRFPS_CanvasController.instance.gameplayScreen.SetActive(false);
        VRFPS_CanvasController.instance.loadingScreen.SetActive(false);
        VRFPS_CanvasController.instance.deathScreen.SetActive(false);
        VRFPS_CanvasController.instance.loseScreen.SetActive(false);
        VRFPS_CanvasController.instance.winScreen.SetActive(false);
        VRFPS_CanvasController.instance.versionContainer.SetActive(false);
        VRFPS_CanvasController.instance.mainBG.SetActive(false);
        VRFPS_CanvasController.instance.drawScreen.SetActive(false);

        if (VRFPS_VRCanvasController.instance != null)
        {
            VRFPS_VRCanvasController.instance.initialMenuScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.gameplayScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.loadingScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.deathScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.loseScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.winScreen.SetActive(false);
            VRFPS_VRCanvasController.instance.versionContainer.SetActive(false);
            VRFPS_VRCanvasController.instance.mainBG.SetActive(false);
            VRFPS_VRCanvasController.instance.drawScreen.SetActive(false);
        }
    }
}
