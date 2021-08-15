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

  [SerializeField]
  private float LVTimeRecord = 0;
  [SerializeField]
  private int LVStarsRecord = 0;
  [SerializeField]
  private bool isLevelUnlocked = false;

  public bool unlockLevel
  {
    get
    {
      return isLevelUnlocked;
    }
    set
    {
      isLevelUnlocked = value;
    }
  }

  public float LVTiming
  {
    get
    {
      return LVTimeRecord;
    }
    set
    {
      LVTimeRecord = LV_duration - value;
      Debug.Log(LVTimeRecord);
      if (LVTimeRecord <= multp_3Stars * LV_duration)
      {
        LVStarsRecord = 3;
      }
      else if (LVTimeRecord <= multp_2Stars * LV_duration)
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

  public void Reset()
  {
    LVTimeRecord = 0;
    LVStarsRecord = 0;
    isLevelUnlocked = false;
  }

  private void OnEnable()
  {
    hideFlags = HideFlags.DontUnloadUnusedAsset;
  }

}
