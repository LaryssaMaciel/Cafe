using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryController : MonoBehaviour
{
    private TextMeshProUGUI txtContPego; // quantos pedidos pegou
    private TextMeshProUGUI txtContEntregue; // quantos pedidos entregou
    public int contPego = 0; // contador de pedidos pegos
    public int contEntregue = 0; // contador de pedidos entregue
    
    void Start()
    {
        // acessa os textos
        txtContPego = GameObject.FindWithTag("cont").GetComponent<TextMeshProUGUI>();
        txtContEntregue = GameObject.FindWithTag("contE").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // atualiza as quantidades e mostra na tela
        txtContPego.text = contPego.ToString();
        txtContEntregue.text = contEntregue.ToString();
    }
}
