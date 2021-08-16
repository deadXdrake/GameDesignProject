using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Timer : MonoBehaviour
{
  public LevelsData LevelsData;
  public Text timerText;
  public BoolVariable isPaused;


  private float timeValue;
  private bool isLevelDone = false;

  public UnityEvent onTimesUp;


  // Start is called before the first frame update
  void Start()
  {
    timeValue = LevelsData.LV_duration;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isPaused.Value)
    {
      if (timeValue > 0 & !isLevelDone)
      {
        timeValue -= Time.deltaTime;
      }
      DisplayTime(timeValue);
    }

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
    
    float star3Time = LevelsData.LV_duration * LevelsData.multp_3Stars; // 18secs 60-18 = 42
    float star2Time = LevelsData.LV_duration * LevelsData.multp_2Stars;

    if (timeToDisplay > 60f - star3Time) {
      // timerText.color = new Color(0, 0, 0, 1);
      timerText.color = Color.green;
    } else if (timeToDisplay > 60f - star2Time) {
      timerText.color = Color.yellow;
    } else {
      timerText.color = Color.red;
    }
  }

  public void PlayerWinResponse()
  {
    isLevelDone = true;
    Debug.Log("Level complete");
    LevelsData.LVTiming = timeValue;
    Debug.Log("Timing: " + LevelsData.LVTiming);
  }

  public void PlayerLostResponse()
  {
    isLevelDone = true;
    Debug.Log("game over");
  }

}
