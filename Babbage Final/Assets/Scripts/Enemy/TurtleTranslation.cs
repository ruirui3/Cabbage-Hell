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

    // Start is called before the first frame update
    void Start()
    {
        moveTo = GameObject.FindWithTag("Player");
        bulletManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DropBulletScript>();
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
            bulletManager.addBullet(1);
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
        
        if (other.gameObject.tag == "bullet") {
            
            hp--;
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
            goPoof();
        }
    }
}
