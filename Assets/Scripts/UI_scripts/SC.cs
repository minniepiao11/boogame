using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SC : MonoBehaviour {

	// Use this for initialization
	public void SceneLoader(int SceneIndex){
		SceneManager.LoadScene (SceneIndex);
	}
}