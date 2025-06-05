using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int score)
    {
        List<int> scores = GetHighScores();

        // Only add if it's not already in the list
        // OR only add if it's higher than the lowest score
        if (!scores.Contains(score))
        {
            scores.Add(score);
            scores.Sort((a, b) => b.CompareTo(a)); // Descending

            if (scores.Count > 5)
                scores.RemoveAt(scores.Count - 1);

            for (int i = 0; i < 5; i++)
            {
                if (i < scores.Count)
                    PlayerPrefs.SetInt("Score" + i, scores[i]);
                else
                    PlayerPrefs.DeleteKey("Score" + i);
            }

            PlayerPrefs.Save();
            Debug.Log("Saved new score: " + score);
        }
        else
        {
            Debug.Log("Duplicate score not saved: " + score);
        }
    }


    public List<int> GetHighScores()
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey("Score" + i))
                scores.Add(PlayerPrefs.GetInt("Score" + i));
        }

        return scores;
    }

    // 👇 Press "J" to clear scores
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ClearAllScores();
        }
    }

    public void ClearAllScores()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.DeleteKey("Score" + i);
        }

        PlayerPrefs.Save();
        Debug.Log("Leaderboard reset.");
    }
}
