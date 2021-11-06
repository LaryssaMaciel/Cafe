using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    public Sprite[] cenarios;
    public SpriteRenderer[] sp;
    private PlayerController pc;

    void Start()
    {
        pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        for (int i = 0; i < 3; i++)
        {
            sp[i].sprite = cenarios[pc.cena];
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pc.cena++;
            for (int i = 0; i < 3; i++)
            {
                sp[i].sprite = cenarios[pc.cena];
            }
            this.gameObject.SetActive(false);
        }
    }
}
