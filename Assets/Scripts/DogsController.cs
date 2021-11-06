using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogsController : MonoBehaviour
{
    private PlayerController pc; // acessa o player
    public float speed; // velocidade dos dogs
    
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // acessa o player
    }

    void Update()
    {
        speed = pc.speed; // atualiza velocidade de acordo com a do player
        Movement();
    }

    void Movement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed); // move automatico
    }
}
