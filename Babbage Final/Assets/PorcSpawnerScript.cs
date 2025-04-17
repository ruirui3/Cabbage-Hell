using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcSpawnerScript : MonoBehaviour
{
    public GameObject PorcBullet;
    public float startingSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 2 == 0) {
            Instantiate(PorcBullet, transform.position, transform.rotation);
        }
    }
}
