using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText)
        {
            scoreText.text = GameObject.FindObjectOfType<GameManagerSingleton>().PlayerScore.ToString();
        }
    }
}
