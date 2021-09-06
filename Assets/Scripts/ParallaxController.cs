using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, length_, startpos, startpos_;
    public float parallaxEffect, parallaxEffectY; 
    public GameObject cam;
    
    void Start()
    {
        cam = GameObject.Find("CM vcam1"); //CM vcam1
        startpos = transform.position.x; // posicao inicial
        startpos_ = transform.position.y; // posicao inicial
        length = GetComponent<SpriteRenderer>().bounds.size.x; // tamanho desse sprite
        length_ = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect); // quao longe moveu em relacao a camera
        float dist = cam.transform.position.x * parallaxEffect; // quao longe moveu do ponto inicial
        
        float dist_ = cam.transform.position.y * parallaxEffectY; // quao longe moveu do ponto inicial

        transform.position = new Vector3(startpos + dist, startpos_ + dist_, transform.position.z); // move bg

        // reposiciona sprites
        if (temp > startpos + length) { startpos += length; }
        else if (temp < startpos - length) { startpos -= length; }

        // cam.transform.Translate(Vector3.right * Time.deltaTime); // move camera automatico
    }
}
