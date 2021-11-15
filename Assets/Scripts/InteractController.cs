using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject key; // img do botao de interacao
    public bool used = false; // se player interagiu com esse obj
    SoundManager soundManager; // audio
    public GameObject emojo; // obj do emote do cliente
    public Sprite[] spt; // sprites de emoji

    // auxiliares
    private DeliveryController dc;
    public GameObject player;
    private PontuacaoManager pm;
    
    void Start()
    {   // configura variaveis
        dc = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        player = GameObject.FindWithTag("Player");
        pm = GameObject.FindWithTag("txtPontos").GetComponent<PontuacaoManager>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    void Update()
    {   // se passou do cliente e nao fez entrega, ele fica com raiva
        if (this.used == false && this.gameObject.tag == "cliente" 
            && this.transform.position.x < player.transform.position.x - 3f) { EmojiUpdate(1, true); }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") // se player colidiu
        {
            this.key.SetActive(true); // mostra img do btn de interacao
            if (Input.GetButton("Acao") && !this.used) // se nao interagiu antes e clicou btn de interacao
            {   
                if (this.gameObject.tag == "cliente") // se colidiu com cliente
                {   
                    if (dc.contPego > 0) // se tem lanche sobrando
                    {
                        player.GetComponent<PlayerController>().entrega = true; // faz entrega
                        dc.contEntregue++; // incrementa entregas
                        dc.contPego--; // decrementa lanches
                        pm.pontos += 50 * pm.multiplicador; // add pontos
                        EmojiUpdate(0, true); // cliente feliz
                        AudioUpdate(2); // toca audio de entrega
                    }
                    else { EmojiUpdate(1, true); } // se nao tem lanche, cliente com raiva
                }
                
                if (this.gameObject.tag == "rest") // se colidiu com restaurante
                {
                    player.GetComponent<PlayerController>().entrega = true; // fez entrega
                    dc.contPego++; // atualiza contador
                    AudioUpdate(2); // toca audio de entrega
                }
                this.used = true; // interagiu
            }
        }
    }

    void EmojiUpdate(int sprite, bool active) // atualiza sprite de feedback do cliente
    {
        emojo.GetComponent<SpriteRenderer>().sprite = spt[sprite]; // cliente com raiva
        emojo.SetActive(active);
    }

    void AudioUpdate(int audio) // atualiza audio
    {
        player.GetComponent<PlayerController>().audioSource.clip = soundManager.som[audio];
        player.GetComponent<PlayerController>().audioSource.Play();
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") { key.SetActive(false); } // esconde img do btn
    }
}
