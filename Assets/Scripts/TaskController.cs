using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    public static TextMeshPro taskTxt;
    public int minEntregas = 2;
    
    void Start()
    {
        taskTxt = GetComponent<TextMeshPro>();
    }
    
    void Update()
    {
        taskTxt.text = "Faça " + minEntregas.ToString() + " entregas";
    }
}
