using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject key; // img do botao de interacao
    public bool used = false; // se player interagiu com esse obj

    public GameObject emojo; 
    public Sprite[] spt;

    // variaveis auxiliaress
    private DeliveryController dc;
    public GameObject player;
    private PontuacaoManager pm;

    SoundManager soundManager;

    
    void Start()
    {
        // configura variaveis
        dc = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        player = GameObject.FindWithTag("Player");
        pm = GameObject.FindWithTag("txtPontos").GetComponent<PontuacaoManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {
        // se passou do cliente e nao fez entrega, ele fica com raiva
        if (this.used == false && this.gameObject.tag == "cliente" && this.transform.position.x < player.transform.position.x - 3f)
        {
            emojo.GetComponent<SpriteRenderer>().sprite = spt[1];
            emojo.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") // se player colidiu
        {
            this.key.SetActive(true); // mostra img do btn de interacao
            if (Input.GetButton("Acao") && !this.used) // se nao interagiu antes e clicou btn
            {
                if (this.gameObject.tag == "cliente" && dc.contPego > 0) // entrega
                {
                    player.GetComponent<PlayerController>().entrega = true;
                    dc.contEntregue++;
                    dc.contPego--;
                    pm.pontos += 50 * pm.multiplicador;
                    emojo.GetComponent<SpriteRenderer>().sprite = spt[0];
                    emojo.SetActive(true);
                    player.GetComponent<PlayerController>().audioSource.clip = soundManager.som[2];
                    player.GetComponent<PlayerController>().audioSource.Play();
                }
                else if (this.gameObject.tag == "cliente" && dc.contPego <= 0)
                {
                    emojo.GetComponent<SpriteRenderer>().sprite = spt[1];
                    emojo.SetActive(true);
                }
                
                if (this.gameObject.tag == "rest") // pega
                {
                    player.GetComponent<PlayerController>().entrega = true;
                    dc.contPego++;
                    player.GetComponent<PlayerController>().audioSource.clip = soundManager.som[2];
                    player.GetComponent<PlayerController>().audioSource.Play();
                }
                this.used = true; // interagiu
                //StartCoroutine("wait", 1f);
            }
        }
    }

    // IEnumerator wait()
    // {
    //     yield return new WaitForSeconds(1f);
    //     player.GetComponent<PlayerController>().anim.SetTrigger("a");
    // }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            key.SetActive(false); // esconde img do btn
            // player.GetComponent<PlayerController>().anim.ResetTrigger("entrega");
            // if (this.used == false && this.gameObject.tag == "cliente")
            // {
            //     emojo.GetComponent<SpriteRenderer>().sprite = spt[1];
            //     emojo.SetActive(true);
            // }
        }
    }
}
