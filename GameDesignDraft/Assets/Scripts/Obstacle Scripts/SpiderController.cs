using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour, ObstacleInterface
{
  // Start is called before the first frame update
  public GameConstants gameConstants;
  private float originalX;
  private float maxOffset = 5.0f;
  private float enemyPatroltime = 2.0f;
  private int moveRight = -1;
  private Vector2 velocity;

  private Rigidbody2D enemyBody;
  private SpriteRenderer enemySprite;
  void Start()
  {
    enemyBody = GetComponent<Rigidbody2D>();
    enemySprite = GetComponent<SpriteRenderer>();
    // get the starting position
    originalX = transform.position.x;
    ComputeVelocity();
  }

  public void affectPlayer(GameObject player)
  {
    gameConstants.nezukoSpeedX -= 100;   //TODO: Hardcoded time. Put in scriptable constants? HOW TO STOP STACKING OR SET NEZUKO MIN SPEED.
    Debug.Log("Player speed decreased!");
    StartCoroutine(removeEffect(player));
  }

  IEnumerator removeEffect(GameObject player)
  {
    yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
    gameConstants.nezukoSpeedX += 100;
    // Debug.Log("Player speed resumes!");
  }

  void ComputeVelocity()
  {
    velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
  }
  void MoveSpider()
  {
    enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
  }

  // Update is called once per frame
  void Update()
  {
    // change direction
    moveRight *= -1;
    ComputeVelocity();
    MoveSpider();
  }

}
