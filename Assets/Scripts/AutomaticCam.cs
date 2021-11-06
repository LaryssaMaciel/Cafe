using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCam : MonoBehaviour
{
    private PlayerController pc; // acessa variavel
    public float speed; // velocidade de movimento da camera
    
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // acessa script
    }

    void Update()
    {
        speed = pc.speed; // atualiza velocidade de acordo com a do player
        this.transform.Translate(Vector3.right * Time.deltaTime * speed); // move automatico
    }
}
