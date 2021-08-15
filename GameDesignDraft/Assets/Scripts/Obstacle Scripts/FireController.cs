using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour, ObstacleInterface
{
  public FloatVariable nezukoSpeed;
  public FloatVariable nezukoUpSpeed;
  private Vector2 screenBounds;
  private Rigidbody2D fireBody;
  private SpriteRenderer fireSprite;

  // Start is called before the first frame update
  void Start()
  {
    fireBody = GetComponent<Rigidbody2D>();
    fireSprite = GetComponent<SpriteRenderer>();
  }

  public void affectPlayer()
  {
    if (nezukoSpeed.Value == 140f)
    {
      // Debug.Log(nezukoSpeed.Value);

      nezukoSpeed.ApplyChange(-100f);
      nezukoUpSpeed.ApplyChange(-10f);

      // Debug.Log(nezukoSpeed.Value);
      // Debug.Log("Player burnt! Speed and upSpeed reduced.");
      StartCoroutine(removeEffect());
    }
  }

  IEnumerator removeEffect()
  {
    yield return new WaitForSeconds(5.0f);  //TODO: Hardcoded time. Put in scriptable constants?
    
    if (nezukoSpeed.Value != 0) {
      nezukoSpeed.ApplyChange(100f);
      nezukoUpSpeed.ApplyChange(+10f);
      // nezukoSpeed.ApplyChange(100);
      // Debug.Log("Nezuko recovered from burn!");
    }
  }

  // Update is called once per frame
  void Update()
  {
    screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    if (transform.position.x < screenBounds.x - (18 + 5))
    {
      Destroy(this.gameObject);
    }
  }
}
