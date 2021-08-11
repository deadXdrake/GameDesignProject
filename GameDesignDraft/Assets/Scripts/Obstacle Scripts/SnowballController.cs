using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    private float originalX;
    private int moveRight = -1;
    private Vector2 velocity;
    private Vector2 screenBounds;
    private Rigidbody2D snowballBody;
    private SpriteRenderer snowballSprite;
    private Animator snowballAnimator;
    private CircleCollider2D snowballCollider;
    private bool faceRightState = true;
    private bool onFloor = false;

    // Start is called before the first frame update
    void Start()
    {
        snowballBody = GetComponent<Rigidbody2D>();
        snowballSprite = GetComponent<SpriteRenderer>();
        snowballCollider = GetComponent<CircleCollider2D>();
        // get the starting position
        originalX = transform.position.x;
        snowballAnimator = GetComponent<Animator>();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * 2.5f, 0);
    }

    void MoveSnowball()
    {
        ComputeVelocity();
        snowballBody.MovePosition(snowballBody.position + velocity * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Ground")) {
            onFloor = true;
        }

        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Ground")) {
            moveRight *= -1;
            faceRightState = !faceRightState;
            snowballSprite.flipX = faceRightState;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onFloor) {
            MoveSnowball();
            snowballAnimator.SetFloat("speedX", Mathf.Abs(velocity.x));
        }
        
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x - (18 + 5))
        {
        Destroy(this.gameObject);
        }
    }
}
