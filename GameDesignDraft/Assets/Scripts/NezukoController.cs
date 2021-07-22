using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NezukoController : MonoBehaviour
{
  public float speed;
  public float maxSpeed = 10; //set the maximum speed
  public float upSpeed;
  private bool onGroundState = true;
  private bool onShrinkState = false;
  private bool faceRightState = true;

  public Camera cam; // Camera's Transform
  private Vector3 leftCamera;
  private Vector3 middleCam;

  public UnityEvent onPlayerFast;
  public UnityEvent onPlayerDeath;
  public UnityEvent onLevelComplete;

  private Rigidbody2D nezukoBody;
  private SpriteRenderer nezukoSprite;
  private Animator nezukoAnimator;


  // Start is called before the first frame update
  void Start()
  {
    // Application.targetFrameRate = 50;
    nezukoBody = GetComponent<Rigidbody2D>();
    nezukoSprite = GetComponent<SpriteRenderer>();
    nezukoAnimator = GetComponent<Animator>();
    cam = Camera.main;
  }

  void FixedUpdate()
  {
    //apply force to move horizontally when a/d pressed
    float moveHorizontal = Input.GetAxis("Horizontal");
    if (Mathf.Abs(moveHorizontal) > 0)
    {
      Vector2 movement = new Vector2(moveHorizontal, 0);
      if (nezukoBody.velocity.magnitude < maxSpeed)
        nezukoBody.AddForce(movement * speed);
    }

  }

  // Update is called once per frame
  void Update()
  {
    nezukoAnimator.SetFloat("xSpeed", Mathf.Abs(nezukoBody.velocity.x));

    //nezuko jumps when spacebar is presssed and she is on ground
    if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
    {
      print("space pressed");
      nezukoBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
      onGroundState = false;
      nezukoAnimator.SetBool("onGround", onGroundState);
    }

    //stop, set velocity to zero when "a" or "d" is lifted up
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
    {
      nezukoBody.velocity = Vector2.zero;
    }

    //shrink when s is pressed
    if (Input.GetKeyDown("s") && !onShrinkState)
    {
      onShrinkState = true;
      nezukoAnimator.SetBool("onShrink", onShrinkState);
    }

    //return to original size when w is pressed
    if (Input.GetKeyDown("w") && onShrinkState)
    {
      onShrinkState = false;
      nezukoAnimator.SetBool("onShrink", onShrinkState);
    }

    //nezuko face left when a is pressed
    if (Input.GetKeyDown("a") && faceRightState)
    {
      faceRightState = false;
      nezukoSprite.flipX = true; //flip to face left
    }

    //nezuko face right when d is pressed
    if (Input.GetKeyDown("d") && !faceRightState)
    {
      faceRightState = true;
      nezukoSprite.flipX = false; //flip to face right
    }

    //x of the left edge of the camera
    leftCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    //print(leftCameraX); 
    middleCam = Camera.main.ViewportToWorldPoint(new Vector3((float)0.5, (float)0.5, 0));
    //print(middleCam);

    if (leftCamera.x >= this.transform.position.x) //If Nezuko hits or go past the left edge of camera
    {
      onPlayerDeath.Invoke();
    }

    if (this.transform.position.x >= middleCam.x) //If Nezuko is on right 
    {
      onPlayerFast.Invoke();
    }
  }

  //============================================================
  //called when the cube hits the floor, resets ground state to true
  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Ground") && !onGroundState)
    {
      onGroundState = true;
      nezukoAnimator.SetBool("onGround", onGroundState);
    }
  }

  public void PlayerDeathResponse()
  {
    //Death sequence
    nezukoAnimator.SetBool("isGameOver", true);
    transform.position = new Vector3(leftCamera.x + nezukoSprite.bounds.extents.x, transform.position.y, transform.position.z);

    print("died");
    Time.timeScale = 0;
  }


}
