using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public ManageScore manager;
    public float speed;
    public bool isMoving = true;
    private int pierceCount = 0;
    public float maxPierce;
    public AudioClip explosionSfx;
    private int currentBulletType;
    public PlayerController playerController;
    private HashSet<GameObject> hitEnemies;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        manager = canvas.transform.Find("Score").GetComponent<ManageScore>();
        playerController = GameObject.Find("Cabbage Cart").GetComponent<PlayerController>();
        currentBulletType = playerController.GetBulletType();
        maxPierce = 2; //temporary
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
        }
        if (currentBulletType == 3)
        {
            speed = 5f;
        }
        if (currentBulletType == 4)
        {
            speed = 5f;
        }
        if (currentBulletType == 5)
        {
            speed = 5f;
        }

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
        if (isMoving)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (NotInScreen())
        {
            isMoving = false;
            KillBullet();
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
        // Disable the GameObject - gives an error when playercontroller code tries to respawn it? 
        //gameObject.SetActive(false);


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
        } else
        {
            HitEnemy(other);
        }

    }

    private void HitEnemy(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            manager.AddScore(2); //suggest changing value based on type of enemy
            KillBullet();
            AudioSource.PlayClipAtPoint(explosionSfx, transform.position);
        }
    }

    private void CarrotBulletFunction(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && !hitEnemies.Contains(other.gameObject))
        {
            hitEnemies.Add(other.gameObject);
            manager.AddScore(2); //suggest changing value based on type of enemy
            pierceCount++;

            if (pierceCount >= maxPierce)
            {
                KillBullet();
                AudioSource.PlayClipAtPoint(explosionSfx, transform.position);
            }

        }
    }
}
