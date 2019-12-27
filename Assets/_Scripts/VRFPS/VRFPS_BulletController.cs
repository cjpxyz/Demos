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
            //other.GetComponent<VRFPS_TeamController>().WasHit(other.GetComponent<VRFPS_TeamController>().playerName);
            other.GetComponent<VRFPS_TeamController>().playerAvatar.GetComponent<NetworkObject>().GetDemage(VRFPS_GameController.instance.initialHitDemage);
            Debug.Log(other.GetComponent<VRFPS_TeamController>().playerAvatar.GetComponent<NetworkObject>().playerName + " get hit!");
        }
    }
}
