using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject img;
    public ManageHealth player;
    public ScoreTracker players;

    void Start() {
        img.SetActive(false);
    // Start is called before the first frame update

    }
    // not working implementation of escape to end screen 
    //public void End(){
    //    img.SetActive(true);
    //}

    void Update(){
        if (player.healthAmount <= 0){
            img.SetActive(true);
            //save the score tracked at the end of the game
            ScoreManager.Instance.SaveScore(players.GetScore());

        }


    }

}
