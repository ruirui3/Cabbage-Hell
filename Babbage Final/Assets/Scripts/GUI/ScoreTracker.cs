using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{

    public int scoreTrack;

    // Start is called before the first frame update
    void Start()
    {
        scoreTrack = 0;

    }

    // Update is called once per frame
    void Update(){
        
    }

    public void AddScore(int score)
    {
        scoreTrack += score;
    }

    public void RemoveScore(int score)
    {
        if (scoreTrack-score >= 0)
        {
            scoreTrack -= score;
        } else
        {
            scoreTrack = 0;
        }
    }

    public int GetScore()
    {
        return scoreTrack;
    }


}
