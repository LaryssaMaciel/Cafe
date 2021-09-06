using System.Collections;
using System.Collections.Generic;
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

    public float speed = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Jump();
        Movement();
    }

    void Movement()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * speed); // move
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "box")
        {
            SceneManager.LoadScene("SampleScene"); // reseta cena
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
