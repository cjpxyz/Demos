using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRFPS_TeamController : MonoBehaviour
{
    public static VRFPS_TeamController instance;

    public int myTeam;

    public void WasHit()
    {
        VRFPS_CanvasController.instance.AddPointToTeam(myTeam);
        Destroy(gameObject);
    }
}
