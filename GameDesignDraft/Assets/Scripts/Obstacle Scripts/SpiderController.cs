using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour, ObstacleInterface
{
  // Start is called before the first frame update
  // public GameConstants gameConstants;
  public FloatVariable nezukoSpeed;
  private float originalX;
  private float maxOffset = 5.0f;
  private float enemyPatroltime = 2.0f;
  private int moveRight = -1;
  private Vector2 velocity;
  private Vector2 screenBounds;

  private Rigidbody2D enemyBody;
  private SpriteRenderer enemySprite;
  private Animator spiderAnimator;
  void Start()
  {
    enemyBody = GetComponent<Rigidbody2D>();
    enemySprite = GetComponent<SpriteRenderer>();
    // get the starting position
    originalX = transform.position.x;
    ComputeVelocity();
    spiderAnimator = GetComponent<Animator>();
  }

  public void affectPlayer()
  {
    if (nezukoSpeed.Value == 140f)
    {
      Debug.Log(nezukoSpeed.Value);

      nezukoSpeed.ApplyChange(-100f);

      Debug.Log(nezukoSpeed.Value);
      Debug.Log("Player speed decreased!");
      StartCoroutine(removeEffect());
    }
  }

  public void playerLevelComplete()
  {
    velocity = new Vector2(0, 0);
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.CompareTag("Player")) {
      Debug.Log("Direction changed!");
      moveRight *= -1;
      ComputeVelocity();
      MoveSpider();
    }
  }

  IEnumerator removeEffect()
  {
    yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
    if (nezukoSpeed.Value != 0) {
      nezukoSpeed.ApplyChange(100f);
      // nezukoSpeed.ApplyChange(100);
      Debug.Log("Player speed resumes!");
    } 
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
    if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
    {// move gomba
      MoveSpider();
    }
    else
    {
      // change direction
      moveRight *= -1;
      ComputeVelocity();
      MoveSpider();
    }

    spiderAnimator.SetFloat("xSpeed", Mathf.Abs(velocity.x));
    // Debug.Log(Mathf.Abs(velocity.x));

    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    if (transform.position.x < screenBounds.x - (18 + 5))
    {
      Destroy(this.gameObject);
    }
  }

}
