using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogsController : MonoBehaviour
{
    private PlayerController pc; // acessa variavel
    public float speed;
    
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        speed = pc.speed; // atualiza velocidade de acordo com a do player
        Movement();
    }

    void Movement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
