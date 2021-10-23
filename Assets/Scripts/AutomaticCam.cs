using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCam : MonoBehaviour
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
        this.transform.Translate(Vector3.right * Time.deltaTime * speed);
    }
}
