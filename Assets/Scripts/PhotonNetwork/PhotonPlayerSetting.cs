using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Text;

public class PhotonPlayerSetting : Photon.MonoBehaviour {
    GameObject originalCam;
	// Use this for initialization
	void Start () {
        if(!photonView.isMine)
        {
            this.GetComponent<ThirdPersonUserControl>().enabled = false;
            originalCam = GameObject.Find("Main Camera");
            Destroy(originalCam);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
