using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryController : MonoBehaviour
{
    private TextMeshProUGUI txtContPego;
    private TextMeshProUGUI txtContEntregue;
    public int contPego = 0; // contador de pedidos pegos
    public int contEntregue = 0; // contador de pedidos entregue
    
    void Start()
    {
        txtContPego = GameObject.FindWithTag("cont").GetComponent<TextMeshProUGUI>();
        txtContEntregue = GameObject.FindWithTag("contE").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        txtContPego.text = contPego.ToString();
        txtContEntregue.text = contEntregue.ToString();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // if (other.gameObject.GetComponent<Restaurante>().used == 1) 
        // {
        //     contPego++; // coleta lanche
        //     other.gameObject.GetComponent<Restaurante>().used = 2;
        // }

        // if (other.gameObject.tag == "cliente" && contPego > 0 && Input.GetButtonDown("Acao")) // se tem lanche sobrando
        // {
        //     contEntregue++; // entrega lanche
        //     contPego--;
        // }
    }
}
