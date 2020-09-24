using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayCanvas : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI healthText;

    private void OnEnable()
    {
        LevelManager.ScoreUpdate += UpdateScore;
        Player.HealthUpdated += UpdateHealth;
    }

    private void UpdateHealth(int health)
    {
        healthText.text = health.ToString();
    }


    public void UpdateScore(int score)
    {
        if (scoreText)
        {
            scoreText.text = score.ToString();
        }
    }

    private void OnDisable()
    {
        LevelManager.ScoreUpdate -= UpdateScore;
        Player.HealthUpdated -= UpdateHealth;
    }



}
