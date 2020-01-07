using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_AmmoSpawner : MonoBehaviour
{
    public void CallNewAmmo()
    {
        Invoke("SpwanNewAmmo", 10f);
    }

    public void SpwanNewAmmo()
    {
        PlayoVR.AvatarSpawnManager.instance.SpawnAmmo(transform, 1);
    }
}
