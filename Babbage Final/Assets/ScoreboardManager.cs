using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public class ScoreboardManager : MonoBehaviour
{
    private TMP_Text[] scoreTexts; // Assign in Inspector

    private void OnEnable() {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene load event
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "Scoreboard") {
            GameObject[] textObjects = GameObject.FindGameObjectsWithTag("ScoreText");
            scoreTexts = new TMP_Text[textObjects.Length];
            for (int i = 0; i < textObjects.Length; i++) {
                scoreTexts[i] = textObjects[i].GetComponent<TMP_Text>();
            }


            DisplayHighScores();
        }
    }
    public void DisplayHighScores()
    {
        List<int> scores = ScoreManager.Instance.GetHighScores();
        for (int i = 0; i < scoreTexts.Length; i++) {
            if (i < scores.Count) {
                scoreTexts[i].text = scores[i].ToString(); // Show Score
            }
            else {
                scoreTexts[i].text = "0"; // If no score, show 0
            }
        }
    }
    void Start()
    {
        LoadScores();
    }


    void LoadScores()
    {
        List<int> scores = ScoreManager.Instance.GetHighScores();
        for (int i = 0; i < scoreTexts.Length; i++) {
            scoreTexts[i].text = scores[i].ToString();
        }
    }
}
