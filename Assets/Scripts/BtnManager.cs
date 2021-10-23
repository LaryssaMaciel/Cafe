using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // ### Funções pro menu

    public GameObject goMenu, goFases;
    public void Start_Game()
    {
        goMenu.SetActive(false);
        goFases.SetActive(true);
    }
    public void Back_Menu()
    {
        goMenu.SetActive(true);
        goFases.SetActive(false);
    }

    public void Fase1()
    {
        SceneManager.LoadScene("Jogo");
    }
    
    public void Fase2()
    {
        SceneManager.LoadScene("Jogo1");
    }

    private bool cred = false;
    public GameObject goCredits;
    public void Credits()
    {
        cred = !cred;
        if (cred)
        {
            goCredits.SetActive(true);
            goMenu.SetActive(false);
        }
        else
        {
            goCredits.SetActive(false);
            goMenu.SetActive(true);
        }
    }

    public void Sair()
    {
        Application.Quit();
    }
    
    // ### Funções pra gameplay

    public void Menu()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1; // resume
    }

    public void Restart()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // resume
    }

    private int lvl = 0;
    public void NextLvl()
    {
        lvl++;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Jogo" + lvl.ToString());
        Time.timeScale = 1; // resume
    }
}
