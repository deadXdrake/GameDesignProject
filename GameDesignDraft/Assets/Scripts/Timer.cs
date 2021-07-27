using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
  private float timeValue = 180;
  private bool isLevelDone = false;

  public Text timerText;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (timeValue > 0 & !isLevelDone)
    {
      timeValue -= Time.deltaTime;
    }
    // else
    // {
    //   timeValue = 0;
    // }

    // timerText.text = string.Format("Time left:\n{0:000} secs", timeValue);
    DisplayTime(timeValue);
  }

  void DisplayTime(float timeToDisplay)
  {
    if (timeToDisplay < 0)
    {
      timeToDisplay = 0;
    }
    float mins = Mathf.FloorToInt(timeToDisplay / 60);
    float secs = Mathf.FloorToInt(timeToDisplay % 60);
    float msecs = timeToDisplay % 1 * 100;

    timerText.text = string.Format("Time left:\n{0:00}:{1:00}.{2:00}", mins, secs, msecs);
  }

  public void StopTimeEvent()
  {
    isLevelDone = true;
  }

}
