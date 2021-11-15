using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, startpos, startpos_; // tamanho, posicao x inicial e posicao y inicial da imagem
    public float parallaxEffect, parallaxEffectY; // efeito parallax no x e no y
    public GameObject cam; // camera
    
    void Start()
    {   // configura variaveis
        cam = GameObject.FindWithTag("cmCam"); //CM vcam1
        startpos = transform.position.x; // posicao inicial x
        startpos_ = transform.position.y; // posicao inicial y
        length = GetComponent<SpriteRenderer>().bounds.size.x; // tamanho desse sprite
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect); // quao longe moveu em relacao a camera
        float dist = cam.transform.position.x * parallaxEffect; // quao longe moveu do ponto inicial x
        float dist_ = cam.transform.position.y * parallaxEffectY; // quao longe moveu do ponto inicial y

        transform.position = new Vector3(startpos + dist, startpos_ + dist_, transform.position.z); // move bg

        // reposiciona sprites (infinito)
        if (temp > startpos + length) { startpos += length; }
        else if (temp < startpos - length) { startpos -= length; }
    }
}
