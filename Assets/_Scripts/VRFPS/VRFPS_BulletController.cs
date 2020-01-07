using NetBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            if (other.GetComponent<VRFPS_TeamController>().playerAvatar.name != VRFPS_GameController.instance.playerName)
            {
                VRFPS_NetworkController.instance.GetDemage(other.GetComponent<VRFPS_TeamController>().playerAvatar.GetComponent<NetworkObject>().playerName, VRFPS_GameController.instance.initialHitDemage);
                Debug.Log(other.GetComponent<VRFPS_TeamController>().playerAvatar.GetComponent<NetworkObject>().playerName + " get hit!");

                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
