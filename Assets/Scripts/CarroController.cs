using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarroController : MonoBehaviour
{
    private float speed = 10f;
    private Transform player;

    private void Start() => player = GameObject.FindWithTag("Player").GetComponent<Transform>();

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);

        if (this.transform.position.x < player.transform.position.x - 7f)
        {
            Destroy(this.gameObject);
        }
    }
}
