using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballController : MonoBehaviour, ObstacleInterface
{
    private Rigidbody2D eyeballBody;
    private SpriteRenderer eyeballSprite;
    private CircleCollider2D eyeballCollder;
    private Animator eyeballAnimator;
    public FloatVariable nezukoSpeed;
    public BoolVariable isNezukoStuck;
    private bool eyeballEnabled = true;
    private Vector2 screenBounds;
    
    // Start is called before the first frame update
    void Start()
    {
        eyeballBody = GetComponent<Rigidbody2D>();
        eyeballSprite = GetComponent<SpriteRenderer>();
        eyeballAnimator = GetComponent<Animator>();
        eyeballCollder = GetComponent<CircleCollider2D>();
        eyeballCollder.enabled = true;

        StartCoroutine(openDemonEyes());
    }
    public void affectPlayer()
    {
        if (nezukoSpeed.Value == 140f) {
        // Debug.Log(nezukoSpeed.Value);

            nezukoSpeed.SetValue(0f);
            isNezukoStuck.SetValue(true);

            StartCoroutine(removeEffect());
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Disable collider
            eyeballCollder.enabled = false;
            eyeballEnabled = false;
            Debug.Log("Web collider disabled!");
        }
    }

    IEnumerator removeEffect()
    {
        yield return new WaitForSeconds(4.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        nezukoSpeed.SetValue(140f);
        isNezukoStuck.SetValue(false);
        Debug.Log("Nezuko broke free!");

    }

    IEnumerator openDemonEyes() {
        while (eyeballEnabled) {
            float randOpenTimer = Random.Range(2.0f, 6.0f);
            yield return new WaitForSeconds(randOpenTimer);
            // Debug.Log("OPENING EYES");
            // Debug.Log(randOpenTimer);
            eyeballAnimator.SetTrigger("EyeOpen");
        }
    }

    void Update()
    {

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x - (18 + 5)) {
            Destroy(this.gameObject);
        }
    }
}
