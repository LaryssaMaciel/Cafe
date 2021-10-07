using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private LayerMask platformLayer; // layer do chao
    private Rigidbody2D rb;

    public bool isGrounded;
    public Transform feetPos;
    private float checkRadius = .3f;
    
    public float jumpTime = .4f, jumpCounter, jumpForce = 10f;
    public bool isJumping;

    public float speed = 5f;

    private EndLVLController endlvl;
    private bool pause = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        endlvl = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
    }

    void Update()
    {
        Jump();
        Movement();
        Pause();
    }

    void Pause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            pause = !pause;
        }

        if (pause && endlvl.end == false)
        {
            endlvl.EndScreen(0, true); // pause
        }
        else if (!pause && endlvl.end == false)
        {
            endlvl.EndScreen(1, false); // resume
        }
    }

    void Movement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed); // move
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "limbo": // se caiu no limbo e colidiu com isso
            case "dogs": // se colidiu com dogs
                Scene cena = SceneManager.GetActiveScene(); // pega nome da cena atual
                SceneManager.LoadScene(cena.name); // reseta cena
                break;
        }
    }
    
    void Jump ()
    {
        // checa se ta no chao
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, platformLayer); 
        
        if (isGrounded == true && Input.GetButtonDown("Jump")) // se apertou pra pular
        {
            isJumping = true;
            jumpCounter = jumpTime;
            rb.velocity = jumpForce * Vector2.up;
        }

        if (Input.GetButton("Jump") && isJumping == true) // se ta pulando
        {
            if (jumpCounter > 0)
            {
                rb.velocity = jumpForce * Vector2.up;
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else
        {
            isJumping = false;
        }
    }
}
