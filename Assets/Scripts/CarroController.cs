using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroController : MonoBehaviour
{
    private float speed = 10f; // velocidade do carro 
    private Transform player;
    // private PlayerController playerController;
    public AudioSource audioSource;
    public bool visible = false;

    private void Start() 
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>(); // acessa o player
        audioSource = GetComponent<AudioSource>();
        // playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed); // move automatico

        if (this.transform.position.x < player.transform.position.x - 20f) // se passou do player, se destroi
        {
            Destroy(this.gameObject);
        } 
        else if (this.transform.position.x > player.transform.position.x + 15f)
        {
            audioSource.Play();
        }

        // if (visible == true)
        // {
        //     if (!playerController.pause)
        //     {
        //         audioSource.Play();
        //     }
        //     else
        //     {
        //         audioSource.Stop();
        //     }
        // }
    }

    // void OnBecameVisible()
    // {
    //     visible = true;
    // }

    // void OnBecameInvisible()
    // {
    //     visible = false;
    // }
}
