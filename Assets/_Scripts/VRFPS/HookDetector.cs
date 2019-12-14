using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hookable")
        {
            player.GetComponent<Grappling>().hooked = true;
            player.GetComponent<Grappling>().hookedObj = other.gameObject;
        }
    }
}
