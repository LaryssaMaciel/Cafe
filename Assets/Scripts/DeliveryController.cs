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
    [SerializeField] private int contPego = 0; // contador de pedidos pegos
    [SerializeField] private int contEntregue = 0; // contador de pedidos entregue
    
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "rest")
        {
            contPego++; // coleta lanche
        }

        if (other.gameObject.tag == "cliente" && contPego > 0) // se tem lanche sobrando
        {
            contEntregue++; // entrega lanche
            contPego--;
        }
    }
}
