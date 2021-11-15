using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Todos os sons")]
    public AudioClip[] som; // sons
    public AudioSource audioSource; // audio

    [Header("Mixers")]
    public AudioMixer MusMixer;
    public AudioMixer GamMixer;

    [Header("Sliders config. audio")]
    public Slider sliMus, sliGam;

    private float musVol = 1, gameVol = 1; // volumes
    private string sceneName; // nome da cena

    void Start()
    {   // configura variaveis
        audioSource = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name.ToString();
        switch (sceneName)
        {   // define musica principal da cena
            case "Menu":
                AudioManager(5); // som de menu
                break;
            case "Jogo":
            case "Jogo1":
                AudioManager(5); // som de gameplay
                break;
        }
        // pega valores salvos das configuracoes de audio
        musVol = PlayerPrefs.GetFloat("musicaVol");
        SetMusVolume(musVol);
        if (sliMus != null) { sliMus.value = musVol; }
        gameVol = PlayerPrefs.GetFloat("gameVol");
        SetGamVolume(gameVol);
        if (sliGam != null) {sliGam.value = gameVol; }
    }

    public void SetMusVolume(float vol) // define e salva configuracoes do volume da musica principal
    {
        MusMixer.SetFloat("volume", vol);
        PlayerPrefs.SetFloat("musicaVol", vol);
    }

    public void SetGamVolume(float vol) // define e salva configuracoes do volume de audio da gameplay
    {
        GamMixer.SetFloat("volume", vol);
        PlayerPrefs.SetFloat("gameVol", vol);
    }

    void AudioManager(int audio)
    {
        audioSource.clip = som[audio];
        audioSource.Play();
    }
}