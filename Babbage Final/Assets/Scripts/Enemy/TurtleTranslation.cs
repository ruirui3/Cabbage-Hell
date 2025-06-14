using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleTranslation : MonoBehaviour
{
    public GameObject moveTo;
    public DropBulletScript bulletManager;

    public float initSpeed = 2f;
    public float rotatingSpeed = 1f;
    public float bottomBound = -5f;
    public float rotationDistance = 4f;
    public bool isRotating = false;
    public Vector2 targetSpeedDir;
    public AudioClip damageSFX;
    public int hp = 2;
    public ManageScore manager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas = GameObject.Find("Canvas");
        moveTo = GameObject.FindWithTag("Player");
        bulletManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DropBulletScript>();
        manager = canvas.transform.Find("Manager").GetComponent<ManageScore>();
    }

    // Update is called once per frame
    void Update()
    {    
        if (isRotating) {
            transform.Translate(targetSpeedDir * rotatingSpeed * Time.deltaTime);
            // transform.Rotate(Vector3.forward * rotationRate);
        } else {
            transform.Translate(Vector2.down * Time.deltaTime * initSpeed);
        }

        if (hp <= 0) {
            bulletManager.addBullet(4);
            manager.AddCombo(5);
            manager.AddScore(300);
            goPoof();
        } else if (transform.position.y < bottomBound)
        {
            goPoof();
        } 
        
        if (Mathf.Abs(transform.position.y - moveTo.transform.position.y) < rotationDistance && isRotating == false) {
            Vector2 targetSpeed = moveTo.transform.position - transform.position;
            targetSpeedDir = targetSpeed / targetSpeed.magnitude; // make unit vector for movement direction
            isRotating = true;
        }
            
        
    }

    void goPoof() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        

        if (other.gameObject.tag == "bullet" || other.CompareTag("CurlBullet")) {
            hp--;
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
            goPoof();
        }
    }

    public void TakeDamage()
    {
        hp--;
    }
}
