using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryEverEnableMesh : MonoBehaviour
{
    void Update()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }
}
