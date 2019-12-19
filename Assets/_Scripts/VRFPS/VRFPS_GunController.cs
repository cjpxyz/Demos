using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_GunController : MonoBehaviour
{
    public static VRFPS_GunController instance;

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

    public void RemoveBullet()
    {
        VRFPS_GameController.instance.initialAmmoCount--;
    }
}
