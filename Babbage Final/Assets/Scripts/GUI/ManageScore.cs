using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ManageScore : MonoBehaviour
{

    public TMP_Text scoreText;
    public TMP_Text comboText;

    private int score = 0;

    private int combo = 0;

    private Vector3 comboOriginalScale;


    // Start is called before the first frame update
    void Start()
    {
        
        scoreText.text = score.ToString("D8");
        comboText.text = "x" + combo.ToString();
        comboOriginalScale = comboText.rectTransform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AddScore(4);
        }

        if (Input.GetKeyDown(KeyCode.M)) // Optional miss simulation
        {
            ResetCombo();
            UpdateUI();
        }

       

    }

    public void AddScore(int addedScore)
    {

        float multiplier = combo < 2 ? 1f : combo / 2f;



        score +=  Mathf.RoundToInt(addedScore * multiplier);
        UpdateUI();

    }

    public void UpdateUI()
    {
        scoreText.text = score.ToString("D8");
        comboText.text = "x" + combo.ToString();


    }

    public void RemoveScore(int removedScore)
    {
        score = Mathf.Max(0, score - removedScore);
        UpdateUI();

    }

    public void AddCombo(int addedCombo)
    {
        combo += addedCombo;
        UpdateUI();
        StartCoroutine(AnimateComboPop());
    }

    public void ResetCombo()
    {
        combo = 0;
        UpdateUI();
    }

    private IEnumerator AnimateComboPop()
    {
        RectTransform comboRect = comboText.rectTransform;
        Vector3 targetScale = comboOriginalScale * 1.15f;
        float t = 0f;
        float duration = 0.1f;

        // Scale up
        while (t < duration)
        {
            t += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(comboOriginalScale, targetScale, t / duration);
            yield return null;
        }

        // Scale back
        t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            comboRect.localScale = Vector3.Lerp(targetScale, comboOriginalScale, t / duration);
            yield return null;
        }

        comboRect.localScale = comboOriginalScale;
    }



}
