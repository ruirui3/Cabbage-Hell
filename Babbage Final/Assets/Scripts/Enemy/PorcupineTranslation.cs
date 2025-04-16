using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcupineTranslation : MonoBehaviour
{   
    public DropBulletScript bulletManager;

    public float initSpeed = 2f;
    public float bottomBound = -5f;
    public float walkBound = 10f;
    public AudioClip damageSFX;
    public int hp = 1;
    public Animator animator;
    public bool porcShooting = false;

    public GameObject PorcBullet;
    public float timer = 0f;
    public float spawnInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DropBulletScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (transform.position.y >= walkBound) {
            transform.Translate(Vector2.down * Time.deltaTime * initSpeed);
        } else {
            animator.SetBool("isShooting", true);
            porcShooting = true;
        }
        
        if (hp <= 0) {
            bulletManager.addBullet(4);
            goPoof();
        } else if (transform.position.y < bottomBound)
        {
            goPoof();
        }

        //Porcupine bullets
        if (porcShooting) {
            if (timer >= spawnInterval) {
                Instantiate(PorcBullet, transform.position, transform.rotation);
                Instantiate(PorcBullet, transform.position, Quaternion.Euler(0, 0, 30));
                Instantiate(PorcBullet, transform.position, Quaternion.Euler(0, 0, -30));
                timer = 0f;
            }
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
