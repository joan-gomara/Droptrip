using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    //List<GameObject> obstacleList = new List<GameObject>();

    public float spawnTime = 1f;
    private float timeUntilSpawn;

    private void Start()
    {
        timeUntilSpawn = Random.Range(0f, spawnTime);
    }

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilSpawn += Time.deltaTime;

        if (timeUntilSpawn >= spawnTime)
        {
            Spawn();
            timeUntilSpawn = 0f;
        }

        //foreach (GameObject obstacle in obstacleList)
        //{
        //    if (obstacle.transform.position.y < -20f)
        //    {
        //        //obstacleList.Remove(obstacle);
        //        Destroy(obstacle);
        //    }
        //}

        //for (int i = obstacleList.Count - 1; i >= 0; i--)
        //{
        //    if (obstacleList[i].transform.position.y < -20f)
        //        obstacleList.Remove(obstacleList[i]);
        //        //Destroy(obstacleList[i]);
        //}

    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        //GameObject spawnedObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);
        //obstacleList.Add(spawnedObstacle);
        //print(obstacleList.Count);

    }

}
