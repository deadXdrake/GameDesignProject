using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanjiroController : MonoBehaviour
{
  private Animator tanjiroAnimator;

  // Start is called before the first frame update
  void Start()
  {
    tanjiroAnimator = GetComponent<Animator>();

  }

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter2D(Collision2D col)
  {
    if (col.gameObject.CompareTag("Player"))
    {
      tanjiroAnimator.SetBool("isWin", true);
    }
  }
}
