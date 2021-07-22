using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;
    public float respawnTime;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(obstacleWave());
    }

    private void spawnObstacles() {
        int numberOfObstacles = Random.Range(1, spawnPoints.Length);

        for (int j =  0; j  <  numberOfObstacles; j++) {

            int randObstacle = Random.Range(0, obstaclePrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject obstacle = Instantiate(obstaclePrefabs[randObstacle]);
            obstacle.tag = "Obstacles";
            obstacle.layer = 9; //Layer is "Obstacles" for collision matrix
            obstacle.transform.position = new Vector3(spawnPoints[randSpawnPoint].position.x + Random.Range(-4.5f, 4.5f), spawnPoints[randSpawnPoint].position.y, 0);
        }
    }

    IEnumerator obstacleWave() {
        while( true ) {
            yield return new WaitForSeconds(respawnTime);
            spawnObstacles();
        }
    }

}
