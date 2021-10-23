using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    public GameObject key;
    public bool used = false; 

    private DeliveryController dc;
    
    void Start()
    {
        dc = GameObject.FindWithTag("Player").GetComponent<DeliveryController>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.key.SetActive(true);
            if (Input.GetButton("Acao") && !this.used)
            {
                if (this.gameObject.tag == "cliente")
                {
                    dc.contEntregue++;
                    dc.contPego--;
                }
                else if (this.gameObject.tag == "rest")
                {
                    dc.contPego++;
                }
                this.used = true;
                // add feedback
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            key.SetActive(false);
        }
    }
}
