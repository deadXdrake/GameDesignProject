using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int numberOfObstacles;
    public ObjectType[] objectTypes;
    // Start is called before the first frame update
    void Start()
    {
        // spawn two gombaEnemy
        for (int j =  0; j  <  numberOfObstacles; j++)
            spawnFromPooler(objectTypes[ Random.Range(0, objectTypes.Length) ]);
    }
    void  spawnFromPooler(ObjectType i){
        // static method access
        GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item  !=  null){
            //set position, and other necessary states
            // item.transform.position  =  new  Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.transform.position  =  new  Vector3(item.transform.position.x + Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
            item.SetActive(true);
        }
        else{
            Debug.Log("not enough items in the pool.");
        }
    }
}
