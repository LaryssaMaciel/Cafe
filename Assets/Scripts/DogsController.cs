using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogsController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed; // velocidade dos dogs
    private float jumpCounter, jumpTime = .2f, jumpForce; // contador, tempo max e forca do pulo
    public AudioSource audioSource; // audio dos dogs
    public GameObject dogsFollow;
    public bool jumping;

    // auxiliares
    private EndLVLController endLVL;
    private PlayerController player;
    private PlayerController pc; 

    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // acessa o player
        rb = GetComponent<Rigidbody2D>(); // acessa rigdbody
        if (this.gameObject.name == "Dogs") { audioSource = GetComponent<AudioSource>(); } // acessa audio
        dogsFollow = GameObject.Find("DogsFollow");
        // acessa scripts
        endLVL = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        speed = pc.speed; // atualiza velocidade de acordo com a do player
        Movement(); // chama metodo de movimentacao
        DogsNaCam(); // mantem dogs na area da camera

        // gerencia audio
        if (this.gameObject.name == "Dogs") {
            if (endLVL.end || player.pause) { this.audioSource.Pause(); }
            else if (!player.pause) { this.audioSource.UnPause(); }
        }
    }

    void Movement() 
    {
        // ponto x dos dogs, fixo, move automatico
        if (this.gameObject.name == "DogsFollow") { this.transform.Translate(Vector3.right * Time.deltaTime * speed); } 
        // dogs ficam fixo no ponto x
        if (this.gameObject.name == "Dogs") { this.transform.position = new Vector2(dogsFollow.transform.position.x, this.transform.position.y); }
    }
    
    void DogsNaCam() // manter player na visao da camera
    {
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);         
        pos.x = Mathf.Clamp01(pos.x);         
        pos.y = Mathf.Clamp01(pos.y);         
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    Collider2D col_; // objeto que colidiu com dogs
    void OnTriggerEnter2D(Collider2D col)
    {
        col_ = col; // atualiza objeto que colidiu
        // dependendo da tag do que colidiu
        switch(col.gameObject.tag) 
        {   
            // DESTROI OBSTACULO
            case "obstacle":
            case "car":
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(col_.gameObject, 1);
                break;
            // PULA OBSTACULO
            case "buraco":
                jumpForce = 7;
                jumpCounter = jumpTime;
                if (jumpCounter > 0)
                {
                    rb.velocity = jumpForce * Vector2.up;
                    jumpCounter -= Time.deltaTime;
                }
                break;
        }
    }
}
