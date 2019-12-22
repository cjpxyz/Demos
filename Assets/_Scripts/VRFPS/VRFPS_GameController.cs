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
    public int initialTotalRounds;

    public bool canStartMacth;
    public bool macthEnd = false;

    public float matchTime;
    public bool isConnectedInPhoton;

    public GameObject playerVRPrefab;
    public GameObject vRScriptsPrefab;

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
        if (canStartMacth && !macthEnd)
        {
            matchTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(matchTime / 60F);
            int seconds = Mathf.FloorToInt(matchTime - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

            VRFPS_CanvasController.instance.roundTimeCount.GetComponent<Text>().text = niceTime;
            VRFPS_CanvasController.instance.ammoCount.GetComponent<Text>().text = "00" + initialAmmoCount;
        }
    }

    public void StartGame()
    {
        VRFPS_CanvasController.instance.SetInitialGameInfos();
        canStartMacth = true;

        StartCoroutine(StartSequence());
    }

    private IEnumerator StartSequence()
    {
        PhotonNetwork.Instantiate(playerVRPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);

        yield return new WaitForSeconds (0.5f);
        GameObject vrScripts = Instantiate(vRScriptsPrefab);

        yield return new WaitUntil(() => isConnectedInPhoton);
        VRFPS_CanvasController.instance.mainBG.SetActive(false);
    } 
}
