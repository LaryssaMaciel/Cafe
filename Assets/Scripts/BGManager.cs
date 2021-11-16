using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public Sprite[] cenarios; // cenarios que vao ficar no fundo em parallax
    public SpriteRenderer[] sp; // sprites dos cenarios
    // auxiliares
    private PlayerController pc; 
    public SoundManager soundManager;

    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); // acessa o script do player
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        for (int i = 0; i < 3; i++) { sp[i].sprite = cenarios[pc.cena]; } // atualiza os sprites inciais do cenario
        soundManager.AudioManager(pc.cena); // muda musica
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") // se player colidiu com BGManager
        {
            pc.cena++; // incrementa
            for (int i = 0; i < 3; i++) { sp[i].sprite = cenarios[pc.cena]; } // atualiza BG de acordo com numero da cena
            soundManager.AudioManager(pc.cena); // muda musica
            this.gameObject.SetActive(false); // esconde esse BGManager
        }
    }
}
