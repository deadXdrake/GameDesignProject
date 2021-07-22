using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    obstacle1 = 0,
    obstacle2 = 1,
    obstacle3 = 2
}

[System.Serializable]
public class ObjectPoolItem
{
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}

public class ExistingPoolItem
{
    public GameObject gameObject;
    public ObjectType type;

    // constructor
    public ExistingPoolItem(GameObject gameObject, ObjectType type)
    {
        // reference input
        this.gameObject = gameObject;
        this.type = type;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<ObjectPoolItem> itemsToPool; // types of different object to pool
    public List<ExistingPoolItem> pooledObjects; // a list of all objects in the pool, of all types
    public static ObjectPooler SharedInstance;

    void Awake() {
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        foreach (ObjectPoolItem item in itemsToPool) {
            for (int i = 0; i < item.amount; i++)
                {
                    // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                    int randSpawnPoint = Random.Range(0, spawnPoints.Length);

                    GameObject pickup = (GameObject)Instantiate(item.prefab, spawnPoints[randSpawnPoint].position, transform.rotation);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    ExistingPoolItem e = new ExistingPoolItem(pickup, item.type);
                    pooledObjects.Add(e);
                }
        }
    }

    public GameObject GetPooledObject(ObjectType type)
    {
        // return inactive pooled object if it matches the type
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type) {
                return pooledObjects[i].gameObject;
            }
        }

        return null;
    }
}
