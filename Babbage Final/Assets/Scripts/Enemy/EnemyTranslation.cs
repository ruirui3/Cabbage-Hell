using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTranslation : MonoBehaviour
{
    float speed = 4.5f;
    //float cameraYBound = -5f;
    public AudioClip damageSFX;

    bool isMoving;
    bool isInTornado;
    private GameObject tornado;
    private Transform tornadoTransform;
    private PlayerController playerController;
    private BulletScript bulletScript;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        isMoving = true;
        isInTornado = false;
        playerController = GameObject.Find("Cabbage Cart").GetComponent<PlayerController>();
        
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

        if (notInScreen())
        {
            
            gopoof();
        }
    }

    private bool notInScreen()
    {
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position); //set variable to cameras position
        //^ Viewport position in Unity refers to the coordinates of a point in relation to the camera's viewport
        return viewportPosition.y > 1; //1 is top of the screen
    }

    private void gopoof() {
        // Disable the GameObject - gives an error when playercontroller code tries to respawn it? 
        //gameObject.SetActive(false);
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            gameObject.SetActive(false); // delete the bullet ?
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "bullet" || other.gameObject.tag == "CurlBullet") {
            
            gopoof();
        }
        if (other.gameObject.tag == "Player")
        {
            
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

            gopoof();
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
