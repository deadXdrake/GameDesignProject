using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour, ObstacleInterface
{
    private Rigidbody2D webBody;
    private SpriteRenderer webSprite;
    private BoxCollider2D webCollider;
    public FloatVariable nezukoSpeed;
    public BoolVariable isNezukoStuck;
    private Vector2 screenBounds;
    // private bool onHit = false;

    // Start is called before the first frame update
    void Start()
    {
        webBody = GetComponent<Rigidbody2D>();
        webSprite = GetComponent<SpriteRenderer>();
        webCollider = GetComponent<BoxCollider2D>();
        webCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Disable collider
            webCollider.enabled = false;
            Debug.Log("Web collider disabled!");
        }
    }

    public void affectPlayer()
    {
        if (nezukoSpeed.Value != 0) {
        // Debug.Log(nezukoSpeed.Value);

            nezukoSpeed.SetValue(0f);
            isNezukoStuck.SetValue(true);

            StartCoroutine(removeEffect());
        }
    }

    IEnumerator removeEffect()
    {
        yield return new WaitForSeconds(4.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        nezukoSpeed.SetValue(140f);
        isNezukoStuck.SetValue(false);
        Debug.Log("Nezuko broke free!");

    }

    // Update is called once per frame
    void Update()
    {
        // if (onHit ) {
        //     webCollider.enabled = false;
        //     Debug.Log("Web disabled!");
        // }

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x - (18 + 5)) {
            Destroy(this.gameObject);
        }
    }
}
