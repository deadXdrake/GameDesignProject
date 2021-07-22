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
        //float desiredX = player.position.x + offset;
        float desiredX = Time.time + offset;
        //print(Time.time);
        // check if desiredX is within startX and endX
        if (desiredX > startX && desiredX < endX)
        {
            //print("if condition");
            //this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
            transform.Translate(Vector3.right * Time.deltaTime);
        }
    }

    public void PlayerDeathResponse()
    {
        //transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        this.enabled = false; //Stops camera movement
        //Debug.Log("camera called");
    }

    public void PlayerFastResponse()
    {
    //Fast sequence
    //print("onplayerfast");
        float nezukoSpeed = Mathf.Abs(nezukoBody.velocity[0]);
        if (nezukoSpeed >= 4 && nezukoSpeed < 5)
        {
            Debug.Log("1");
            float fastX = (Time.time + offset) * (float)1.5;
            transform.Translate(Vector3.right * (Time.deltaTime*(float)1.5));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }

        if (nezukoSpeed >= 5 && nezukoSpeed < 6)
        {
            Debug.Log("2");
            float fastX = (Time.time + offset) * (float)1.8;
            transform.Translate(Vector3.right * (Time.deltaTime * (float)2));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }

        if (nezukoSpeed >= 6)
        {
            Debug.Log("3");
            float fastX = (Time.time + offset) * (float)2;
            transform.Translate(Vector3.right * (Time.deltaTime * (float)2.5));
            //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
        }
        

        //float fastX = (Time.time + offset) * (float)1.5; 
            //(Mathf.Abs(nezukoBody.velocity[0]) / 10);
        //when nezuko is on right, if nezuko speed in a certain range, move camera in certain speeed.
   // print(Mathf.Abs(nezukoBody.velocity[0]));
    //this.transform.position = Vector3.Slerp(this.transform.position, player.position, (float)0.5);
    //this.transform.position = new Vector3(fastX, this.transform.position.y, this.transform.position.z);
    //transform.Translate(new Vector3((float)1.5, 0, 0) * Time.deltaTime); //1.5x camera speed
    }
}
