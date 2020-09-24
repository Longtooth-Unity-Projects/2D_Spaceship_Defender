using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int startScene = 0;
    [SerializeField] private int gameScene = 1;

    public void LoadStartScene()
    {
        GameObject.FindObjectOfType<GameManagerSingleton>().ResetGame();
        SceneManager.LoadScene(startScene);
    }


    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }


    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            LoadStartScene();
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}//end of class scene loader
