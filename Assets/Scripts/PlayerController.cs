using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer _renderer;
    private Animator anim;

    public GameObject dogs;
    public bool dogsCol = false; // se colidiu com dogs

    [Header("VAR GLOBAL CENA ATUAL")]
    public int cena = 0;

    [Header("VIDAS")]
    public float vidas = 3;
    public bool invencivel = false;
    public float invTime = 2f;
    public float invCounter = 0;
    public Image lifebar;

    [Header("GROUND CHECK")]
    [SerializeField] private LayerMask platformLayer; // layer do chao
    public bool isGrounded; // verifica se ta no chao
    public Transform feetPos; // posicao do pe
    private float checkRadius = .3f; // auxiliar pra verificar se ta no chao
    
    [Header("PULO")]
    // tempo maximo de pulo, tempo atual de pulo, forca do pulo 
    public float jumpTime = .4f;
    public float jumpCounter;
    public float jumpForce = 10f;
    public bool isJumping; // se ta pulando

    [Header("MOVIMENTACAO")]
    public float speed = 5f; // velocidade do plaeyr
    private Vector3 autoDir; // direcao da movimentacao automatica 

    [Header("PAUSE/ENDLVL")]
    public GameObject panel; // painel de pause
    public bool gameOver = false; // se morreu
    private EndLVLController endlvl; // acessa o endlvl
    private bool pause = false; // se ta pausado
    //private bool falling = false;
    SpriteRenderer a;
    void Start()
    {
        // configurando variavel ao inicio da fase
        gameOver = false;
        rb = GetComponent<Rigidbody2D>();
        endlvl = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
        _renderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        a = GetComponent<SpriteRenderer>();
        lifebar = GameObject.FindWithTag("lifebar").GetComponent<Image>();
        dogsCol = false;
    }

    void Update()
    {
        if (!gameOver)
        {
            Movement();
            Jump();
        }
        Pause();
        EndScreen();
        Invencibilidade();
        VidasManager();
    }

    void VidasManager()
    {
        if (vidas <= 0)
        {
            vidas = 0;
            gameOver = true;
        }

        lifebar.fillAmount = vidas / 3; // life bar
    }

    void Invencibilidade()
    {
        if (invencivel == true)
        {
            if (invCounter > 0)
            {
                invCounter -= Time.deltaTime;
                a.color = Color.red;
            }
            else
            {
                
                a.color = Color.white;
                invencivel = false;
            }
        }
    }

    void OnBecameInvisible() // se sair da camera, reseta
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString()); // reseta cena
        //gameOver = true;
    }

    void Pause() // metodo de pause
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
    
    // public WheelJoint2D[] rodas;
    void Movement() //movimentacao do player
    {
        // for (int i = 0; i < rodas.Length; i++)
        // {
        //     JointMotor2D motor = rodas[i].motor;
        //     motor.motorSpeed = Input.GetAxis("Horizontal") * 200;
        //     rodas[i].motor = motor;
        // }

        // define direcao da movimentacao automatica
        if (Input.GetKey(KeyCode.A)) 
        {
            autoDir = Vector3.left; // ré
        }
        else
        {
            autoDir = Vector3.right;
        }
        
        // movimentação em si (automatica + manual)
        this.transform.Translate(autoDir * Time.deltaTime * speed + // movimentacao automatica
                                 Vector3.right * Time.deltaTime * speed * Input.GetAxis("Horizontal")); // + movimentacao manual
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (!invencivel && !gameOver)
        {
            switch (col.gameObject.tag)
            {
                case "dogs": // se colidiu com dogs
                case "obstacle": // obstaculos normais
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name.ToString()); // reseta cena
                    vidas--;
                    invencivel = true;
                    invCounter = invTime;
                    break;
                //case "buraco":
                    //falling = true;
                    // this.gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
                    // this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    //break;
            }
        }

        switch (col.gameObject.tag)
        {
            case "limbo":
                gameOver = true;
                dogsCol = true;
                break;
            case "dogs":
                if (gameOver)
                {
                    dogsCol = true;
                }
                break;
        }
    }
    
    void Jump () // metodo de pulo
    {
        // checa se ta no chao
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, platformLayer); 
        
        if (isGrounded == true && Input.GetButtonDown("Jump")) //&&falling == false | se apertou pra pular
        {
            isJumping = true;
            jumpCounter = jumpTime;
            rb.velocity = jumpForce * Vector2.up;
            
        }
        else if (isGrounded == true)
        {
            anim.SetBool("ground", true);
            anim.SetBool("jump", false);
        }

        if (Input.GetButton("Jump") && isJumping == true) // se ta pulando
        {
            anim.SetBool("jump", true);
            anim.SetBool("ground", false);
            
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
    
    public void EndScreen() // tela de fim de fase/pause
    {
        if (gameOver && !dogsCol)
        {
            Time.timeScale = 0.5f;
            if (Vector3.Distance(this.transform.position, dogs.transform.position) >= 0)
            {
                dogs.transform.position = Vector3.MoveTowards(dogs.transform.position, this.transform.position, 10 * Time.deltaTime);
            }
            else
            {
                dogsCol = true;
            }
        }

        if (dogsCol)
        {
            Time.timeScale = 0;
            panel.SetActive(true);
        }
    }
}
