using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // parametros
    private Rigidbody2D rb;
    private Renderer _renderer;
    private SpriteRenderer sr;
    public Animator anim;
    // auxiliares
    private PontuacaoManager pm;
    private CarroController carro;
    private SoundManager soundManager;
    private AutomaticCam camSpeed;
    // powerup
    private bool superSpeed = false; // powerup super velocidade
    private float speTime = 1.3f, speCounter; // tempo max do powerup
    // dogs
    public GameObject dogs; // dogs
    public bool dogsCol = false; // se colidiu com dogs

    public int animState = 0; // estado da animacao
    public bool manobra = false, entrega = false; // se fez manobra, se fez entrega
    public AudioSource audioSource, motoAudio; // audios

    [Header("VAR GLOBAL CENA ATUAL")]
    public int cena = 0; // cena q muda no fundo

    [Header("VIDAS")]
    public float vidas = 3; // vidas totais
    private bool invencivel = false; // dano/powerup
    private float invTime = 1.3f; // tempo max de invencibilidade
    private float invCounter = 0; // tempo atual invencibilidade
    public Image lifebar; // barra de vida

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
    public float speed = 0f; // velocidade do player
    public float velNormal = 10f;
    private Vector3 autoDir; // direcao da movimentacao automatica 

    [Header("PAUSE/ENDLVL")]
    public GameObject panel; // painel de pause
    public bool gameOver = false; // se morreu
    private EndLVLController endlvl; // acessa o endlvl
    public bool pause = false; // se ta pausado
    
    void Start()
    {
        // configurando variaveis
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
        camSpeed = GameObject.Find("AutoCam").GetComponent<AutomaticCam>();
    }

    void Update()
    {   // chama metodos
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

        if (entrega == true) // animacao de entrega
        {
            animState = 4;
            StartCoroutine("wait", 1f);
        }

        anim.SetInteger("animState", animState); // atualiza animacao
    }

    IEnumerator wait() // fim da animacao de entrega
    {
        yield return new WaitForSeconds(1f);
        entrega = false;
        animState = 0;
    }

    void PlayerNaCam() // manter player na visao da camera
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

    IEnumerator Act() // faz manobra de fato
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
        if (vidas <= 0) {
            vidas = 0;
            gameOver = true;
        }

        lifebar.fillAmount = vidas / 3; // life bar
    }

    float blinkTime = .10f, blinkCounter = 0; // timers pra piscar o player
    void PowerUps()
    {
        // super velocidade
        if (superSpeed)
        {
            if (speCounter > 0) { speCounter -= Time.deltaTime; } // diminui tempo do powerup
            else { // acaba poweup
                speCounter = 0;
                superSpeed = false;
            }
        }
        else // se nao tem powerup
        {   // volta velocidade a multiplicado normais
            speed = velNormal;
            camSpeed.speed = velNormal;
            pm.multiplicador = 1;
        }

        // invencibilidade
        if (invencivel)
        {
            sr.color = new Color(1f, 1f, 1f, .5f); // fica meio transparente
            if (invCounter > 0) { invCounter -= Time.deltaTime; } // diminui tempo do powerup
            else { // acaba poweup
                invCounter = 0;
                invencivel = false;
            }
            
            // pisca player
            if (blinkCounter > 0) { blinkCounter -= Time.deltaTime; } // diminui tempo do powerup
            else { // acaba blink
                _renderer.enabled = !_renderer.enabled;
                blinkCounter = blinkTime;
            }
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f); // full 
            _renderer.enabled = true;
        }
    }
    
    bool carStop = false;
    public void Pause() // metodo de pause
    {
        if (Input.GetButtonDown("Pause")) { pause = !pause; }
        
        if (pause && !endlvl.end || gameOver) // se pausou ou deu gameover
        {
            endlvl.EndScreen(0, true); // pause
            // pausa audios
            motoAudio.Pause();
            if (carro.visible && carro != null) {
                carro.audioSource.Pause();
                carStop = true;
            }
        }
        else if (!pause && !endlvl.end) // resume jogo
        {
            endlvl.EndScreen(1, false); // resume
            // despausa audios
            motoAudio.UnPause();
            if (carro.visible && carStop && carro != null) {
                carro.audioSource.UnPause();
                carStop = false;
            }
        }

        if (endlvl.end)
        {
            motoAudio.Pause();
            if (carro != null) {carro.audioSource.Pause(); }
        }
    }
    
    void Movement() //movimentacao do player
    {
        // define direcao da movimentacao automatica
        if (Input.GetKey(KeyCode.A)) { autoDir = Vector3.left; } // ré
        else { autoDir = Vector3.right; } // frente
        
        // movimentação em si (automatica + manual)
        this.transform.Translate(
            autoDir * Time.deltaTime * speed + // movimentacao automatica
            Vector3.right * Time.deltaTime * speed * Input.GetAxis("Horizontal")); // + movimentacao manual
    }

    void AudioManager(int audio)
    {
        audioSource.clip = soundManager.som[audio];
        audioSource.Play();
    }
    
    void OnTriggerStay2D(Collider2D col)
    {
        if (!invencivel && !gameOver) // se nao ta invencivel nem deu gameover
        {
            switch (col.gameObject.tag)
            {   // leva dano, se colidiu com
                case "dogs": 
                case "obstacle": 
                case "car":
                    vidas--; // perde vida
                    this.rb.AddForce(-this.transform.right * 150, ForceMode2D.Impulse);
                    invencivel = true; // invencibilidade breve
                    invTime = 1.3f; // tempo de invencibilidade
                    invCounter = invTime; // começa contador
                    AudioManager(8); // som de dano
                    break;
            }
        }

        switch (col.gameObject.tag)
        {   // gameover, se colidiu com
            case "limbo":
                AudioManager(3); // som de derrota
                gameOver = true;
                endlvl.end = true;
                dogsCol = true;
                break;
            case "dogs":
                if (gameOver) { dogsCol = true; }
                break;
            //  POWER UPS   
            case "powerup":
                AudioManager(6); // som de powerup
                // invencibilidade
                if (col.gameObject.GetComponent<PowerUpManager>().tipo == "inv")
                {  
                    invencivel = true;
                    invTime = 1.3f;
                    invCounter = invTime;
                }
                // super velocidade
                else if (col.gameObject.GetComponent<PowerUpManager>().tipo == "vel")
                {
                    speed = speed * 2;
                    camSpeed.speed = camSpeed.speed * 2;
                    superSpeed = true;
                    speTime = 2f;
                    pm.multiplicador = 2;
                    speCounter = speTime;
                }
                Destroy(col.gameObject);
                break;
        }
    }
    
    void Jump () // metodo de pulo
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, platformLayer); // checa se ta no chao
        
        if (isGrounded && Input.GetButtonDown("Jump") && !manobra) // se ta no chao, apertou pra pular e nao fez manobra
        {   // pode pular
            isJumping = true;
            jumpCounter = jumpTime; // timer de pulo
            rb.velocity = jumpForce * Vector2.up; // vai pra cima
        }
        else if (!isGrounded && !entrega) { animState = 0; } // transicao pra animacao de idle

        if (Input.GetButton("Jump") && isJumping) // se ta pulando
        {
            animState = 1; // animacao de pulo
            if (jumpCounter > 0) {
                rb.velocity = jumpForce * Vector2.up; // continua o pulo
                jumpCounter -= Time.deltaTime; // diminui timer de pulo
            } else { isJumping = false; } // acabou o pulo
        } else { isJumping = false; } // parou de pular
    }
    
    public void EndScreen() // tela de endgame/pause
    {
        if (gameOver && !dogsCol) // se deu gameover e dogs nao pegaram player
        {   
            Time.timeScale = 0.5f; // camera lenta
            if (Vector3.Distance(this.transform.position, dogs.transform.position) >= 0) // se dogs tao longe do player
            {   // dogs vao ate player
                dogs.transform.position = Vector3.MoveTowards(dogs.transform.position, this.transform.position, 10 * Time.deltaTime);
            } else { dogsCol = true; } // se chegou no player, colidiu
        }

        if (dogsCol) // se colidiu com dogs
        {   // pause e mostra tela de endgame
            Time.timeScale = 0; 
            panel.SetActive(true);
        }
    }
}
