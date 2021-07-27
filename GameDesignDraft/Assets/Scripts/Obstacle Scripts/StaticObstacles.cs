using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObstacles : MonoBehaviour
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
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        if (transform.position.x < screenBounds.x - (18 + 5)) {
            Destroy(this.gameObject);
        }
    }
}
