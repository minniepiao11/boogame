using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Text;

public class PhotonPlayerSetting : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(!photonView.isMine)
        {
            this.GetComponent<ThirdPersonUserControl>().enabled = false;
            //this.GetComponent<ThirdPersonCharacter>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
