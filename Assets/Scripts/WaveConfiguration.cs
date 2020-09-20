using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfiguration : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numOfEnemies = 5;
    [SerializeField] private float moveSpeed = 2f;

    public GameObject EnemyPrefab { get => enemyPrefab; }
    public GameObject PathPrefab { get => pathPrefab; }
    public float TimeBetweenSpawns { get => timeBetweenSpawns; }
    public float SpawnRandomFactor { get => spawnRandomFactor; }
    public int NumOfEnemies { get => numOfEnemies; }
    public float MoveSpeed { get => moveSpeed; }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        
        foreach (Transform waypoint in pathPrefab.transform)
        {
            waypoints.Add(waypoint);
        }

        return waypoints;
    }

}
