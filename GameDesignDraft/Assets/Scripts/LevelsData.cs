using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
public class LevelsData : ScriptableObject
{
#if UNITY_EDITOR
  [Multiline]
  public string DeveloperDescription = "";
#endif


  public float LV_duration = 180;
  public float multp_3Stars = 0.5f;
  public float multp_2Stars = 0.25f;

  private float LVTimeRecord;
  private int LVStarsRecord;

  public float LVTiming
  {
    get
    {
      return LVTimeRecord;
    }
    set
    {
      LVTimeRecord = value;
      if (LVTimeRecord >= multp_3Stars * LV_duration)
      {
        LVStarsRecord = 3;
      }
      else if (LVTimeRecord >= multp_2Stars * LV_duration)
      {
        LVStarsRecord = 2;
      }
      else
      {
        LVStarsRecord = 1;
      }
      Debug.Log("Level's timing set, stars = " + LVStarsRecord);
    }
  }

  public int LVStars
  {
    get
    {
      return LVStarsRecord;
    }
  }



}
