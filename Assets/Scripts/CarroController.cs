using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroController : MonoBehaviour
{
    private float speed = 10f; // velocidade do carro 
    private Transform player;

    private void Start() => player = GameObject.FindWithTag("Player").GetComponent<Transform>(); // acessa o player

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed); // move automatico

        if (this.transform.position.x < player.transform.position.x - 10f) // se passou do player, se destroi
        {
            Destroy(this.gameObject);
        }
    }
}
