using UnityEngine;

public class HoneyBulletScript : MonoBehaviour
{
    public float speed = 10f;
    public float explosionRadius = 10f;
    public float slowDuration = 3f;
    public float slowFactor = 0.5f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Explode();
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyTranslation enemy = hit.GetComponent<EnemyTranslation>();
                Bee bee = hit.GetComponent<Bee>();
                Beehive beehive = hit.GetComponent<Beehive>();
                PorcupineTranslation porcupine = hit.GetComponent<PorcupineTranslation>();
                TurtleTranslation turtle = hit.GetComponent<TurtleTranslation>();
                
                if (enemy != null)
                {
                    enemy.TakeDamage(); // optional if not already destroyed
                    
                }

                if (bee != null)
                {
                    bee.TakeDamage();
                }

                if (beehive != null)
                {
                    beehive.TakeDamage();
                }
                if (porcupine != null)
                {
                    porcupine.TakeDamage();
                }
                if (turtle != null)
                {
                    turtle.TakeDamage();
                }

                
               
                
                
            }
        }

        // Optional: Add explosion effect (VFX prefab here)
        Debug.Log("Honey Bullet exploded and applied slow to enemies.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
