using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
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
        SceneManager.LoadScene("Laryssa" + lvl.ToString());
        Time.timeScale = 1; // resume
    }
}
