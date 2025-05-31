using UnityEngine;

public class Beehive : MonoBehaviour
{
    public GameObject beePrefab;
    public int hp = 5;
    public float beeSpawnChanceOnHit = 0.5f;
    public float beeSpawnRange = 1.5f;
    public AudioClip buzz;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bullet"))
        {
            if (Random.Range(0, 1.0f) < beeSpawnChanceOnHit) SpawnBee();
            hp--;
        }
    }

    void Update()
    {
        if (hp <= 0)
        {
            SpawnBee();
            SpawnBee();
            Destroy(gameObject);
        }
    }

    void SpawnBee()
    {
        AudioSource.PlayClipAtPoint(buzz, transform.position);
        Instantiate(beePrefab, transform.position + new Vector3(Random.Range(-beeSpawnRange, beeSpawnRange), Random.Range(-beeSpawnRange, beeSpawnRange)), Quaternion.identity);
    }
}
