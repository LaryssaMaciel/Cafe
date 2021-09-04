using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private LayerMask platformLayer;
    private Rigidbody2D rb;

    public bool isGrounded;
    public Transform feetPos;
    private float checkRadius = .3f;
    
    public float jumpTime = .4f, jumpCounter, jumpForce = 10f;
    public bool isJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, platformLayer);
        
        if (isGrounded == true && Input.GetButtonDown("Fire1"))
        {
            isJumping = true;
            jumpCounter = jumpTime;
            Jump(rb);
        }

        if (Input.GetButton("Fire1") && isJumping == true)
        {
            if (jumpCounter > 0)
            {
                Jump(rb);
                jumpCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isJumping = false;
        }
    }
    
    public void Jump (Rigidbody2D rb2d)
    {
        rb2d.velocity = jumpForce * Vector2.up;
    }
}
