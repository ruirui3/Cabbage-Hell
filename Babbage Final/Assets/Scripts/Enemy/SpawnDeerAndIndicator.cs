using UnityEngine;

public class SpawnDeerAndIndicator : MonoBehaviour
{
    public GameObject deer;
    public GameObject indicator;
    private float startTime;
    private float randX;
    private bool deerSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        randX = Random.Range(-2.0f, 2.0f);
        Destroy(Instantiate(indicator, new Vector2(randX, 0), transform.rotation), 1);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!deerSpawned && Time.time - startTime > 1.0f)
        {
            Instantiate(deer, new Vector2(randX, 4.85f), transform.rotation);
            deerSpawned = true;
        }
    }
}
