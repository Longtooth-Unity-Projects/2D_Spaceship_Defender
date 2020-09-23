using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCanvas : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;

    private void OnEnable()
    {
        LevelManager.ScoreUpdate += UpdateScore;
    }
    private void OnDisable()
    {
        LevelManager.ScoreUpdate -= UpdateScore;
    }

    public void UpdateScore(int score)
    {
        if (scoreText)
        {
            scoreText.text = score.ToString();
        }
    }


}
