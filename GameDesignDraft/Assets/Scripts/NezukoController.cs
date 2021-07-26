using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NezukoController : MonoBehaviour
{
  public GameConstants gameConstants;
  private float speed;
  private float maxSpeed;
  private float upSpeed;
  private bool onGroundState = true;
  private bool onShrinkState = false;
  private bool faceRightState = true;

  private Vector3 leftCamera;
  private Vector3 middleCam;

  public UnityEvent onPlayerFast;
  public UnityEvent onPlayerDeath;
  public UnityEvent onLevelComplete;
  public UnityEvent onObstaclesCollided;

  private Rigidbody2D nezukoBody;
  private SpriteRenderer nezukoSprite;
  private Animator nezukoAnimator;
  private BoxCollider2D nezukoCollider;


  // Start is called before the first frame update
  void Start()
  {
    speed = gameConstants.nezukoSpeedX;
    maxSpeed = gameConstants.nezukoMaxSpeed;
    upSpeed = gameConstants.nezukoUpSpeed;
    // Application.targetFrameRate = 50;
    nezukoBody = GetComponent<Rigidbody2D>();
    nezukoSprite = GetComponent<SpriteRenderer>();
    nezukoAnimator = GetComponent<Animator>();
    nezukoCollider = GetComponent<BoxCollider2D>();
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

      nezukoCollider.size = nezukoSprite.sprite.bounds.size;
    }

  }

  // Update is called once per frame
  void Update()
  {
    nezukoAnimator.SetFloat("xSpeed", Mathf.Abs(nezukoBody.velocity.x));

    //nezuko jumps when spacebar is presssed and she is on ground
    if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
    {
      // print("space pressed");
      nezukoCollider.size = nezukoSprite.sprite.bounds.size;
      nezukoBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
      onGroundState = false;
      nezukoAnimator.SetBool("onGround", onGroundState);
    }

    //stop, set velocity to zero when "a" or "d" is lifted up
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
    {
      nezukoCollider.size = nezukoSprite.sprite.bounds.size;
      nezukoBody.velocity = Vector2.zero;
    }

    //shrink when s is pressed
    if (Input.GetKeyDown("s") && !onShrinkState)
    {
      onShrinkState = true;
      nezukoAnimator.SetBool("onShrink", onShrinkState);
      nezukoCollider.size = new Vector2(nezukoSprite.sprite.bounds.size.x,nezukoSprite.sprite.bounds.size.y-0.25f);
    }

    //return to original size when w is pressed
    if (Input.GetKeyDown("w") && onShrinkState)
    {
      onShrinkState = false;
      nezukoAnimator.SetBool("onShrink", onShrinkState);
      nezukoCollider.size = nezukoSprite.sprite.bounds.size;
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
    if ((col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles")) && !onGroundState)
    {
      onGroundState = true;
      nezukoAnimator.SetBool("onGround", onGroundState);
    }

    if (col.gameObject.CompareTag("Enemy"))
    {
      // Debug.Log("Collided with enemy!");
      onObstaclesCollided.Invoke();
    }

    if (col.gameObject.CompareTag("EdgeLimit"))
    {
      Debug.Log("Collided with endlimit!");
      //Camera.main.transform.Translate(Vector3.right * (Time.deltaTime * (float)2.5));
    }
  }

  public float getNezukoSpeed()
  {
    Debug.Log(nezukoBody.velocity.magnitude);
    return nezukoBody.velocity.magnitude;
  }

  public void PlayerDeathResponse()
  {
    //Death sequence
    nezukoAnimator.SetBool("isGameOver", true);
    transform.position = new Vector3(leftCamera.x + nezukoSprite.bounds.extents.x, transform.position.y, transform.position.z);

    Debug.Log("Nezuko died!");
    //Time.timeScale = 0;
    //GetComponent<Animator>().SetBool("playerIsDead", true); //play playerdead animation

    // GetComponent<Collider2D>().enabled = false;
    //nezukoBody.AddForce(Vector2.up, ForceMode2D.Impulse); //how to stop adding force after awhile?
    //nezukoBody.gravityScale = 10;

    //Game over, click, Transition to the menu,
    //Audio of nezuko dying
    //Sprite of nezuko dying with animator transition, go back into box

    //this.enabled = false;
  }

}
