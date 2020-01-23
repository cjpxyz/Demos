using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRFPS_SceneManager : Photon.PunBehaviour
{
    public static VRFPS_SceneManager instance;

    public string sceneToLoadMap1;
    public string sceneToLoadMap2;
    public float fixedTime = 5;
    public enum LoadType {progress, fixedTime};
    public LoadType loadType;
    public Image loadingBar;
    public Text progressText;
    private int progress = 0;
    private string defaultText;

    private float tempval;
    private AsyncOperation async;
    private bool canLoading;

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

    void Update()
    {
        if (canLoading)
        {
            try
            {
                //Debug.Log(async.progress);
                tempval += async.progress;
                loadingBar.fillAmount = tempval;
                progressText.text = (int)(loadingBar.fillAmount * 100) + "%";
                //Debug.Log("Progress: " + (loadingBar.fillAmount * 100));
            }
            catch (NullReferenceException ex)
            {
                //Debug.Log("Async not loaded!");
            }
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("!! OnCreatedRoom");
    }

    public override void OnConnectedToMaster()
    {
        AfterPhotonInLobby();
        Debug.Log("OnConnectedToMaster");
    }

    public override void OnJoinedLobby()
    {
        AfterPhotonInLobby();
        Debug.Log("Entrou no Lobby do Photon!");
    }

    public void AfterPhotonInLobby()
    {
        Debug.Log("Entrou no lobby tentar criar sala!");
        CallChooseGameMode();
    }

    public void CallChooseGameMode()
    {
        VRFPS_CanvasActions.instance.CallChooseGameScreen();
        Debug.Log("!! CallChooseGameMode");
    }

    public void CallChooseName()
    {
        VRFPS_CanvasActions.instance.CallChangeNameScreen();
    }

    /*public void CallChooseTeam()
    {
        VRFPS_CanvasActions.instance.CallChangeTeamScreen();
    }*/

    public void CallChooseJoinBtn()
    {
        VRFPS_CanvasActions.instance.CallChangeJoinScreen();
    }

    public void ChooseGameMode(int mode)
    {
        VRFPS_GameController.instance.currentGameMode = mode;
        CallChooseName();
    }

    public void ChooseName(GameObject inputName)
    {
        VRFPS_GameController.instance.currentPlayerRealName = inputName.GetComponent<InputField>().text;

        if (inputName.GetComponent<InputField>().text != "")
        {
            CallChooseJoinBtn();
        }
    }

    public void ChooseTeam(int team)
    {
        VRFPS_GameController.instance.currentPlayerTeam = team;
        CallChooseJoinBtn();
    }

    public void CallTryJoinRoom()
    {
        Debug.Log("CallTryJoinRoom: " + VRFPS_GameController.instance.currentGameMode);

        VRFPS_CanvasController.instance.roomConfigsContainer.SetActive(false);
        //VRFPS_VRCanvasController.instance.roomConfigsContainer.SetActive(false);

        RealPlayGame();
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame!");

        ConnectInPhoton();
    }

    public void ConnectInPhoton()
    {
        VRFPS_PhotonNetworkController.instance.ConnectToPhoton();
    }

    public void RealPlayGame()
    {
        Debug.Log("PlayGame!");

        VRFPS_CanvasController.instance.mainBG.SetActive(true);
        VRFPS_GameController.instance.isConnectedInPhoton = false;

        if (loadType == LoadType.progress)
        {
            StartCoroutine(LoadingScene(sceneToLoadMap1));
        }
        else
        {
            StartCoroutine(Fixed("VRFPS_MapDungeon"));
        }
    }

    public void BackToMenu()
    {
        SceneManager.UnloadSceneAsync("VRFPS_MapDungeon");
        VRFPS_CanvasActions.instance.CallBackToMenu();
    }

    IEnumerator LoadingScene(string scene)
    {
        VRFPS_CanvasController.instance.initialMenuScreen.SetActive(false);
        VRFPS_CanvasController.instance.loadingScreen.SetActive(true);
        canLoading = true;
        tempval = 0;

        //Debug.Log(tempval);

        yield return new WaitForSeconds(0.1f);
        async = SceneManager.LoadSceneAsync("VRFPS_MapDungeon", LoadSceneMode.Additive);//add the next scene name that to be loaded
        yield return async;

        VRFPS_PhotonFindRoomController.instance.TryJoinRoom(VRFPS_GameController.instance.currentGameMode, 1);
        //VRFPS_PhotonNetworkController.instance.ConnectToPhoton();

        yield return new WaitUntil(() => VRFPS_GameController.instance.isConnectedInPhoton);
        VRFPS_CanvasController.instance.loadingScreen.SetActive(false);
        canLoading = false;
        VRFPS_GameController.instance.StartGame();
    }

    IEnumerator Fixed(string scene)
    {
        yield return new WaitForSeconds(fixedTime);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }
}
