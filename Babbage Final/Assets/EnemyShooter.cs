using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject EnemyBullet;
    public float speed = 5f;
    public int rate = 1;
    public int mils = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mils ++;

        int count = 360/rate;

        if (mils%count == 0) {
            // Vector3 spawnPosition = transform.position + Vector3.up * (transform.localScale.y + bullet.transform.localScale.y);
            Vector3 spawnPosition = transform.position + Vector3.down  * 1.5f; // temporary displacement to avoid bumping into plane. TODO: make bullet not collide with plane
            //changed .up to .down so it should go downwards?
            Instantiate(EnemyBullet, spawnPosition, transform.rotation);
        }
    }
}
