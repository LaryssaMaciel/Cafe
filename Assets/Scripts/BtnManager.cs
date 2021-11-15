using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // GERENCIADOR DOS BOTÔES

    public AudioSource audioSource; // audio dos botoes
    public void Som() => audioSource.Play(); // toca som do botao

    // ### Funções pro MENU

    public GameObject goMenu, goFases; // botao 'menu' e fases
    public void Start_Game() // ao clicar em start game, mostra as fases disponiveis e esconde botoes do menu inicial
    {
        goMenu.SetActive(false);
        goFases.SetActive(true);
        goSettings.SetActive(false);
        goCredits.SetActive(false);
        goTutorial.SetActive(false);
    }
    public void Back_Menu() // ao clicar em voltar, esconde as fases e mostra botoes do menu inicial
    {
        goMenu.SetActive(true);
        goFases.SetActive(false);
        goSettings.SetActive(false);
        goCredits.SetActive(false);
        goTutorial.SetActive(false);
    }

    public GameObject goSettings; // configurações
    public void Show_Settings() // mostra as configurações
    {
        goSettings.SetActive(true);
        goMenu.SetActive(false);
        goFases.SetActive(false);
        goCredits.SetActive(false);
        goTutorial.SetActive(false);
    }

    // Fases
    public void Fase1() => SceneManager.LoadScene("Jogo"); // vai pra fase 1
    public void Fase2() => SceneManager.LoadScene("Jogo1"); // vai pra fase 2

    public GameObject goCredits; // botao de creditos
    public void Credits() // se clicar no botao creditos mostra creditos
    {
        goCredits.SetActive(true);
        goMenu.SetActive(false);
        goSettings.SetActive(false);
        goFases.SetActive(false);
        goTutorial.SetActive(false);
    }

    public GameObject goTutorial; // botao de tutorial
    public void ShowTutorial() // mostra tutorial
    {
        goCredits.SetActive(false);
        goMenu.SetActive(false);
        goSettings.SetActive(false);
        goFases.SetActive(false);
        goTutorial.SetActive(true);
    }

    public void Sair() => Application.Quit(); // sai do jogo
    
    // ### Funções pra gameplay

    public GameObject goBTNs; // botoes no pause
    public void Back_Pause() // esconde configuracoes e mostra opcoes
    {
        goBTNs.SetActive(true);
        goSettings.SetActive(false);
    }

    public void Menu() // ao clicar no btn menu no jogo, vai pro menu principal
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1; // resume
    }

    public void Restart() // ao clicar no btn restart no jogo, recomeça fase
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1; // resume
    }

    public void SettingsPause_() // mostra configuracoes e esconde opcoes
    {
        goBTNs.SetActive(false);
        goSettings.SetActive(true); 
    }

    public int lvl = 0; // numero da fase (0 = fase 1 || 1 = fase 2)
    public void NextLvl() // ao clicar no btn next level no jogo, vai pra proxima fase
    {
        lvl++;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Jogo" + lvl.ToString());
        Time.timeScale = 1; // resume
    }
}
