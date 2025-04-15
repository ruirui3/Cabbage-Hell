using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBulletScript : MonoBehaviour
{   
    PlayerController shooter;
    float[] bulletChance = {0f, 0.2f, 0.1f, 0f, 0.1f, 0f}; // normal (0) triple (1) carrot (2) honey (3) curl (4) tornado (5)
    int[] bulletStackCount = {0, 5, 10, 5, 10, 5};

    // Start is called before the first frame update
    void Start()
    {
        shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void addBullet(int type) {
        if (Random.value < bulletChance[type]) {
            shooter.addBullet(type, bulletStackCount[type]);
            Debug.Log("Type " + type + " bullet dropped");
        }
    }
}
