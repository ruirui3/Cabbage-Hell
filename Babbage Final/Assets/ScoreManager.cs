using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep it across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SaveScore(int score){
        List<int> scores = GetHighScores(); //creates list with 0 as all high score or past scores on list
        scores.Add(score); //add in the score
        scores.Sort((a, b) => b.CompareTo(a));

        if (scores.Count > 5) {
            scores.RemoveAt(5);
        }

        for (int i = 0; i < scores.Count; i++) {
            PlayerPrefs.SetInt("Score" + i, scores[i]);
            Debug.Log("Saving Score " + i + ": " + scores[i]);
        }

        PlayerPrefs.Save(); 
        Debug.Log("Scores Saved!");
    }  

    public List<int> GetHighScores() {   
        List<int> scores = new List<int>();

        for (int i = 0; i < 5; i++) {
            int score = PlayerPrefs.GetInt("Score" + i, 0); // Default 0 if no score found
            scores.Add(score);

        }
        return scores;
    }

}
