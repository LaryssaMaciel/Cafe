using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Instantiate(prefab, new Vector3(spawnPoint.transform.position.x + 35f, 
            spawnPoint.transform.position.y, spawnPoint.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
