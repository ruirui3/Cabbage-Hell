using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public ManageScore manager;
    public float speed;
    private int pierceCount;
    public float maxPierce;
    public AudioClip explosionSfx;
    private int currentBulletType;
    public PlayerController playerController;
    private HashSet<GameObject> hitEnemies;

    private Vector2 curlDirection;
    private float curlSpeed = 3f;
    private float curlDuration = 1.5f;
    private float curlTimer;
    private bool isCurlBullet = false;
    private float curlBulletLifetime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        manager = canvas.transform.Find("Manager").GetComponent<ManageScore>();
        playerController = GameObject.Find("Cabbage Cart").GetComponent<PlayerController>();
        currentBulletType = playerController.GetBulletType();
        
        hitEnemies = new HashSet<GameObject>();

        //Different types of bullets can have different types of attributes?
        if (currentBulletType == 0)
        {
            speed = 5f;
        }
        if (currentBulletType == 1)
        {
            speed = 5f;
        }
        if (currentBulletType == 2)
        {
            speed = 5f;
            maxPierce = 2; //temporary
            pierceCount = 0;
        }
        if (currentBulletType == 3) 
        {
            speed = 5f;
        }
        if (currentBulletType == 4) //curl
        {
            speed = 5f;
            isCurlBullet = true;

        }
        if (currentBulletType == 5)
        {
            speed = 5f;
        }

    }

    public int GetCurrentBulletType()
    {
        return currentBulletType;
    }

    // Update is called once per frame
    void Update()
    {

        currentBulletType = playerController.GetBulletType();
        Debug.Log(currentBulletType); 

        ChecksBoundAndMovement();

    }

    private void ChecksBoundAndMovement()
    {
        
        if (!isCurlBullet) // Normal movement
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (isCurlBullet) // Curl bullet movement
        {
            CommitCurlMovement();
        }

        if (NotInScreen())
        {
            KillBullet();
        }
    }

    private void CommitCurlMovement()
    {
        if (curlTimer > 0)
        {
            curlTimer -= Time.deltaTime;
            transform.position += (Vector3) curlDirection * curlSpeed * Time.deltaTime;
            transform.Rotate(0, 0, 180 * Time.deltaTime); // Spins while moving outward
        }
        else
        {
            isCurlBullet = false;
            speed = 5f; // Resume normal movement
        }
    }

    private bool NotInScreen()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y > 1; //1 is top of the screen
    }

    private void KillBullet()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        KillBullet();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (currentBulletType == 2) //Carrot bullet. Does bullet have piercing?
        {
            CarrotBulletFunction(other);
        } else if (currentBulletType == 4 && gameObject.tag != "CurlBullet")
        {
            SpawnCurlBullets();
            KillBullet();
        } else
        {
            HitEnemy(other);
        }
        
    }

    private void SpawnCurlBullets()
    {
        int numBullets = 6; //can be changed
        int equalAngle = 360 / numBullets;

        for (int i = 0; i < numBullets; i++) 
        {
            int angle = i* equalAngle;
            Vector2 spawnDirection = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            Vector3 spawnPosition = transform.position + (Vector3)spawnDirection * 0.3f;

            GameObject newBullet = Instantiate(gameObject, spawnPosition, Quaternion.identity);
            newBullet.tag = "CurlBullet";
            BulletScript bulletScript = newBullet.GetComponent<BulletScript>();
            bulletScript.SetCurlBullet(spawnDirection);
            Destroy(newBullet, curlBulletLifetime);
        }

    }

    public void SetCurlBullet(Vector2 dir)
    {
        curlDirection = dir;
        curlTimer = curlDuration;
        isCurlBullet = true;
        speed = 0;
    }

    private void HitEnemy(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            manager.AddScore(40); //suggest changing value based on type of enemy
            manager.AddCombo(1);
            KillBullet();
            AudioSource.PlayClipAtPoint(explosionSfx, transform.position);
        }
    }

    private void CarrotBulletFunction(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Add(other.gameObject);
            manager.AddScore(1); //suggest changing value based on type of enemy
            manager.AddCombo(1);
            pierceCount++;

            if (pierceCount >= maxPierce)
            {
                KillBullet();
                AudioSource.PlayClipAtPoint(explosionSfx, transform.position);
            }

        }
    }
}
