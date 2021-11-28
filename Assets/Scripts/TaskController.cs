using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public static TextMeshPro taskTxt; // texto da task no cenario
    public int minEntregas = 7; // entregas minimas pra passar de fase
    
    void Start() => taskTxt = GetComponent<TextMeshPro>(); // acessa o texto
    
    void Update() => taskTxt.text = "Faça " + minEntregas.ToString() + " entregas"; // atualiza a task
}
