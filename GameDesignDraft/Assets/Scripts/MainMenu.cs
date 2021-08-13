using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  public LevelsData level1Data;


  //DEBUGGING [TO DELETE AFTER]
  public LevelsData lv2Data;
  public LevelsData lv3Data;

  // Start is called before the first frame update
  void Start()
  {
    level1Data.unlockLevel = true;
    Debug.Log("Level 1 unlock");
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void PlayLevel1()
  {
    SceneManager.LoadScene("Level1");
  }
  public void PlayLevel2()
  {
    SceneManager.LoadScene("Level2");
  }
  public void PlayLevel3()
  {
    SceneManager.LoadScene("Level3");
  }

  public void Reset()
  {
    //DEBUGGING [TO DELETE AFTER]
    level1Data.Reset();
    lv2Data.Reset();
    lv3Data.Reset();
    Debug.Log("levels reset");
    SceneManager.LoadScene("Menu");
  }

  public void ExitGame()
  {
    Application.Quit();
  }
}
