using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
  public Text countdownText;
  private float countdownTime = 3;
  public BoolVariable FinishCountDown;
  // private bool isCountFinish = false;
  // Start is called before the first frame update
  void Start()
  {
    FinishCountDown.SetValue(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (countdownTime > 0)
    {
      countdownTime -= Time.deltaTime;
    }
    else
    {
      countdownTime = 0;
      gameObject.SetActive(false);
      FinishCountDown.SetValue(true);
    }

    int displayCount = Mathf.FloorToInt(countdownTime);
    if (displayCount > 0)
    {
      countdownText.text = string.Format("{0:0}", displayCount);

    }
    else
    {
      countdownText.text = "RUN!";
    }
  }
}
