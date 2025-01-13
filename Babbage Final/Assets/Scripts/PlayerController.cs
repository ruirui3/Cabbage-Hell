
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    private float horizontal;
    public float speed = 5f;
    public int rate = 1;
    public int ms = 0;
    private float leftBound = -3f;
    private float rightBound = 3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ms ++;




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

        

        int count = 360/rate;

        if (ms%count == 0) {
            // Vector3 spawnPosition = transform.position + Vector3.up * (transform.localScale.y + bullet.transform.localScale.y);
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f; // temporary displacement to avoid bumping into plane. TODO: make bullet not collide with plane
            Instantiate(bullet, spawnPosition, transform.rotation);
        }
    }
}