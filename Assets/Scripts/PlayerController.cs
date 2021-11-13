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
    private SpriteRenderer sr;
    public Animator anim;
    public int animState = 0;
    public bool manobra = false, entrega = false;
    private PontuacaoManager pm;
    private bool superSpeed = false;
    private float speTime = 4f, speCounter;
    public AudioSource audioSource, motoAudio;
    private SoundManager soundManager;
    private CarroController carro;

    public GameObject dogs;
    public bool dogsCol = false; // se colidiu com dogs

    [Header("VAR GLOBAL CENA ATUAL")]
    public int cena = 0;

    [Header("VIDAS")]
    public float vidas = 3;
    private bool invencivel = false;
    private float invTime = 2f;
    private float invCounter = 0;
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
    public bool pause = false; // se ta pausado
    
    void Start()
    {
        // configurando variavel ao inicio da fase
        gameOver = false;
        rb = GetComponent<Rigidbody2D>();
        endlvl = GameObject.FindWithTag("end").GetComponent<EndLVLController>();
        _renderer = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        lifebar = GameObject.FindWithTag("lifebar").GetComponent<Image>();
        dogsCol = false;
        pm = GameObject.FindWithTag("txtPontos").GetComponent<PontuacaoManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        audioSource = GetComponent<AudioSource>();
        motoAudio = GameObject.Find("FeetPos").GetComponent<AudioSource>();
        carro = GameObject.FindWithTag("car").GetComponent<CarroController>();
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
        PowerUps();
        VidasManager();
        Manobra();
        PlayerNaCam();    

        if (entrega == true)
        {
            animState = 4;
            StartCoroutine("wait", 1f);
        }

        anim.SetInteger("animState", animState);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1f);
        entrega = false;
        animState = 0;
    }

    void PlayerNaCam() // manter player na visaod a camera
    {
        Vector3 pos = Camera.main.WorldToViewportPoint (transform.position);         
        pos.x = Mathf.Clamp01(pos.x);         
        pos.y = Mathf.Clamp01(pos.y);         
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void Manobra()
    {
        if (Input.GetButtonDown("Acao1") && !isJumping && !gameOver && !anim.GetBool("manobra") && !anim.GetBool("entrega"))
        {
            StartCoroutine("Act", 1.5f);
            manobra = true;
            animState = 3;
        } 
    }

    IEnumerator Act()
    {
        anim.SetBool("manobra", true);
        yield return new WaitForSeconds(1.5f);
        pm.pontos += 10 * pm.multiplicador;
        anim.SetBool("manobra", false);
        manobra = false;
        animState = 0;
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

    float x = .25f, y = 0; // timers pra piscar o player
    void PowerUps()
    {
        // super velocidade
        if (superSpeed)
        {
            if (speCounter > 0)
            {
                speCounter -= Time.deltaTime;
            }
            else
            {
                speCounter = 0;
                superSpeed = false;
            }
        }
        else
        {
            speed = 5f;
            pm.multiplicador = 1;
        }

        // invencibilidade
        if (invencivel)
        {
            sr.color = new Color(1f, 1f, 1f, .5f);
            if (invCounter > 0) { invCounter -= Time.deltaTime; }
            else {
                invCounter = 0;
                invencivel = false;
            }
            
            // pisca player
            if (y > 0) { y -= Time.deltaTime; }
            else {
                _renderer.enabled = !_renderer.enabled;
                y = x;
            }
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
            _renderer.enabled = true;
        }
    }
    
    bool carStop = false;
    void Pause() // metodo de pause
    {
        if (Input.GetButtonDown("Pause"))
        {
            pause = !pause;
        }
        
        if (pause && endlvl.end == false)
        {
            endlvl.EndScreen(0, true); // pause
            motoAudio.Pause();
            if (carro.visible)
            {
                carro.audioSource.Pause();
                carStop = true;
            }
        }
        else if (!pause && endlvl.end == false)
        {
            endlvl.EndScreen(1, false); // resume
            motoAudio.UnPause();
            if (carro.visible && carStop)
            {
                carro.audioSource.UnPause();
                carStop = false;
            }
        }

        if (endlvl.end)
        {
            motoAudio.Pause();
            carro.audioSource.UnPause();
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
        if (invencivel == false && gameOver == false)
        {
            switch (col.gameObject.tag)
            {
                case "dogs": // se colidiu com dogs
                case "obstacle": // obstaculos normais
                case "car":
                    vidas--;
                    invencivel = true;
                    invTime = 2f;
                    invCounter = invTime;
                    audioSource.clip = soundManager.som[8];
                    audioSource.Play();
                    break;
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
            //  POWER UPS   
            case "powerup":
                audioSource.clip = soundManager.som[6];
                audioSource.Play();
                // invencibilidade
                if (col.gameObject.GetComponent<PowerUpManager>().tipo == "inv")
                {  
                    invencivel = true;
                    invTime = 4f;
                    invCounter = invTime;
                }
                // super velocidade
                else if (col.gameObject.GetComponent<PowerUpManager>().tipo == "vel")
                {
                    speed = 10f;
                    superSpeed = true;
                    speTime = 4f;
                    pm.multiplicador = 2;
                    speCounter = speTime;
                }
                Destroy(col.gameObject);
                break;
        }
    }
    
    void Jump () // metodo de pulo
    {
        // checa se ta no chao
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, platformLayer); 
        
        if (isGrounded == true && Input.GetButtonDown("Jump") && !manobra) //&&falling == false | se apertou pra pular
        {
            isJumping = true;
            jumpCounter = jumpTime;
            rb.velocity = jumpForce * Vector2.up;
            
        }
        else if (!isGrounded && !entrega)
        {
            animState = 0;
            // anim.SetBool("ground", true);
            // anim.SetBool("jump", false);
        }

        if (Input.GetButton("Jump") && isJumping == true) // se ta pulando
        {
            // anim.SetBool("jump", true);
            // anim.SetBool("ground", false);
            animState = 1;
            
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
