using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTranslation : MonoBehaviour
{
    float speed = 4.5f;
    //float cameraYBound = -5f;
    public AudioClip damageSFX;
    public DropBulletScript bulletManager;
    public ManageScore manager;

    bool isMoving;
    bool isInTornado;
    private GameObject tornado;
    private Transform tornadoTransform;
    private PlayerController playerController;
    private BulletScript bulletScript;
    public GameObject bulletPrefab;
    public float bottomBound = -5f;
    //hp
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        manager = canvas.transform.Find("Manager").GetComponent<ManageScore>();
        isMoving = true;
        isInTornado = false;
        playerController = GameObject.Find("Cabbage Cart").GetComponent<PlayerController>();
        bulletManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DropBulletScript>();
        

    }

    

    public void TakeDamage()
    {
        bulletManager.addBullet(2);
        manager.AddScore(100);
        manager.AddCombo(3);
        GoPoof();
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        //transform.Translate(Vector2.down * Time.deltaTime * speed);

        
        if (isInTornado)
        {
            TurnOffMoving();
            transform.position = Vector3.MoveTowards(transform.position, tornadoTransform.position, speed);


        }
        else
        {
            TurnOnMoving();
        }

        if (isMoving)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (transform.position.y < bottomBound)
        {
            GoPoof();
        }
    }

    private bool notInScreen()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y > 1; //1 is top of the screen
    }

    private void GoPoof() {
        
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "CurlBullet") {
            bulletManager.addBullet(2);
            manager.AddScore(100);
            manager.AddCombo(3);
            GoPoof();
        }
        if (other.gameObject.tag == "Player")
        {
            manager.ResetCombo();
            ManageHealth health = other.gameObject.GetComponentInChildren<ManageHealth>();
            
            if (health != null)
            {
                health.TakeDamage(4f);
                AudioSource.PlayClipAtPoint(damageSFX, transform.position);
                
            }
            else
            {
                Debug.LogError("ManageHealth component is missing on the Player!");
            }

            GoPoof();
        }
    }

    


    public void TurnOffMoving()
    {
        isMoving = false;
    }

    public void TurnOnMoving()
    {
        isMoving = true;
    }


}
