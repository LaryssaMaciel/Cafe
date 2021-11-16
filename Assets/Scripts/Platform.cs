using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "playPlat")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (col.gameObject.tag == "playCol")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
