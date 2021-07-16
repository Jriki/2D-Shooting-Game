using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float spawnTime = 0.5f;
    [SerializeField] float randomSpawnTime = 0.3f;
    [SerializeField] int nEnemies = 5; //numbers of enemies
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform childWP in pathPrefab.transform)
        {
            waveWayPoints.Add(childWP);
        }
        return waveWayPoints;
    }
    public float GetSpawnTime()
    {
        return spawnTime;
    }
    public float GetRandomSpawnTime()
    {
        return randomSpawnTime;
    }
    public int GetNEnemies()
    {
        return nEnemies;
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

}
