using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PontuacaoManager : MonoBehaviour
{
    public static TextMeshProUGUI pontosTxt; // texto da task
    public int pontos = 0; // entregas minimas pra passar de fase

    private float delayTime = .5f, delayCounter = 0;

    private EndLVLController endLVL;
    private bool endTask = true; // auxiliar pra soma extra de ponto
    
    void Start()
    {
        pontosTxt = GetComponent<TextMeshProUGUI>(); // acessa o texto
        endLVL = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
    }
    
    void Update()
    {
        // ponto extra por completar task
        if (endLVL.taskCompleted && endTask)
        {
            pontos += 100;
            endTask = false;
        }

        // os pontos por entregas estao no script InteractController, ao realizar entrega

        pontosTxt.text = pontos.ToString(); // atualiza texto dos pontos
        
        // pontos continuos conforme joga
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
        }
        else
        {
            delayCounter = delayTime;
            pontos++;
        }
    }
}
