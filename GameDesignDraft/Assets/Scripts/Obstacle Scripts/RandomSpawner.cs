using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform spawnPoint;
    public GameObject[] obstaclePrefabs;
    public GameObject treePrefab;
    public float respawnTime;
    public Transform leftBound;
    public Transform rightBound;
    private CameraController cameraScript;
    public Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        // Inital spawn
        GameObject treeObstacle = Instantiate( treePrefab, new Vector3(spawnPoint.position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0), Quaternion.identity);

        StartCoroutine(obstacleWave());
        cameraScript = mainCam.GetComponent<CameraController>();
    }

    private void spawnObstacles() {
        // int numberOfObstacles = Random.Range(1, spawnPoints.Length);

        // Spawn a tree at random position
        GameObject treeObstacle = Instantiate( treePrefab, new Vector3(spawnPoint.position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0), Quaternion.identity);
        // Then spawn others while checking the position of the tree

        // for (int j =  0; j  <  numberOfObstacles; j++) {
        float leftRange;
        if (treeObstacle.transform.position.x - 5.0f > leftBound.transform.position.x) {
            leftRange = Random.Range(leftBound.transform.position.x, treeObstacle.transform.position.x - 5.0f);
        } else {
            leftRange = 0f;
        }

        float rightRange;
        if (treeObstacle.transform.position.x + 5.0f < rightBound.transform.position.x) {
            rightRange = Random.Range(treeObstacle.transform.position.x + 5.0f, rightBound.transform.position.x);
        } else {
            rightRange = 0f;
        }

        int randObstacle = Random.Range(0, obstaclePrefabs.Length);

        if (obstaclePrefabs[randObstacle].name.Equals("Spider")) {
            Debug.Log("Instantiating spider!");

            int leftOrRightRandom = Random.Range(0, 1);
            Vector3 randomPosition;
            // Do left right random first
            if (leftRange != 0 && rightRange != 0) {
                if (leftOrRightRandom == 0) {   // Spawn on Left
                    randomPosition = new Vector3(leftRange, -3.12f, 0);
                } else {
                    randomPosition = new Vector3(rightRange, -3.12f, 0);
                }
            } else if (leftRange == 0) {
                randomPosition = new Vector3(rightRange, -3.12f, 0);        //TODO: y-position hardcoded. Add to scriptableObj constants
            } else {
                randomPosition = new Vector3(leftRange, -3.12f, 0);
            }

            GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
            obstacle.tag = "Obstacles";
            obstacle.layer = 9;

        }

        if (obstaclePrefabs[randObstacle].name.Equals("Rock")) {
            Debug.Log("Instantiating rock!");
            int leftOrRightRandom = Random.Range(0, 1);
            Vector3 randomPosition;
            // Do left right random first
            if (leftRange != 0 && rightRange != 0) {
                if (leftOrRightRandom == 0) {   // Spawn on Left
                    randomPosition = new Vector3(leftRange, -2.69f, 0);
                } else {
                    randomPosition = new Vector3(rightRange, -2.69f, 0);
                }
            } else if (leftRange == 0) {
                randomPosition = new Vector3(rightRange, -2.69f, 0);        //TODO: y-position hardcoded. Add to scriptableObj constants
            } else {
                randomPosition = new Vector3(leftRange, -2.69f, 0);
            }

            GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
            obstacle.tag = "Obstacles";
            obstacle.layer = 9;
        }

        // }
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
