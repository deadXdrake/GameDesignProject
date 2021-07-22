using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NezukoController : MonoBehaviour
{
  public float speed;
  public float maxSpeed = 10; //set the maximum speed
  public float upSpeed;
  private Rigidbody2D nezukoBody;
  private bool onGroundState = true;
  public UnityEvent onPlayerFast;
  public UnityEvent onPlayerDeath;
  public UnityEvent onLevelComplete;

  private SpriteRenderer nezukoSprite;

  // Start is called before the first frame update
  void Start()
  {
    nezukoBody = GetComponent<Rigidbody2D>();
    nezukoSprite = GetComponent<SpriteRenderer>();
   
  }

  void FixedUpdate()
  {

  }

  // Update is called once per frame
  void Update()
  {
    //dynamic rigidbody
    float moveHorizontal = Input.GetAxis("Horizontal");
    if (Mathf.Abs(moveHorizontal) > 0)
    {
      Vector2 movement = new Vector2(moveHorizontal, 0);
      if (nezukoBody.velocity.magnitude < maxSpeed)
        nezukoBody.AddForce(movement * speed);
    }

    //stop, set velocity to zero when "a" or "d" is lifted up
    if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
    {
      nezukoBody.velocity = Vector2.zero;
    }

    //nezuko jumps when spacebar is presssed and she is on ground
    if (Input.GetKeyDown("space") && onGroundState)
    {
      nezukoBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
      onGroundState = false;
    }

    if (Input.GetKeyDown("s") && onGroundState)
    {

    }

    //x of the left edge of the camera
    Vector3 leftCamera = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
    //print(leftCameraX); 
    Vector3 middleCam = Camera.main.ViewportToWorldPoint(new Vector3((float)0.5, (float)0.5, 0));
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
    if (col.gameObject.CompareTag("Ground"))
    {
      onGroundState = true;
    }
  }

    public void PlayerDeathResponse()
    {
        //Death sequence
        
        Debug.Log("Nezuko died!");
        //Time.timeScale = 0;
        //GetComponent<Animator>().SetBool("playerIsDead", true); //play playerdead animation

        GetComponent<Collider2D>().enabled = false;
        //nezukoBody.AddForce(Vector2.up, ForceMode2D.Impulse); //how to stop adding force after awhile?
        //nezukoBody.gravityScale = 10;

        //Game over, click, Transition to the menu, 
        //Audio of nezuko dying
        //Sprite of nezuko dying with animator transition, go back into box

        //this.enabled = false;
    }

    
}
