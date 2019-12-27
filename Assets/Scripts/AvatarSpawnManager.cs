namespace PlayoVR
{
    using NetBase;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VRTK;
    using Hashtable = ExitGames.Client.Photon.Hashtable;

    public class AvatarSpawnManager : Photon.PunBehaviour
    {
        [Tooltip("Reference to the player avatar prefab")]
        public GameObject playerAvatar;

        private GameObject[] spawnPoints;
        private bool sceneLoaded = false;
        private bool connected = false;
        private GameObject currentPlayer;

        void Awake()
        {
            if (playerAvatar == null)
            {
                Debug.LogError("AvatarSpawnManager is missing a reference to the player avatar prefab!");
            }
            spawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
            if (spawnPoints.Length == 0)
            {
                Debug.LogError("No spawn points were found!");
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene loaded");
            sceneLoaded = true;
        }

        public override void OnJoinedRoom()
        {
            connected = true;
            // Player sets its own name when joining
            PhotonNetwork.playerName = playerName(PhotonNetwork.player);
            // Initialize the master client
            InitPlayer(PhotonNetwork.player);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            InitPlayer(newPlayer);
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
        }

        void InitPlayer(PhotonPlayer newPlayer)
        {
            Debug.Log("!! Instantiate Player 0");
            if (PhotonNetwork.isMasterClient && connected && sceneLoaded)
            {
                // The master client tells everyone about the new player
                Hashtable props = new Hashtable();
                props[PlayerPropNames.PLAYER_NR] = playerNr(newPlayer);
                newPlayer.SetCustomProperties(props);
                photonView.RPC("SpawnAvatar", newPlayer);

                Debug.Log("!! Instantiate Player 1");
            }
        }

        [PunRPC]
        void SpawnAvatar()
        {
            if (!PhotonNetwork.player.CustomProperties.ContainsKey(PlayerPropNames.PLAYER_NR))
            {
                Debug.LogError("Player does not have a PLAYER_NR property!");
                return;
            }
            int nr = (int)PhotonNetwork.player.CustomProperties[PlayerPropNames.PLAYER_NR];
            // Create a new player at the appropriate spawn spot
            var trans = spawnPoints[nr].transform;
            var name = PhotonNetwork.playerName;
            var player = PhotonNetwork.Instantiate(playerAvatar.name, trans.position, trans.rotation, 0, new object[] { name });

            currentPlayer = player.gameObject;

            if (VRFPS_NetworkController.instance.playersInRoom % 2 == 0)
            {
                player.gameObject.GetComponent<NetworkObject>().playerTeam = 1;
            }
            else
            {
                player.gameObject.GetComponent<NetworkObject>().playerTeam = 2;
            }

            photonView.RPC("CallToEveryone", PhotonTargets.AllBuffered);
        }

        [PunRPC]
        void CallToEveryone()
        {
            //Debug.Log("CallToEveryone");
            VRFPS_NetworkController.instance.playersInRoom++;

            currentPlayer.GetComponent<NetworkObject>().playerName = playerName(PhotonNetwork.player);

            if (VRFPS_NetworkController.instance.playersInRoom == 1)
            {
                VRFPS_NetworkController.instance.player1 = currentPlayer;
            }
            else if(VRFPS_NetworkController.instance.playersInRoom == 2)
            {
                VRFPS_NetworkController.instance.player2 = currentPlayer;
                VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
            }
        }

        private string playerName(PhotonPlayer ply)
        {
            return "Player " + ply.ID;
        }

        private int playerNr(PhotonPlayer ply)
        {
            // TODO: do something a bit more clever here
            // We want players to actually show up in an empty spot
            return PhotonNetwork.otherPlayers.Length;
        }
    }
}

