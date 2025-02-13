using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    public float FIRING_DELAY_MUTIPLIER = 1f;
    public float SPEED_MULTIPLIER = 1f;
    private float horizontal;
    public float bulletFiringDelay; // seconds per bullets
    public float ms = 0f;
    private float leftBound = -3f;
    private float rightBound = 3f;
    private bool tripleBullets = false;

    private float acceleration = 10f;
    private float deceleration = 20f;
    private float maxVelocity = 10f;
    private float currentVelocity = 0f;
    private bool reversing = false; // If the key pressed is opposing motion

    void Start()
    {
        tripleBullets = true;
        bulletFiringDelay = 1f; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            tripleBullets = !tripleBullets;
        }

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            moveInput = 0;
        }

        // Detect if player is reversing motion
        bool isChangingDirection = ((moveInput != 0) && (Mathf.Sign(moveInput) != Mathf.Sign(currentVelocity)) && (currentVelocity != 0));

        if (isChangingDirection)
        {
            reversing = true;
        }

        if (reversing)
        {
            currentVelocity -= Mathf.Sign(currentVelocity) * deceleration * 2f * Time.deltaTime;

            if (Mathf.Abs(currentVelocity) < 0.1f)
            {
                currentVelocity = 0f;
                reversing = false;
            } //just set velocity to 0 if it is slow enough
        }
        else
        {
            // Normal acceleration
            if (moveInput != 0)
            {
                currentVelocity += moveInput * acceleration * Time.deltaTime;
            }
            else
            {
                // Normal deceleration when no input is given
                if (currentVelocity != 0)
                {
                    currentVelocity -= Mathf.Sign(currentVelocity) * deceleration * Time.deltaTime;
                }

                // Stop when velocity gets very small
                if (Mathf.Abs(currentVelocity) < 0.1f)
                {
                    currentVelocity = 0f;
                }
            }
        }

        // Returns value within the min max range so that it doesn't go over or under
        currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);

        // Apply movement
        transform.position += new Vector3(currentVelocity * Time.deltaTime, 0, 0) * SPEED_MULTIPLIER;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 
            leftBound, rightBound), transform.position.y, 
            transform.position.z
            ); //checks if in bound

        // Stop velociy when hit bound
        if (transform.position.x <= leftBound || transform.position.x >= rightBound)
        {
            currentVelocity = 0f;
        }

        ms += Time.deltaTime;

        if (ms >= bulletFiringDelay * FIRING_DELAY_MUTIPLIER)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);

            if (tripleBullets)
            {
                Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 45));
                Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 315));
            }

            ms = 0;
        }
    }
}
