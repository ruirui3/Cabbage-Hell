using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    
    public float FIRING_DELAY_MUTIPLIER = 1f; //could consider having multiple firing delay multiplier constants for each respective bullet types
    public float SPEED_MULTIPLIER = 1f;

    public float bulletFiringDelay; // seconds per bullets
    public float ms = 0f;

    private float leftBound = -3f;
    private float rightBound = 3f;

    private float acceleration = 10f;
    private float deceleration = 20f;
    private float maxVelocity = 10f;
    private float currentVelocity = 0f;
    private bool reversing = false; // If the key pressed is opposing motion

    private int currentBulletType;
    private Queue<int> bulletQueue;

    void Start()
    {
        bulletFiringDelay = 1f;
        bulletQueue = new Queue<int>(); //need some way to reference bullets in inventory. Current bullet types: normal (0) triple (1) carrot (2) honey (3) curl (4) tornado (5)     - mostly in terms of progression of implementation
        
        for (int i = 0; i < 6; i++)
        {
            bulletQueue.Enqueue(i);  //Temporary for loop for enqueuing all of the bullet types
        }

    }

    void Update()
    {

        currentBulletType = bulletQueue.Peek();
        UpdatePosition();
        SeekAndRunBulletType(currentBulletType);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchBulletType();
        }

    }

    public void SwitchBulletType()
    {
        if (bulletQueue.Count != 0)
        {
            bulletQueue.Enqueue(bulletQueue.Dequeue()); //puts item in front to back, and places 2nd item to the front
        }
    }

    private void SeekAndRunBulletType(int bulletIndex)
    {
        ms += Time.deltaTime;

        if (bulletIndex == 0) //normal
        {
            NormalBulletFunction();
        }
        if (bulletIndex == 1) //triple
        {
            TripleBulletFunction();
        }
        if (bulletIndex == 2) //carrot
        {
            Debug.Log("Currently attempting to run carrot bullet. Not implemented. Please swap bullet type.");
            ms = 0;
        }
        if (bulletIndex == 3) //honey
        {
            Debug.Log("Currently attempting to run honey bullet. Not implemented. Please swap bullet type.");
            ms = 0;
        }
        if (bulletIndex == 4) //curl
        {
            Debug.Log("Currently attempting to run curl bullet. Not implemented. Please swap bullet type.");
            ms = 0;
        }
        if (bulletIndex == 5) //tornado
        {
            Debug.Log("Currently attempting to run tornado bullet. Not implemented. Please swap bullet type.");
            ms = 0;
        }
    }

    private void NormalBulletFunction()
    {
        if (ms >= bulletFiringDelay * FIRING_DELAY_MUTIPLIER)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);

            ms = 0;
            Debug.Log("Currently running normal bullet");
        }
        
    }

    private void TripleBulletFunction()
    {
        if (ms >= bulletFiringDelay * FIRING_DELAY_MUTIPLIER)
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);
            Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 45));
            Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 315));

            ms = 0;
            Debug.Log("Currently running triple bullet");
        }
    }

    private void UpdatePosition()
    {
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

    }
}
