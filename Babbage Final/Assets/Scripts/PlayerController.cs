
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    private float horizontal;
    public float speed = 5f;
    public float rate; //bullet/second
    public float ms = 0f;
    private float leftBound = -3f;
    private float rightBound = 3f;
    private bool tripleBullets = false; 

    // Start is called before the first frame update
    void Start()
    {
        tripleBullets=true;
        rate = 1f; //could be changed

    }

    // Update is called once per frame
    void Update()
    {




        if (transform.position.x > leftBound && transform.position.x < rightBound) {
            horizontal = Input.GetAxis("Horizontal");
            horizontal *= speed * Time.deltaTime;
            transform.Translate(new Vector3(horizontal, 0f, 0f));
        } else if (transform.position.x <= leftBound)
        {
            transform.position = new Vector3(leftBound+0.001f, transform.position.y, transform.position.z);

        } else
        {
            transform.position = new Vector3(rightBound - 0.001f, transform.position.y, transform.position.z);
        }

        Debug.Log(ms);

        ms += Time.deltaTime;

        if (ms >= rate) {
            // Vector3 spawnPosition = transform.position + Vector3.up * (transform.localScale.y + bullet.transform.localScale.y);
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f; // temporary displacement to avoid bumping into plane. TODO: make bullet not collide with plane
            Instantiate(bullet, spawnPosition, transform.rotation);

            if (tripleBullets)
            {
                Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 45));
                Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 315));
            }
            ms = 0;

            
            
        }
    }

    void EnableTripleBullets()
    {
        tripleBullets = true;
    }

    void DisableTripleBullets()
    {
        tripleBullets = false;
    }



}