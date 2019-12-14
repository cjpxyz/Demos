using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VRFPS_SceneManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject camCanvas;

    public GameObject initialContainer;
    public GameObject loadingContainer;

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
                Debug.Log("Async not loaded!");
            }
        }
    }

    public void PlayGame()
    {
        Debug.Log("PlayGame!");

        if(loadType == LoadType.progress)
        {
            StartCoroutine(LoadingScene(sceneToLoadMap1));
        }
        else
        {
            StartCoroutine(Fixed("VRFPS_MapDungeon"));
        }
    }

    IEnumerator LoadingScene(string scene)
    {
        initialContainer.SetActive(false);
        loadingContainer.SetActive(true);
        canLoading = true;
        tempval = 0;

        Debug.Log(tempval);

        yield return new WaitForSeconds(0.1f);
        async = SceneManager.LoadSceneAsync("VRFPS_MapDungeon", LoadSceneMode.Additive);//add the next scene name that to be loaded
        yield return async;

        loadingContainer.SetActive(false);
        canLoading = false;
    }

    IEnumerator Fixed(string scene)
    {
        yield return new WaitForSeconds(fixedTime);
        SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
    }
}
