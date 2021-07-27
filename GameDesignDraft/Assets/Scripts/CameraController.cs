using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Nezuko's Transform
    public Rigidbody2D nezukoBody;
    public Transform endLimit; // GameObject that indicates end of map
    public Transform startLimit; // GameObject that indicates end of map
    private float offset; // initial x-offset between camera and Mario
    private float startX; // smallest x-coordinate of the Camera
    private float endX; // largest x-coordinate of the camera
    private float viewportHalfWidth;
    // Start is called before the first frame update
    void Start()
    {

        // get coordinate of the bottomleft of the viewport
        // z doesn't matter since the camera is orthographic
    

        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);

        offset = this.transform.position.x - player.position.x;
        startX = startLimit.transform.position.x + viewportHalfWidth;
        endX = endLimit.transform.position.x - viewportHalfWidth;
        //print(startX);
        //this.transform.position = new Vector3


    }

    // Update is called once per frame
    void Update()
    {
        //Right edge collider (EndLimit) moves with camera
        Vector3 rightCam = Camera.main.ViewportToWorldPoint(new Vector3((float)1, 0, 0));
        endLimit.position = rightCam;
        
        //float desiredX = Time.time + offset;
        //this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
        // check if desiredX is within startX and endX
        transform.Translate(Vector3.right * Time.deltaTime);
        
    }

    public void PlayerDeathResponse()
    {
        //transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.enabled = false;   // Stops camera movement
    }

    public void PlayerWinResponse() {
        this.enabled = false;   // Stops camera movement
    }

    public void PlayerFastResponse()
    {
    //Fast sequence
        float nezukoSpeed = Mathf.Abs(nezukoBody.velocity[0]);
        if (nezukoSpeed >= 4 && nezukoSpeed < 5)
        {
            //Debug.Log("1");
            float fastX = (Time.time + offset) * (float)1.5;
            transform.Translate(Vector3.right * (Time.deltaTime*(float)1.5));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }

        if (nezukoSpeed >= 5 && nezukoSpeed < 6)
        {
            //Debug.Log("2");
            float fastX = (Time.time + offset) * (float)1.8;
            transform.Translate(Vector3.right * (Time.deltaTime * (float)2));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }

        if (nezukoSpeed >= 6)
        {
            //Debug.Log("3");
            float fastX = (Time.time + offset) * (float)2;
            transform.Translate(Vector3.right * (Time.deltaTime * (float)2.5));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }
        
    }
}
