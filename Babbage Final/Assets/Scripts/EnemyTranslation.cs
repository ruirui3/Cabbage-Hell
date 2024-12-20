using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTranslation : MonoBehaviour
{
    float speed = 4.5f;
    float cameraYBound = -5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed);
        if (transform.position.y < cameraYBound)
        {
            Destroy(gameObject);
        }
        //do a if y reach like -3f or smth destroy() obj)
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
        
        if (other.gameObject.tag == "bullet") {
            
            gopoof();
        }
        if (other.gameObject.tag == "Player")
        {
            
            ManageHealth health = other.gameObject.GetComponentInChildren<ManageHealth>();
            
            if (health != null)
            {
                health.TakeDamage(4f);
                
            }
            else
            {
                Debug.LogError("ManageHealth component is missing on the Player!");
            }



            gopoof();
        }
    }
}
