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
        if (VRFPS_CanvasController.instance != null)
        {
            VRFPS_CanvasController.instance.GetHit(myTeam, name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AmmoCollectable")
        {
            PhotonNetwork.Destroy(other.gameObject);
            VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().AddBullets(10);
            VRFPS_CanvasController.instance.UpdateBulletsNumber();
        }
    }
}