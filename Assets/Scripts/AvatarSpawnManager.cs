namespace PlayoVR
{
    using NetBase;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VRTK;
    using Hashtable = ExitGames.Client.Photon.Hashtable;

    public class AvatarSpawnManager : Photon.PunBehaviour
    {
        [Tooltip("Reference to the player avatar prefab")]
        public GameObject playerAvatar;
        public GameObject ammoPrefab;

        public Camera camType1;
        public Camera camType2;
        public Camera camType3;

        public LayerMask maskToPlayer1;
        public LayerMask maskToPlayer2;
        public LayerMask maskToPlayer3;
        public LayerMask maskToPlayer4;
        public LayerMask maskToPlayer5;
        public LayerMask maskToPlayer6;
        public LayerMask maskToPlayer7;
        public LayerMask maskToPlayer8;

        private GameObject[] spawnPoints;
        private bool sceneLoaded = false;
        private bool connected = false;
        private GameObject currentPlayer;

        private GameObject[] ammoSpawnList;

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

        void Start()
        {
            ammoSpawnList = GameObject.FindGameObjectsWithTag("SpawnBullets");
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
            //Debug.Log("!! OnJoinedRoom");
            connected = true;
            // Player sets its own name when joining
            PhotonNetwork.playerName = playerName(PhotonNetwork.player);
            // Initialize the master client
            InitPlayer(PhotonNetwork.player);
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            //Debug.Log("!! OnPhotonPlayerConnected");
            InitPlayer(newPlayer);
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
        }

        void InitPlayer(PhotonPlayer newPlayer)
        {
            //Debug.Log("!! InitPlayer 0");
            if (PhotonNetwork.isMasterClient && connected && sceneLoaded)
            {
                // The master client tells everyone about the new player
                Hashtable props = new Hashtable();
                props[PlayerPropNames.PLAYER_NR] = playerNr(newPlayer);
                newPlayer.SetCustomProperties(props);
                photonView.RPC("SpawnAvatar", newPlayer);

                //Debug.Log("!! InitPlayer 1");
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

            if (VRFPS_GameController.instance != null)
            {
                VRFPS_GameController.instance.myAvatarObject = currentPlayer;
                VRFPS_GameController.instance.inGameScene = true;
                VRFPS_GameController.instance.playerName = name;

                if (VRFPS_NetworkController.instance.playersInRoom % 2 == 0)
                {
                    player.gameObject.GetComponent<NetworkObject>().playerTeam = 1;
                }
                else
                {
                    player.gameObject.GetComponent<NetworkObject>().playerTeam = 2;
                }

                if (VRFPS_GameController.instance.playerName == "Player 1")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player1Body");

                    camType1.cullingMask = maskToPlayer1;
                    camType2.cullingMask = maskToPlayer1;
                    camType3.cullingMask = maskToPlayer1;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 2")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player2Body");

                    camType1.cullingMask = maskToPlayer2;
                    camType2.cullingMask = maskToPlayer2;
                    camType3.cullingMask = maskToPlayer2;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 3")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player3Body");

                    camType1.cullingMask = maskToPlayer3;
                    camType2.cullingMask = maskToPlayer3;
                    camType3.cullingMask = maskToPlayer3;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 4")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player4Body");

                    camType1.cullingMask = maskToPlayer4;
                    camType2.cullingMask = maskToPlayer4;
                    camType3.cullingMask = maskToPlayer4;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 5")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player5Body");

                    camType1.cullingMask = maskToPlayer5;
                    camType2.cullingMask = maskToPlayer5;
                    camType3.cullingMask = maskToPlayer5;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 6")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player6Body");

                    camType1.cullingMask = maskToPlayer6;
                    camType2.cullingMask = maskToPlayer6;
                    camType3.cullingMask = maskToPlayer6;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 7")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player7Body");

                    camType1.cullingMask = maskToPlayer7;
                    camType2.cullingMask = maskToPlayer7;
                    camType3.cullingMask = maskToPlayer7;
                }
                else if (VRFPS_GameController.instance.playerName == "Player 8")
                {
                    ChangeLayers(currentPlayer.GetComponent<NetworkObject>().playerMesh, "Player8Body");

                    camType1.cullingMask = maskToPlayer8;
                    camType2.cullingMask = maskToPlayer8;
                    camType3.cullingMask = maskToPlayer8;
                }
            }

            for (int i = 0; i < ammoSpawnList.Length; i++)
            {
                Debug.Log("Instantiate ammo: " + i);
                var ammo = PhotonNetwork.Instantiate(ammoPrefab.name, new Vector3(ammoSpawnList[i].transform.position.x, ammoSpawnList[i].transform.position.y + 1, ammoSpawnList[i].transform.position.z), ammoSpawnList[i].transform.rotation, 0);
                ammo.gameObject.transform.parent = ammoSpawnList[i].transform;
            }

            photonView.RPC("CallToEveryone", PhotonTargets.AllBuffered);
        }

        private void ChangeLayers(GameObject parent, string layer)
        {
            parent.layer = LayerMask.NameToLayer(layer);

            foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
            {
                child.gameObject.layer = LayerMask.NameToLayer(layer);  // add any layer you want. 
            }
        }

        [PunRPC]
        void CallToEveryone()
        {
            //Debug.Log("!!!! CallToEveryone");
            if (VRFPS_GameController.instance != null)
            {
                VRFPS_NetworkController.instance.playersInRoom++;
            }

            currentPlayer.GetComponent<NetworkObject>().playerName = playerName(PhotonNetwork.player);

            if (VRFPS_GameController.instance != null)
            {
                VRFPS_NetworkController.instance.currentGO = currentPlayer;

                if (VRFPS_NetworkController.instance.playersInRoom == 1)
                {
                    VRFPS_NetworkController.instance.player1 = currentPlayer;
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 2)
                {
                    VRFPS_NetworkController.instance.player2 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 3)
                {
                    VRFPS_NetworkController.instance.player3 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 4)
                {
                    VRFPS_NetworkController.instance.player4 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                    VRFPS_NetworkController.instance.player3 = GameObject.Find("Player 3");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 5)
                {
                    VRFPS_NetworkController.instance.player5 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                    VRFPS_NetworkController.instance.player3 = GameObject.Find("Player 3");
                    VRFPS_NetworkController.instance.player4 = GameObject.Find("Player 4");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 6)
                {
                    VRFPS_NetworkController.instance.player6 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                    VRFPS_NetworkController.instance.player3 = GameObject.Find("Player 3");
                    VRFPS_NetworkController.instance.player4 = GameObject.Find("Player 4");
                    VRFPS_NetworkController.instance.player5 = GameObject.Find("Player 5");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 7)
                {
                    VRFPS_NetworkController.instance.player7 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                    VRFPS_NetworkController.instance.player3 = GameObject.Find("Player 3");
                    VRFPS_NetworkController.instance.player4 = GameObject.Find("Player 4");
                    VRFPS_NetworkController.instance.player5 = GameObject.Find("Player 5");
                    VRFPS_NetworkController.instance.player6 = GameObject.Find("Player 6");
                }
                else if (VRFPS_NetworkController.instance.playersInRoom == 8)
                {
                    VRFPS_NetworkController.instance.player8 = currentPlayer;
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player1 = GameObject.Find("Player 1");
                    VRFPS_NetworkController.instance.player2 = GameObject.Find("Player 2");
                    VRFPS_NetworkController.instance.player3 = GameObject.Find("Player 3");
                    VRFPS_NetworkController.instance.player4 = GameObject.Find("Player 4");
                    VRFPS_NetworkController.instance.player5 = GameObject.Find("Player 5");
                    VRFPS_NetworkController.instance.player6 = GameObject.Find("Player 6");
                    VRFPS_NetworkController.instance.player7 = GameObject.Find("Player 7");
                }
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

