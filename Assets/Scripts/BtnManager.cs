using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    // ### Funções pro menu

    public GameObject goMenu, goFases; // botao 'menu' e botoes das fases disponiveis
    public void Start_Game() // ao clicar em start game, mostra as fases disponiveis e esconde botoes do menu inicial
    {
        goMenu.SetActive(false);
        goFases.SetActive(true);
    }
    public void Back_Menu() // ao clicar em voltar, esconde as fases e mostra botoes do menu inicial
    {
        goMenu.SetActive(true);
        goFases.SetActive(false);
    }

    public void Fase1() // vai pra fase 1
    {
        SceneManager.LoadScene("Jogo");
    }
    
    public void Fase2() // vai pra fase 2
    {
        SceneManager.LoadScene("Jogo1");
    }

    private bool cred = false; // variavel auxiliar
    public GameObject goCredits; // botao de creditos
    public void Credits() // se clicar no botao creditos mostra/esconde creditos
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

    public void Sair() // sai do jogo
    {
        Application.Quit();
    }
    
    // ### Funções pra gameplay

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

    public int lvl = 0; // numero da fase (0 = fase 1)
    public void NextLvl() // ao clicar no btn next level no jogo, vai pra proxima fase
    {
        lvl++;
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Jogo" + lvl.ToString());
        Time.timeScale = 1; // resume
    }
}
