using UnityEngine;

public class Bee : MonoBehaviour
{
    public float acceleration = 6f;
    public float maxSpeed = 10f;
    public int hp = 1;
    private Rigidbody2D rb;
    private GameObject player;
    public AudioClip damageSFX;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity + direction * acceleration * Time.fixedDeltaTime, maxSpeed);

        if (hp <= 0 || transform.position.y < -Camera.main.orthographicSize - 1f) // screen bottom buffer
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            hp--;
        } 
        if (other.CompareTag("Player"))
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
            Destroy(gameObject);
        }
    }
}
