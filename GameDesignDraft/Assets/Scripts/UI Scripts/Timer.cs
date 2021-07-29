using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
  public LevelsData LevelsData;
  public Text timerText;

  private float timeValue;
  private bool isLevelDone = false;

  public UnityEvent onTimesUp;


  // Start is called before the first frame update
  void Start()
  {
    timeValue = LevelsData.LV1_duration;
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
    //   onTimesUp.Invoke();
    // }

    // timerText.text = string.Format("Time left:\n{0:000} secs", timeValue);
    DisplayTime(timeValue);
  }

  void DisplayTime(float timeToDisplay)
  {
    if (timeToDisplay < 0)
    {
      timeToDisplay = 0;
      onTimesUp.Invoke();
    }
    float mins = Mathf.FloorToInt(timeToDisplay / 60);
    float secs = Mathf.FloorToInt(timeToDisplay % 60);
    float msecs = timeToDisplay % 1 * 100;

    timerText.text = string.Format("Time:\n{0:00}:{1:00}.{2:00}", mins, secs, msecs);
  }

  public void PlayerWinResponse()
  {
    isLevelDone = true;
    Debug.Log("Level complete");
  }

  public void PlayerLoseResponse()
  {
    isLevelDone = true;
    Debug.Log("game over");
  }

}
