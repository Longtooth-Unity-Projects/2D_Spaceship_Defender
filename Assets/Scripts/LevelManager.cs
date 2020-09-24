using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using TMPro;
using System;

public class LevelManager : MonoBehaviour
{
    //events
    public static Action<int> ScoreUpdate;


    //config parameters
    [SerializeField] float sceneLoadDelay = 3.0f;

    public int CurrentScore { get; set; }

    //cached reference
    private SceneLoader sceneLoader;
    private GameManagerSingleton gameManager;

    private void OnEnable()
    {
        //add listeners
        Player.PlayerDestroyed += PlayerDestroyed;
        Enemy.Destroyed += EnemyDestroyed;
    }

    private void Start()
    {
        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        gameManager = GameObject.FindObjectOfType<GameManagerSingleton>();
        CurrentScore = gameManager.GetPlayerScore();
        ScoreUpdate?.Invoke(CurrentScore);
    }

    private void OnDisable()
    {
        //remove listeners
        Player.PlayerDestroyed -= PlayerDestroyed;
        Enemy.Destroyed -= EnemyDestroyed;
        ScoreUpdate?.Invoke(CurrentScore);
    }

    
    public void EnemyDestroyed(int pointValue)
    {
        CurrentScore += pointValue;
        ScoreUpdate?.Invoke(CurrentScore);
    }


    public void PlayerDestroyed()
    {
        StartCoroutine(LoadGameOver());
    }

    private IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        sceneLoader.LoadGameOverScene();
    }

    public void NextLevel()
    {
        sceneLoader.LoadNextScene();
    }
}