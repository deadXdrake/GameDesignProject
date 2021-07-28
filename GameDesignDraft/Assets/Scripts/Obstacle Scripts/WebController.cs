using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebController : MonoBehaviour, ObstacleInterface
{
    private Rigidbody2D webBody;
    private SpriteRenderer webSprite;
    public FloatVariable nezukoSpeed;
    public BoolVariable isNezukoStuck;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        webBody = GetComponent<Rigidbody2D>();
        webSprite = GetComponent<SpriteRenderer>();
    }

    public void affectPlayer()
    {
        if (nezukoSpeed.Value == 140f) {
        Debug.Log(nezukoSpeed.Value);

        nezukoSpeed.SetValue(0f);
        isNezukoStuck.SetValue(true);

        Debug.Log(nezukoSpeed.Value);
        Debug.Log("Nezuko caught in web!");
        StartCoroutine(removeEffect());
        }
    }

    IEnumerator removeEffect()
    {
        yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
        nezukoSpeed.SetValue(140f);
        isNezukoStuck.SetValue(false);
        Debug.Log("Nezuko broke free!");
    }

    // Update is called once per frame
    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < screenBounds.x - (18 + 5)) {
            Destroy(this.gameObject);
        }
    }
}
