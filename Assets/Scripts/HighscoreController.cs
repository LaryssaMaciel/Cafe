using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreController : MonoBehaviour
{
    private int highscore; // maior pontuacao
    private Text txtHs; // texto na tela
    
    void Start() => txtHs = GetComponent<Text>(); // acessa o txt

    void Update()
    {
        highscore = PlayerPrefs.GetInt("highscore"); // atualiza highscore
        txtHs.text = "Highscore\n" + highscore.ToString(); // mostra na tela
    }
}
