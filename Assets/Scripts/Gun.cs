namespace PlayoVR
{
    using NetBase;
    using UnityEngine;
    using UnityEngine.UI;
    using VRTK;

    public class Gun : Photon.MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawn;
        public AudioClip fireGunSound;
        public Animation fireAnimation;

        private bool fired;

        // Use this for initialization
        void Awake()
        {
            GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(DoFireGun);
        }

        void DoFireGun(object sender, InteractableObjectEventArgs e)
        {
            fired = true;
        }

        // Update is called once per frame
        void Update()
        {
            // Handle firing
            if(VRFPS_GameController.instance != null)
            {
                if (fired && VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets > 0)
                {
                    CmdFire();
                    fired = false;
                }
            }
            else
            {
                if (fired)
                {
                    CmdFire();
                    fired = false;
                }
            }
            
        }

        void CmdFire()
        {
            if (VRFPS_GameController.instance != null)
            {
                // Now create the bullet and play sound/animation locally and on all other clients
                VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets--;
                VRFPS_CanvasController.instance.ammoCount.GetComponent<Text>().text = "00" + VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerBullets;
                Debug.Log("Remove bullets from: " + VRFPS_GameController.instance.myAvatarObject.GetComponent<NetworkObject>().playerName);
            }

            photonView.RPC("NetFire", PhotonTargets.All, bulletSpawn.position, bulletSpawn.rotation);
        }

        [PunRPC]
        void NetFire(Vector3 position, Quaternion rotation)
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = Instantiate(
                bulletPrefab,
                position,
                rotation);
            // Play sound of gun shooting
            AudioSource.PlayClipAtPoint(fireGunSound, transform.position, 1.0f);
            // Play animation of gun shooting
            fireAnimation.Play();
        }
    }
}
