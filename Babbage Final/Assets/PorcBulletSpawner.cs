using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcBulletSpawner : MonoBehaviour
{
    public AudioClip damageSFX;
    public bool porcShooting = false;
    public GameObject PorcBullet;
    public float timer = 0f;
    public float spawnInterval = 2f;
    public float walkBound = 10f;
    public float bottomBound = -5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Porcupine bullets
        if (transform.position.y < walkBound) {
            if (timer >= spawnInterval) {
                Instantiate(PorcBullet, transform.position, transform.rotation);
                Instantiate(PorcBullet, transform.position, Quaternion.Euler(0, 0, 30));
                Instantiate(PorcBullet, transform.position, Quaternion.Euler(0, 0, -30));
                timer = 0f;
            }
        }
    }
}
