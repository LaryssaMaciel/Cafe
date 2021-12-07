using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Update()
    {
        if ((videoPlayer.frame > 0 && !videoPlayer.isPlaying) || Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
