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

    void SaveScore(int score)
    {
    // Save score with a unique key (e.g., "Score1", "Score2", etc.)
    int highScore = PlayerPrefs.GetInt("HighScore", 0);
    
    if (score > highScore)
    {
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
    }
    }

    // Update is called once per frame
    void Update()
    {
        
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
