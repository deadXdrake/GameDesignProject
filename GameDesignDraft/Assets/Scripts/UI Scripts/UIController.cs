using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
  public BoolVariable FinishCountdown;
  // Start is called before the first frame update
  void Start()
  {
    // children = GetComponentsInChildren<GameObject>();
  }

  // Update is called once per frame
  void Update()
  {
    // if (!gameObject.transform.Find("StartCountdown").gameObject.activeInHierarchy)
    if (FinishCountdown.Value)
    {
      GameObject timer = gameObject.transform.Find("Timer").gameObject;
      timer.GetComponent<Timer>().enabled = true;
    }
    // foreach (GameObject eachChild in children)
    // {
    //     if (eachChild.name == "StartCountdown" && eachChild.activeInHierarchy) {

    //     }

    // }
  }

  public void PlayerLosed()
  {

  }

  public void PlayerWon()
  {

  }

  public void PauseButtonClicked()
  {

  }
}
