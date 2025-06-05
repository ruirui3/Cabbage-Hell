using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escape : MonoBehaviour
{
    public ScoreTracker players;
    //GameOver thing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Escape key was released");

            if (players.GetScore() > 0) // Optional: only save if meaningful
                ScoreManager.Instance.SaveScore(players.GetScore());

            SceneManager.LoadSceneAsync("Main Menu");
        }
    }

}
