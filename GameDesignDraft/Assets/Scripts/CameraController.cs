using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform player; // Nezuko's Transform
  public Rigidbody2D nezukoBody;
  public Transform endLimit; // GameObject that indicates end of map
  public Transform endofScreen; // GameObject that indicates end of map
  private float endX; // largest x-coordinate of the camera
  private float viewportHalfWidth;
  public GameConstants gameConstants;
  private AudioSource audioSource;
  public BoolVariable FinishCountdown;
  public BoolVariable isPaused;

  public Vector3 cameraPosition;
  // Start is called before the first frame update
  void Start()
  {
    // get coordinate of the bottomleft of the viewport
    // z doesn't matter since the camera is orthographic

    Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
    endX = endLimit.transform.position.x - viewportHalfWidth;

    //this.transform.position = new Vector3


  }

  // Update is called once per frame
  void Update()
  {
    if (FinishCountdown.Value && !isPaused.Value)
    {
      //Right edge collider (EndLimit) moves with camera
      Vector3 rightCam = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0, 0));
      endLimit.position = rightCam;

      //Debug.Log(nezukoBody.transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);

      //this.transform.position = new Vector3(desiredX, this.transform.position.y, this.transform.position.z);
      // check if desiredX is within startX and endX
      float nezukoPos = (nezukoBody.transform.position.x - Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);
      if (nezukoPos < 10)
      {
        //Debug.Log("1st");
        transform.Translate(Vector3.right * (Time.deltaTime * 1.5f));
      }

      if (nezukoPos >= 10 && nezukoPos < 13.5)
      {
        //Debug.Log("2nd");
        transform.Translate(Vector3.right * (Time.deltaTime * 4.0f));
      }

      if (nezukoPos > 13.5)
      {
        //Debug.Log("3rd");
        transform.Translate(Vector3.right * (Time.deltaTime * 6.0f));
      }

      if (endofScreen.transform.position.x <= rightCam.x) //Tanjiro seen on screen
      {
        Debug.Log("End of Map");
        this.enabled = false;   // Stops camera movement

      }

      //cameraPosition = this.transform.position;

      Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
      viewportHalfWidth = Mathf.Abs(bottomLeft.x - this.transform.position.x);
      endX = endLimit.transform.position.x - viewportHalfWidth;
      audioSource = GetComponent<AudioSource>();
      //this.transform.position = new Vector3
    }

  }

  public void PlayerDeathResponse()
  {
    //transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    this.enabled = false;   // Stops camera movement
    audioSource.Stop();
  }

  public void PlayerWinResponse()
  {
    this.enabled = false;   // Stops camera movement
    audioSource.Stop();

  }


}
