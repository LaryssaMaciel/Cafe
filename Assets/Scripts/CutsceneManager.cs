using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public static bool fim = false;

    void Update()
    {
        if ((videoPlayer.frame > 0 && !videoPlayer.isPlaying) || Input.GetKey(KeyCode.Space))
        {
            if (SceneManager.GetActiveScene().name == "Custcene")
            {
                SceneManager.LoadScene("Menu");
            }
            else if (SceneManager.GetActiveScene().name == "Jogo2")
            {
                fim = true;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
