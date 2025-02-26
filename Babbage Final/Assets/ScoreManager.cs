using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instances;

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

    public void SaveScore(int newScore)
    {
        List<int> scores = new List<int>();

        // Load existing scores
        for (int i = 0; i < 5; i++)
        {
            scores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }

        // Add new score, sort in descending order, and keep top 5
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a));
        scores = scores.Take(5).ToList(); // Keep only the top 5

        // Save back to PlayerPrefs
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        }

        PlayerPrefs.Save();
    }
}
