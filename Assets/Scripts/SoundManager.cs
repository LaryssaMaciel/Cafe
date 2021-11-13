using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Todos os sons")]
    public AudioClip[] som;
    public AudioSource audioSource;
    public AudioMixer MusMixer;
    public AudioMixer GamMixer;
    public Slider sliMus, sliGam;
    private float musVol = 1, gameVol = 1;
    
    private string sceneName;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name.ToString();
        switch (sceneName)
        {
            case "Menu":
                audioSource.clip = som[5];
                audioSource.Play();
                break;
            case "Jogo":
            case "Jogo1":
                audioSource.clip = som[5];
                audioSource.Play();
                break;
        }
        musVol = PlayerPrefs.GetFloat("musicaVol");
        SetMusVolume(musVol);
        sliMus.value = musVol;
        gameVol = PlayerPrefs.GetFloat("gameVol");
        SetGamVolume(gameVol);
        sliGam.value = gameVol;
    }

    public void SetMusVolume(float vol)
    {
        MusMixer.SetFloat("volume", vol);
        PlayerPrefs.SetFloat("musicaVol", vol);
    }

    public void SetGamVolume(float vol)
    {
        GamMixer.SetFloat("volume", vol);
        PlayerPrefs.SetFloat("gameVol", vol);
    }
}