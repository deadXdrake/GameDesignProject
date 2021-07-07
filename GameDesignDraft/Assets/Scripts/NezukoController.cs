using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NezukoController : MonoBehaviour
{
  public float speed;
  public float maxSpeed = 10; //set the maximum speed
  public float upSpeed;
  private Rigidbody2D nezukoBody;
  private bool onGroundState = true;

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
}
