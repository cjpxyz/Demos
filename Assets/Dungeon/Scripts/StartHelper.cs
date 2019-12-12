using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHelper : MonoBehaviour {

	public GameObject [] disableOnRun;
	public GameObject [] enableOnRun;



	// Use this for initialization
	void Start () {
		for (int i=0; i < disableOnRun.Length; i++) {
			if(disableOnRun[i] != null)
				disableOnRun [i].SetActive(false);
		}
		for (int i=0; i < enableOnRun.Length; i++) {
			if(enableOnRun[i] != null)
				enableOnRun [i].SetActive(true);
		}
		RenderSettings.ambientIntensity = 0.3f;
		RenderSettings.fog = true;
		
	}
	
	void OnApplicationPause() {
		for (int i=0; i < disableOnRun.Length; i++) {
			if(disableOnRun[i] != null)
				disableOnRun [i].SetActive(true);
		}
		for (int i=0; i < enableOnRun.Length; i++) {
			if(enableOnRun[i] != null)
				enableOnRun [i].SetActive(false);
		}
		RenderSettings.ambientIntensity = 1f;
		RenderSettings.fog = false;
	}
}
