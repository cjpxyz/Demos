using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            other.GetComponent<VRFPS_TeamController>().WasHit();
        }
    }
}
