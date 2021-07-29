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


  public float LV1_duration = 180;
  private float LV1TimeRecord;
  private int LV1StarsRecord;

  public float LV1Timing
  {
    get
    {
      return LV1TimeRecord;
    }
    set
    {
      LV1TimeRecord = value;
      if (LV1TimeRecord >= 0.5 * LV1_duration)
      {
        LV1StarsRecord = 3;
      }
      else if (LV1TimeRecord >= 0.25 * LV1_duration)
      {
        LV1StarsRecord = 2;
      }
      else
      {
        LV1StarsRecord = 1;
      }
    }
  }

  public int LV1Stars
  {
    get
    {
      return LV1StarsRecord;
    }
  }



}
