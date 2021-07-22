using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Vector2 screenBounds;
    // public ObjectType objectType;

    // Start is called before the first frame update
    void Start()
    {  

    }

    // Update is called once per frame
    void Update()
    {
        // if (transform.position.x < screenBounds.x * 2) {
        //     // Set active
        //     spawnFromPooler(objectType);
        // }
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        // Debug.Log(screenBounds.x);
        // Debug.Log(Screen.width);

        if (transform.position.x < screenBounds.x - (18 + 5)) {
            Destroy(this.gameObject);
        }
    }

    // void  spawnFromPooler(ObjectType i){
    //     // static method access
    //     GameObject item =  ObjectPooler.SharedInstance.GetPooledObject(i);
    //     if (item  !=  null){
    //         //set position, and other necessary states
    //         // item.transform.position  =  new  Vector3(Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
    //         item.transform.position  =  new  Vector3(item.transform.position.x + Random.Range(-4.5f, 4.5f), item.transform.position.y, 0);
    //         item.SetActive(true);
    //     }
    //     else{
    //         Debug.Log("not enough items in the pool.");
    //     }
    // }
}
