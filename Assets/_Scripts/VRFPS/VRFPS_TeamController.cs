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
            //VRFPS_CanvasController.instance.AddPointToTeam(myTeam);
        }

        //Destroy(gameObject);
    }
}
