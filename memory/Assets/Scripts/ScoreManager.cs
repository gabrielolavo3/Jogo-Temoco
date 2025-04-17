using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreTotalText;
    public Text scoreCorrectText;
    public Text scoreErrorText;

    private int scoreTotal = 0;
    private int scoreCorrect = 0;
    private int scoreErrors = 0;

    public void AddCorrect()
    {
        scoreCorrect++;
        scoreTotal += 100;
        UpdateUI();
    }

    public void AddError()
    {
        scoreErrors++;
        scoreTotal -= 50;
        if (scoreTotal < 0) scoreTotal = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreTotalText.text = scoreTotal.ToString();
        scoreCorrectText.text = scoreCorrect.ToString();
        scoreErrorText.text = scoreErrors.ToString();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("LastScoreTotal", scoreTotal);
        PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        scoreTotal = 0;
        scoreCorrect = 0;
        scoreErrors = 0;
        UpdateUI();
    }
}
