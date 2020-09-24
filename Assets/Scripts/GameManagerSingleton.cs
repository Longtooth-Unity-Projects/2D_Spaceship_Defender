using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    private int _playerScore;



    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        LevelManager.ScoreUpdate += SetPlayerScore;
    }


    // Start is called before the first frame update
    void Start()
    {
        _playerScore = 0;
    }

    private void OnDisable()
    {
        LevelManager.ScoreUpdate -= SetPlayerScore;
    }


    public void ResetGame()
    {
        _playerScore = 0;
    }

    public int GetPlayerScore() { return _playerScore; }
    public void SetPlayerScore(int value) { _playerScore = value; }
}
