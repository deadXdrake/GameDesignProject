using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballController : MonoBehaviour
{
    private float originalX;
    private int moveRight = -1;
    private Vector2 velocity;
    private Vector2 screenBounds;
    private Rigidbody2D enemyBody;
    private SpriteRenderer enemySprite;
    private Animator snowballAnimator;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        // get the starting position
        originalX = transform.position.x;
        ComputeVelocity();
        snowballAnimator = GetComponent<Animator>();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * 2.5f, 0);
    }

    void MoveSnowball()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveSnowball();

        snowballAnimator.SetFloat("speedX", Mathf.Abs(velocity.x));

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x - (18 + 5))
        {
        Destroy(this.gameObject);
        }
    }
}
