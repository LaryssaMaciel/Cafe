using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PontuacaoManager : MonoBehaviour
{
    public static TextMeshProUGUI pontosTxt; // texto dos pontos
    public int pontos = 0, multiplicador = 1, highscore = 0; // pontos, multiplicador de pontos, highscore
    private float delayTime = .5f, delayCounter = 0; // delay de pontos continuos, counter do delay
    private bool endTask = true; // se fez task
    // auxiliares
    public PlayerController pc;
    private EndLVLController endLVL;
    
    void Start()
    {   // configura variaveis
        pontosTxt = GetComponent<TextMeshProUGUI>(); 
        endLVL = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        highscore = PlayerPrefs.GetInt("highscore");
    }
    
    void Update()
    {
        highscore = PlayerPrefs.GetInt("highscore");
        SaveHighscore(); // salva highscore
        pontosTxt.text = pontos.ToString(); // atualiza texto dos pontos
        // os pontos por entregas estao no script InteractController, ao realizar entrega
        // os pontos por manobra estao no playercontroller
        
        if (endLVL.taskCompleted && endTask) // se completou task
        {   // ponto extra por completar task
            pontos += 100 * multiplicador;
            endTask = false;
        }
        
        // pontos continuos conforme joga
        if (delayCounter > 0) { delayCounter -= Time.deltaTime; } // diminui counter do delay
        else {
            delayCounter = delayTime; // reseta counter
            pontos +=1 * multiplicador; // ponto por segundo
        }
    }

    void SaveHighscore()
    {
        if (pontos > highscore) { highscore = pontos; } // atualiza highscore
        PlayerPrefs.SetInt("highscore", highscore); // salva o highscore
    }
}
