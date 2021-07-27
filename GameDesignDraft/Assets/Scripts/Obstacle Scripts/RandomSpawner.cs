using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] obstaclePrefabs;
    public float respawnTime;
    private Vector2 screenBounds;
    //public float cameraMovement;
    //public float dist;
    private CameraController cameraScript;
    public Camera mainCam;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(obstacleWave());
        cameraScript = mainCam.GetComponent<CameraController>();
    }

    private void spawnObstacles() {
        int numberOfObstacles = Random.Range(1, spawnPoints.Length);

        for (int j =  0; j  <  numberOfObstacles; j++) {

            int randObstacle = Random.Range(0, obstaclePrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject obstacle = Instantiate(obstaclePrefabs[randObstacle]);
            obstacle.tag = "Obstacles";
            obstacle.layer = 9;
            if (obstaclePrefabs[randObstacle].name.Equals("Spider")) {
                Debug.Log("Instantiating spider!");
                //TODO: y-position hardcoded. Add to scriptableObj constants
                obstacle.transform.position = new Vector3(spawnPoints[randSpawnPoint].position.x + Random.Range(-1.0f, 1.0f), -3.12f, 0);
            }

            if (obstaclePrefabs[randObstacle].name.Equals("Rock")) {
                Debug.Log("Instantiating rock!");
                obstacle.transform.position = new Vector3(spawnPoints[randSpawnPoint].position.x + Random.Range(-1.0f, 1.0f), -2.69f, 0);
            }

            if (obstaclePrefabs[randObstacle].name.Equals("TreeSnow")) {
                Debug.Log("Instantiating TreeSnow!");
                obstacle.transform.position = new Vector3(spawnPoints[randSpawnPoint].position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0);
            }

        }
    }

    IEnumerator obstacleWave() {
        while( true ) {
            yield return new WaitForSeconds(respawnTime);
            //yield return new WaitForSeconds(Time.deltaTime);
            float div = (Mathf.Floor(cameraScript.cameraPosition.x % respawnTime));
            Debug.Log(div);
            //yield return new WaitWhile(() => div == 0);
            spawnObstacles();
        }
    }

}
