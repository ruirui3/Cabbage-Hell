using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerTranslation : MonoBehaviour
{
    public float speed = 2.5f;
    public float bottomBound = -5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * Time.deltaTime * speed); 
        if (transform.position.y < bottomBound){
            goPoof();
        }
    }
    void goPoof() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player") {
            
            ManageHealth health = other.gameObject.GetComponentInChildren<ManageHealth>();
            
            if (health != null)
            {
                health.TakeDamage(100f);
                //no play sound
                //AudioSource.PlayClipAtPoint(damageSFX, transform.position);
                
            }
            else
            {
                Debug.LogError("ManageHealth component is missing on the Player!");
            }
            goPoof();
        }
    }
}
