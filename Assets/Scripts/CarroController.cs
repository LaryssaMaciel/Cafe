using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroController : MonoBehaviour
{
    private float speed = 10f; // velocidade do carro 
    private Transform player; // auxiliar
    public AudioSource audioSource; // audio do carro
    public bool visible; // se ta perto do player/na tela

    private void Start() 
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>(); // acessa o player
        audioSource = GetComponent<AudioSource>(); // acessa audio
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed); // move automatico
        // se passou do player, se destroi
        if (this.transform.position.x < player.transform.position.x - 20f) { Destroy(this.gameObject); } 
    }

    void OnBecameVisible() // se apareceu na tela, ta visivel, buzina
    {
        audioSource.Play(); 
        visible = true;
    }

    void OnBecameInvisible() => visible = false; // fora da tela
}
