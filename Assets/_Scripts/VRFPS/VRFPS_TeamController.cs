using NetBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_TeamController : MonoBehaviour
{
    public static VRFPS_TeamController instance;

    public GameObject playerAvatar;
    public int myTeam;
    public string playerName;

    public void WasHit(string name)
    {
        if (VRFPS_CanvasActions.instance != null)
        {
            VRFPS_CanvasActions.instance.GetHit(myTeam, name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoCollectable")
        {
            //other.transform.parent.GetComponent<VRFPS_AmmoSpawner>().CallNewAmmo();
            PhotonNetwork.Destroy(other.gameObject);
            VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().AddBullets(10);
            VRFPS_CanvasActions.instance.UpdateBulletsNumber();
        }
    }
}