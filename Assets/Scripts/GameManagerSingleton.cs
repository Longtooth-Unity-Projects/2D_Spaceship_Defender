using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    public int PlayerScore { get; set; }



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
        //register listeners
    }

    private void OnDisable()
    {
        //deregister listeners
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
