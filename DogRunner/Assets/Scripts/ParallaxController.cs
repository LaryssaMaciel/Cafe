using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private float length, startpos;
    public float parallaxEffect; 
    public GameObject cam;
    
    void Start()
    {
        cam = GameObject.Find("Main Camera");
        startpos = transform.position.x; // posicao inicial
        length = GetComponent<SpriteRenderer>().bounds.size.x; // tamanho desse sprite
    }

    void Update()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect); // quao longe moveu em relacao a camera
        float dist = cam.transform.position.x * parallaxEffect; // quao longe moveu do ponto inicial
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z); // move bg

        // reposiciona sprites
        if (temp > startpos + length) { startpos += length; }
        else if (temp < startpos - length) { startpos -= length; }

        cam.transform.Translate(Vector3.right * Time.deltaTime); // move camera automatico
    }
}
