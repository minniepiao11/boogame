using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEnding : MonoBehaviour {
    public VideoClip endingVideo;
    bool isFirstVideo = true;
    float time;
    private void Start()
    {
        GetComponent<VideoPlayer>().Play();
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if(!GetComponent<VideoPlayer>().isPlaying && isFirstVideo && time > 3)
        {
            GetComponent<VideoPlayer>().clip = endingVideo;
            GetComponent<VideoPlayer>().waitForFirstFrame = false;
            GetComponent<VideoPlayer>().Play();
            isFirstVideo = false;
        }
        else if(isFirstVideo == false && time > 5 && !GetComponent<VideoPlayer>().isPlaying)
        {
            SceneManager.LoadScene(0);
        }
	}
}
