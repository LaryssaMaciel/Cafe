using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogsController : MonoBehaviour
{
    private PlayerController pc; // acessa o player
    private Rigidbody2D rb;
    public float speed; // velocidade dos dogs
    float jumpCounter, jumpTime = .2f, jumpForce;
    private AudioSource audioSource;
    private EndLVLController endLVL;
    private PlayerController player;
    
    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // acessa o player
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        endLVL = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        speed = pc.speed; // atualiza velocidade de acordo com a do player
        Movement();

        if (endLVL.end || player.pause)
        {
            this.audioSource.Pause();
        }
        else if (!player.pause)
        {
            this.audioSource.UnPause();
        }
    }

    void Movement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed); // move automatico
    }
    
    Collider2D col_;
    void OnTriggerEnter2D(Collider2D col)
    {
        col_ = col;
        // switch(col.gameObject.tag)
        // {
        //     case "buraco":
        //         jumpForce = 8;
        //         break;
        //     // case "obstacleDog":
        //     //     jumpForce = 5;
        //     //     break;
        //     // case "car":
        //     //     jumpForce = 6.5f;
        //     //     break;
        // }
        switch(col.gameObject.tag)
        {
            // DESTROI OBSTACULO
            case "obstacle":
            case "car":
                col.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                col.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine("destroyDelay", 1f);
                break;
            // PULA O BURACO
            case "buraco":
            // case "obstacleDog":
                jumpForce = 8;
                jumpCounter = jumpTime;
                if (jumpCounter > 0)
                {
                    rb.velocity = jumpForce * Vector2.up;
                    jumpCounter -= Time.deltaTime;
                }
                break;
        }
    }

    IEnumerator destroyDelay() 
    {
        yield return new WaitForSeconds(1f);
        Destroy(col_.gameObject);
    }
}
