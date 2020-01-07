using UnityEngine;

public class SyncGameObject : MonoBehaviour
{
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(gameObject.active);
        }
        else
        {
            gameObject.active = (GameObject)stream.ReceiveNext();
        }
    }
}
