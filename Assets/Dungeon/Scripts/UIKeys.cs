using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public class UIKeys : MonoBehaviour {

	public PostProcessingProfile[] PPProfiles;
	public GameObject FPC;
	public GameObject Teleports;

	private Transform [] tPorts;
	private int portCount = 1;

	// Use this for initialization
	void Start () {

		tPorts = Teleports.GetComponentsInChildren<Transform> ();

		PostProcessingBehaviour ppb = Camera.main.GetComponent<PostProcessingBehaviour>();
		ppb.profile = PPProfiles[0];
		RenderSettings.fogDensity = 0.05f;
		RenderSettings.fogColor = new Color (0.0156f, 0.0470f, 0.055f,1f);
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {

			PostProcessingBehaviour ppb = Camera.main.GetComponent<PostProcessingBehaviour>();
			ppb.profile = PPProfiles[0];
			RenderSettings.fogDensity = 0.05f;
			RenderSettings.fogColor = new Color (0.0156f, 0.0470f, 0.055f,1f);
		}

		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			PostProcessingBehaviour ppb = Camera.main.GetComponent<PostProcessingBehaviour>();
			ppb.profile = PPProfiles[1];
			RenderSettings.fogDensity = 0.07f;
			RenderSettings.fogColor = new Color (0.05f, 0.07f, 0.08f,1f);
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			portCount--;
			if (portCount == 0)
				portCount = tPorts.Length - 1;

			Teleport (portCount);
				
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			portCount++;
			if (portCount == tPorts.Length)
				portCount = 1;

			Teleport (portCount);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Screen.fullScreen = false;
			Cursor.visible = true;
		}


	}


	private void Teleport(int tPortIndex) {

		Transform destination = tPorts [tPortIndex];
		FPC.transform.position = destination.position;
		FPC.transform.rotation = destination.rotation;

	}
}
