using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bullet;
    public AudioClip normalShot, tripleShot, carrotShot, curlShot;
    
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
    private float[] bulletCount = {Mathf.Infinity, 5f, 0f, 0f, 0f, 0f};

    public AudioClip bulletSwitch;

    public GameObject honeyBulletPrefab;
    public float honeyExplosionRadius = 2f;
    public float honeySlowDuration = 3f;
    public float honeySlowFactor = 0.5f;



    public float GetVelocity()
    {
        return Mathf.Abs(currentVelocity);
    }

    public float GetMaxVelocity()
    {
        return maxVelocity;
    }

    void Start()
    {
        bulletFiringDelay = 1f;
        bulletQueue = new Queue<int>(); //need some way to reference bullets in inventory. Current bullet types: normal (0) triple (1) carrot (2) honey (3) curl (4) tornado (5)     - mostly in terms of progression of implementation

        EnqueueBulletType(0);
        EnqueueBulletType(3);
    }

    void Update()
    {
        addBullet(3, 999);
        currentBulletType = bulletQueue.Peek();
        UpdatePosition();
        SeekAndRunBulletType(currentBulletType);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            AudioSource.PlayClipAtPoint(bulletSwitch, transform.position);
            SwitchBulletType();

        }

    }

    public int PeekFirstBullet()
    {
        return bulletQueue.Peek();
    }

    public int PeekSecondBullet()
    {
        if (bulletQueue.Count < 2)
        {
            Debug.LogWarning("Not enough bullets in queue to peek the second one.");
            return -1; //no bullet
        }

        return bulletQueue.ToArray()[1];
    }

    public void addBullet(int type, int amount) {
        bulletCount[type] += (float) amount;
        EnqueueBulletType(type);
    }
    
    public void EnqueueBulletType(int bulletTypeIndex)
    {

        if (!bulletQueue.Contains(bulletTypeIndex))
        {
            bulletQueue.Enqueue(bulletTypeIndex);
        } else
        {
            Debug.Log("Bullet already exists within the queue");
        }

    }

    public int RemoveBulletType(int bulletIndexToBeRemoved) //not tested
    {
        int currentIndex = -1;
        if (bulletQueue.Contains(bulletIndexToBeRemoved))
        {
            
            while (bulletQueue.Contains(bulletIndexToBeRemoved))
            {
                currentIndex = bulletQueue.Dequeue();

                if (currentIndex != bulletIndexToBeRemoved)
                {
                    bulletQueue.Enqueue(currentIndex);
                }

            }
            
        }

        return currentIndex;
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
        if (ms < bulletFiringDelay * FIRING_DELAY_MUTIPLIER) {
            return;
        }
        ms = 0;

        if (bulletIndex == 0 && bulletCount[bulletIndex] > 0) //normal
        {
            NormalBulletFunction();
        }
        if (bulletIndex == 1 && bulletCount[bulletIndex] > 0) //triple
        {
            TripleBulletFunction();
        }
        if (bulletIndex == 2 && bulletCount[bulletIndex] > 0) //carrot
        {
            CarrotBulletFunction();
        }
        if (bulletIndex == 3 && bulletCount[bulletIndex] > 0) //honey
        {
            HoneyBulletFunction();
        }
        if (bulletIndex == 4 && bulletCount[bulletIndex] > 0) //curl
        {
            CurlBulletFunction();
        }
        if (bulletIndex == 5 && bulletCount[bulletIndex] > 0) //tornado
        {
            Debug.Log("Currently attempting to run tornado bullet. Not implemented. Please swap bullet type.");
            ms = 0;
        }

        bulletCount[bulletIndex]--;

        // if bullet type is empty, switch to next
        if (bulletCount[bulletIndex] <= 0) {
            bulletQueue.Dequeue(); // should always be at least one remaining
        }
    }

    private void NormalBulletFunction()
    {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);
            AudioSource.PlayClipAtPoint(normalShot, transform.position);

            Debug.Log("Currently running normal bullet");
        
    }

    private void CarrotBulletFunction()
    {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);
            AudioSource.PlayClipAtPoint(carrotShot, transform.position);

            Debug.Log("Currently running carrot bullet");
    }

    private void CurlBulletFunction()
    {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);
            AudioSource.PlayClipAtPoint(curlShot, transform.position);

            Debug.Log("Currently running curl bullet");
    }

    public int GetBulletType()
    {
        return bulletQueue.Peek();
    }

    private void TripleBulletFunction()
    {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
            Instantiate(bullet, spawnPosition, transform.rotation);
            Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 45));
            Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, 315));
            AudioSource.PlayClipAtPoint(tripleShot, transform.position);

            Debug.Log("Currently running triple bullet");
    }

    private void HoneyBulletFunction()
    {
        Vector3 spawnPosition = transform.position + Vector3.up * 1.5f;
        Debug.Log("Shots fired");
        Instantiate(honeyBulletPrefab, spawnPosition, Quaternion.identity);
        AudioSource.PlayClipAtPoint(carrotShot, transform.position); // Use unique sfx if desired
        Debug.Log("Currently running honey bullet");
    }
    private void UpdatePosition()
    {
        // 1) Read raw input
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
            moveInput = 0;

        // 2) Eat any input pushing *into* a wall
        if ((transform.position.x <= leftBound && moveInput < 0) ||
            (transform.position.x >= rightBound && moveInput > 0))
        {
            moveInput = 0;
        }

        // 3) Detect reversal (input opposite of velocity)
        bool isChangingDirection =
            (moveInput != 0) &&
            (Mathf.Sign(moveInput) != Mathf.Sign(currentVelocity)) &&
            (currentVelocity != 0);

        if (isChangingDirection)
        {
            reversing = true;
        }

        // 4) Apply acceleration or deceleration
        if (reversing)
        {
            // Strong decel when reversing
            currentVelocity -= Mathf.Sign(currentVelocity) * deceleration * 2f * Time.deltaTime;
            if (Mathf.Abs(currentVelocity) < 0.1f)
            {
                currentVelocity = 0;
                reversing = false;
            }
        }
        else
        {
            if (moveInput != 0)
            {
                // Regular acceleration
                currentVelocity += moveInput * acceleration * Time.deltaTime;
            }
            else
            {
                // Coast to stop
                currentVelocity -= Mathf.Sign(currentVelocity) * deceleration * Time.deltaTime;
                if (Mathf.Abs(currentVelocity) < 0.1f)
                    currentVelocity = 0;
            }
        }

        // 5) Clamp to max speed
        currentVelocity = Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);

        // 6) Calculate new position & clamp it
        Vector3 newPos = transform.position + Vector3.right * currentVelocity * SPEED_MULTIPLIER * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, leftBound, rightBound);

        // 7) If we ended up at a bound *and* were pushing into it, zero out velocity
        if ((newPos.x <= leftBound && currentVelocity < 0) ||
            (newPos.x >= rightBound && currentVelocity > 0))
        {
            currentVelocity = 0;
        }

        transform.position = newPos;
    }

}
