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
    
    void Start()
    {
        // configura variaveis
        dc = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
        player = GameObject.FindWithTag("Player");
        pm = GameObject.FindWithTag("txtPontos").GetComponent<PontuacaoManager>();
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
                    dc.contEntregue++;
                    dc.contPego--;
                    pm.pontos += 50 * pm.multiplicador;
                    emojo.GetComponent<SpriteRenderer>().sprite = spt[0];
                    emojo.SetActive(true);
                }
                else if (this.gameObject.tag == "rest") // pega
                {
                    dc.contPego++;
                }
                this.used = true; // interagiu
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            key.SetActive(false); // esconde img do btn
            if (this.used == false && this.gameObject.tag == "cliente")
            {
                emojo.GetComponent<SpriteRenderer>().sprite = spt[1];
                emojo.SetActive(true);
            }
        }
    }
}
