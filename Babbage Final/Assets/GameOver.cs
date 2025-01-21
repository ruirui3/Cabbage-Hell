using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject img;
    public ManageHealth player;

    void Start() {
        img.SetActive(false);
    // Start is called before the first frame update

    }

    void Update(){
        if (player.healthAmount <= 0){
            img.SetActive(true);
        }
    }
}
