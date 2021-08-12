using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NezukoController : MonoBehaviour
{
  public GameConstants gameConstants;
  public FloatVariable nezukoSpeedX;
  public FloatVariable upSpeed;
  public BoolVariable isNezukoStuck;
  public BoolVariable FinishCountdown;
  public BoolVariable isPaused;
  public Text effectText;
    public Transform TextHolder;

  private float maxSpeed;
  private bool onGroundState = true;
  private bool onShrinkState = false;
  private bool faceRightState = true;
  private bool onFire = false;

  private Vector3 leftCamera;
  private Vector3 middleCam;

  public UnityEvent onPlayerDeath;
  public UnityEvent onLevelComplete;
  public UnityEvent onSpiderCollided;
  public UnityEvent onWebCollided;
  public UnityEvent onFireCollided;
  public UnityEvent onEyeballCollided;

  private Rigidbody2D nezukoBody;
  private SpriteRenderer nezukoSprite;
  private Animator nezukoAnimator;
  private BoxCollider2D nezukoCollider;

  private AudioSource nezukoJump;
  private AudioSource nezukoUnShrink;
  private AudioSource nezukoShrink;
  private AudioSource gameOver;
  private AudioSource gameWin;
  public AudioSource LvlAudio;
  public AudioClip snowball;
    public AudioClip stuck;
    public AudioClip slowed;


    // Start is called before the first frame update
    void Start()
  {
    nezukoSpeedX.SetValue(gameConstants.nezukoSpeedX);
    upSpeed.SetValue(gameConstants.nezukoUpSpeed);
    isNezukoStuck.SetValue(false);
    maxSpeed = gameConstants.nezukoMaxSpeed;
    // upSpeed = gameConstants.nezukoUpSpeed;
    // Application.targetFrameRate = 50;
    nezukoBody = GetComponent<Rigidbody2D>();
    nezukoSprite = GetComponent<SpriteRenderer>();
    nezukoAnimator = GetComponent<Animator>();
    nezukoCollider = GetComponent<BoxCollider2D>();

    AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
    nezukoJump = allMyAudioSources[0];
    nezukoUnShrink = allMyAudioSources[1];
    nezukoShrink = allMyAudioSources[2];
    gameOver = allMyAudioSources[3];
    gameWin = allMyAudioSources[4];
  }

  void FixedUpdate()
  {
    if (FinishCountdown.Value && !isPaused.Value)
    {
      //apply force to move horizontally when a/d pressed
      float moveHorizontal = Input.GetAxis("Horizontal");
      if (Mathf.Abs(moveHorizontal) > 0)
      {
        Vector2 movement = new Vector2(moveHorizontal, 0);
        if (nezukoBody.velocity.magnitude < maxSpeed)
          nezukoBody.AddForce(movement * nezukoSpeedX.Value);
      }
    }


  }

  // Update is called once per frame
  void Update()
  {
        Vector3 effectTextPos = Camera.main.WorldToScreenPoint(TextHolder.position);
        effectText.transform.position = effectTextPos;

        // only start running after the countdown screen and when is not paused
        if (FinishCountdown.Value && !isPaused.Value)
    {
      nezukoAnimator.SetFloat("xSpeed", Mathf.Abs(nezukoBody.velocity.x));
      nezukoCollider.size = nezukoSprite.sprite.bounds.size;
      nezukoBody.gravityScale = 2.75f;

      if (onFire)
      {
        gameObject.transform.Find("Fire").gameObject.transform.localScale = new Vector3(nezukoSprite.sprite.bounds.size.x * 4.2f, nezukoSprite.sprite.bounds.size.y * 2.5f, 0);
      }

      // Debug.Log(isNezukoStuck.Value);
      if (isNezukoStuck.Value)
      {
        Debug.Log("Nezuko is stuck!");
        nezukoBody.velocity = Vector3.zero;
        nezukoBody.gravityScale = 0;
      }

      //nezuko jumps when spacebar is presssed and she is on ground
      if (Input.GetKeyDown(KeyCode.Space) && onGroundState)
      {
        // print("space pressed");
        nezukoBody.AddForce(Vector2.up * upSpeed.Value, ForceMode2D.Impulse);
        onGroundState = false;
        nezukoAnimator.SetBool("onGround", onGroundState);
        nezukoJump.Play();
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
        nezukoShrink.Play();
      }

      //return to original size when w is pressed
      if (Input.GetKeyDown("w") && onShrinkState)
      {
        onShrinkState = false;
        nezukoAnimator.SetBool("onShrink", onShrinkState);
        nezukoUnShrink.Play();
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

    }
  }

  //============================================================
  //called when the cube hits the floor, resets ground state to true
  void OnCollisionEnter2D(Collision2D col)
  {
    // To jump on all obstacles
    if ((col.gameObject.CompareTag("Ground")
    || col.gameObject.CompareTag("Spider")
    || col.gameObject.CompareTag("Rock"))
    || col.gameObject.CompareTag("SpiderWeb")
    || col.gameObject.CompareTag("Fire")
    || col.gameObject.CompareTag("Seat")
    || col.gameObject.CompareTag("Snowball")
    || col.gameObject.CompareTag("Obstacles")
     && !onGroundState)
    {
      onGroundState = true;
      nezukoAnimator.SetBool("onGround", onGroundState);
    }

    if (col.gameObject.CompareTag("Spider"))
    {
      Debug.Log("Collided with spider!");
      onSpiderCollided.Invoke();
      nezukoAnimator.speed = 0.2f;
        effectText.enabled = true;
        effectText.text = "Slowed!!";
        StartCoroutine(removeSlowEffect());
      LvlAudio.PlayOneShot(slowed);
    }

    if (col.gameObject.CompareTag("EdgeLimit"))
    {
      Debug.Log("Collided with endlimit!");
      //Camera.main.transform.Translate(Vector3.right * (Time.deltaTime * (float)2.5));
    }

    if (col.gameObject.CompareTag("Tanjiro"))
    {
      Debug.Log("Successfully met Tanjiro!");
      onLevelComplete.Invoke();
      nezukoBody.bodyType = RigidbodyType2D.Static;
      gameWin.Play();
    }

    if (col.gameObject.CompareTag("SpiderWeb"))
    {
      Debug.Log("Collided with spider web!");
      onWebCollided.Invoke();
      nezukoAnimator.SetBool("isStuck", true);
        effectText.enabled = true; 
        effectText.text = "Stuck!"; 
      StartCoroutine(removeStuckEffect());
      LvlAudio.PlayOneShot(stuck);
      
    }

    if (col.gameObject.CompareTag("Fire"))
    {
      Debug.Log("Colldied with fire!");
      onFireCollided.Invoke();
      gameObject.transform.Find("Fire").gameObject.SetActive(true);
      onFire = true;
      nezukoAnimator.speed = 0.2f;
        effectText.enabled = true;
        effectText.text = "Burnt!";
        StartCoroutine(removeFireEffect());
    }

    if (col.gameObject.CompareTag("Eyeball"))
    {
      Debug.Log("Collided with eyeball!");
      onEyeballCollided.Invoke();
      nezukoAnimator.SetBool("isSleeping", true);
        effectText.enabled = true;
        effectText.text = "Stuck!!";
        StartCoroutine(removeSleepEffect());
    }

    if (col.gameObject.CompareTag("Snowball"))
    {
      Debug.Log("Collided with snowball!");
      LvlAudio.PlayOneShot(snowball);
    }

  }

  IEnumerator removeSlowEffect()
  {
    yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        effectText.enabled = false;
        nezukoAnimator.speed = 1;
  }

  IEnumerator removeFireEffect()
  {
    yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        effectText.enabled = false;
        nezukoAnimator.speed = 1;
    gameObject.transform.Find("Fire").gameObject.SetActive(false);
    onFire = false;
  }

  IEnumerator removeStuckEffect()
  {
    yield return new WaitForSeconds(4.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        effectText.enabled = false;
        nezukoAnimator.SetBool("isStuck", false);
  }

  IEnumerator removeSleepEffect()
  {
    yield return new WaitForSeconds(4.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        effectText.enabled = false;
        nezukoAnimator.SetBool("isSleeping", false);
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
    gameOver.Play();
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
