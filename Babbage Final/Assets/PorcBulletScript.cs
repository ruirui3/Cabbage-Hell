using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcBulletScript : MonoBehaviour
{
    public float speed = 50f;
    public float bottomBound = -5f;
    public AudioClip damageSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        Debug.Log("speed: " + speed);

        if (transform.position.y < bottomBound) {
            goPoof();
            //Debug.Log("gopoof");
        }
    }

    void goPoof() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D Other) {
        if (Other.gameObject.tag == "Player")
        {
            Debug.Log("COLLISION");
            ManageHealth health = Other.gameObject.GetComponentInChildren<ManageHealth>();
            
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
