using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porcupe : MonoBehaviour
{
    public float initSpeed = 2f;
    public float bottomBound = -5f;
    public AudioClip damageSFX;
    public int hp = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * initSpeed);

        if (transform.position.y < bottomBound || hp <= 0)
        {
            goPoof();
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
