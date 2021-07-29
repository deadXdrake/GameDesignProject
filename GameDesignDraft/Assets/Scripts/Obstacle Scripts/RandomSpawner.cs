using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform spawnPoint;
    public GameObject[] obstaclePrefabs;
    public GameObject[] treePrefabs;
    public float respawnTime;
    public Transform leftBound;
    public Transform rightBound;
    private CameraController cameraScript;
    public Camera mainCam;
    private bool oneTime = false;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        // Inital spawn
        // GameObject treeObstacle = Instantiate( treePrefab, new Vector3(spawnPoint.position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0), Quaternion.identity);

        // StartCoroutine(obstacleWave());
        cameraScript = mainCam.GetComponent<CameraController>();
    }
    private void Update() {
        float div = (Mathf.Floor(mainCam.transform.position.x) % respawnTime);
        // Debug.Log(Mathf.Floor(mainCam.transform.position.x) % respawnTime);

        if (div == 0) {
            if (!oneTime) {
                if (level == 1) {
                    spawnObstaclesLevel1();
                } else if (level == 2) {
                    spawnObstaclesLevel2();
                }
                
                oneTime = true;
            }
        } else {
            oneTime = false;
        }
    }

    private void spawnObstaclesLevel1() {
        // int numberOfObstacles = Random.Range(1, spawnPoints.Length);

        // Spawn a tree at random position
        int randTree = Random.Range(0, treePrefabs.Length);
        GameObject treeObstacle = Instantiate( treePrefabs[randTree], new Vector3(spawnPoint.position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0), Quaternion.identity);
        // Then spawn others while checking the position of the tree

        // for (int j =  0; j  <  numberOfObstacles; j++) {
        float leftRange;
        if (treeObstacle.transform.position.x - 4.5f > leftBound.transform.position.x) {
            leftRange = Random.Range(leftBound.transform.position.x, treeObstacle.transform.position.x - 4.5f);
        } else {
            leftRange = 0f;
        }

        float rightRange;
        if (treeObstacle.transform.position.x + 3.0f < rightBound.transform.position.x) {
            rightRange = Random.Range(treeObstacle.transform.position.x + 3.0f, rightBound.transform.position.x);
        } else {
            rightRange = 0f;
        }

        int randObstacle = Random.Range(0, obstaclePrefabs.Length);

        if (obstaclePrefabs[randObstacle].name.Equals("Snowball")) {
            Debug.Log("Instantiating snowball!");

            int leftOrRightRandom = Random.Range(0, 1);
            Vector3 randomPosition;
            // Do left right random first
            if (leftRange != 0 && rightRange != 0) {
                if (leftOrRightRandom == 0) {   // Spawn on Left
                    randomPosition = new Vector3(leftRange, -2.83f, 0);
                } else {
                    randomPosition = new Vector3(rightRange, -2.83f, 0);
                }
            } else if (leftRange == 0) {
                randomPosition = new Vector3(rightRange, -2.83f, 0);        //TODO: y-position hardcoded. Add to scriptableObj constants
            } else {
                randomPosition = new Vector3(leftRange, -2.83f, 0);
            }

            GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
            // obstacle.tag = "Spider";
            obstacle.layer = 9;

        }

        if (obstaclePrefabs[randObstacle].name.Equals("Rock")) {
            Debug.Log("Instantiating rock!");
            int leftOrRightRandom = Random.Range(0, 1);
            Vector3 randomPosition;
            // Do left right random first
            if (leftRange != 0 && rightRange != 0) {
                if (leftOrRightRandom == 0) {   // Spawn on Left
                    randomPosition = new Vector3(leftRange, -2.85f, 0);
                } else {
                    randomPosition = new Vector3(rightRange, -2.85f, 0);
                }
            } else if (leftRange == 0) {
                randomPosition = new Vector3(rightRange, -2.85f, 0);        //TODO: y-position hardcoded. Add to scriptableObj constants
            } else {
                randomPosition = new Vector3(leftRange, -2.85f, 0);
            }

            GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
            // obstacle.tag = "Rock";
            obstacle.layer = 9;
        }

        // }
    }

    private void spawnObstaclesLevel2() {
        int randnNumberOfObstacles = Random.Range(1, obstaclePrefabs.Length);

        // Spawn a tree at random position
        int randTree = Random.Range(0, treePrefabs.Length);
        GameObject treeObstacle = Instantiate( treePrefabs[randTree], new Vector3(spawnPoint.position.x + Random.Range(-1.0f, 1.0f), 0.89f, 0), Quaternion.identity);
        // GameObject treeObstacle = Instantiate( treePrefab, new Vector3(prevObstaclePos.x + Random.Range(0.0f, 1.0f), 0.89f, 0), Quaternion.identity);
        // Then spawn others while checking the position of the tree

        for (int j =  0; j  <  randnNumberOfObstacles; j++) {
            
            float leftRange;
            if (treeObstacle.transform.position.x - 4.5f > leftBound.transform.position.x) {
                leftRange = Random.Range(leftBound.transform.position.x, treeObstacle.transform.position.x - 4.5f);
            } else {
                leftRange = 0f;
            }

            float rightRange;
            if (treeObstacle.transform.position.x + 3.0f < rightBound.transform.position.x) {
                rightRange = Random.Range(treeObstacle.transform.position.x + 3.0f, rightBound.transform.position.x);
            } else {
                rightRange = 0f;
            }

            int randObstacle = Random.Range(0, obstaclePrefabs.Length);

            if (obstaclePrefabs[randObstacle].name.Equals("Spider")) {
                Debug.Log("Instantiating spider!");

                float randX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
                Vector3 randomPosition = new Vector3(randX, -3.12f, 0);

                GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
                obstacle.layer = 9;
            }

            if (obstaclePrefabs[randObstacle].name.Equals("Web1") || obstaclePrefabs[randObstacle].name.Equals("Web2") || obstaclePrefabs[randObstacle].name.Equals("Web3")) {
                Debug.Log("Instantiating web!");

                float randX = Random.Range(leftBound.transform.position.x, rightBound.transform.position.x);
                float randY = Random.Range(-3, 4);
                Vector3 randomPosition = new Vector3(randX, randY, 0);

                GameObject obstacle = Instantiate( obstaclePrefabs[randObstacle], randomPosition, Quaternion.identity);
                obstacle.layer = 9;
            }

        }
    }

    IEnumerator obstacleWave() {
        // while( true ) {
            // yield return new WaitForSeconds(2.0f);
            //yield return new WaitForSeconds(Time.deltaTime);
            // float div = (Mathf.Floor(mainCam.transform.position.x) % respawnTime);
            // Debug.Log(Mathf.Floor(mainCam.transform.position.x));
            // yield return new WaitWhile(() => div == 0);
            Debug.Log("Spawning obstacle");
            yield return null;
            // spawnObstacles();
        // }
    }

}
