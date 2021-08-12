using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
  public BoolVariable FinishCountdown;
  public BoolVariable isPaused;
  public LevelsData levelsData;
  public LevelsData nextLevel;

  // Start is called before the first frame update
  void Start()
  {
    isPaused.SetValue(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (FinishCountdown.Value)
    {
      GameObject timer = gameObject.transform.Find("Timer").gameObject;
      GameObject pauseButton = gameObject.transform.Find("PauseButton").gameObject;

      timer.GetComponent<Timer>().enabled = true;
      pauseButton.SetActive(true);

    }
  }

  public void PlayerLost()
  {
    foreach (Transform eachChild in transform)
    {
      if (eachChild.name == "GameOver")
      {
        eachChild.gameObject.SetActive(true);
      }
      else
      {
        eachChild.gameObject.SetActive(false);
      }
    }
  }

  public void PlayerWon()
  {
    foreach (Transform eachChild in transform)
    {
      // Set next level unlocked to true
      nextLevel.unlockLevel = true;

      if (eachChild.name == "LevelComplete" || eachChild.name == "Timer")
      {
        eachChild.gameObject.SetActive(true);

        // show number of stars accordingly
        if (eachChild.name == "LevelComplete")
        {
          GameObject stars = eachChild.transform.Find("Stars").gameObject;
          StartCoroutine(DisplayStars(stars));
        }
      }
      else
      {
        eachChild.gameObject.SetActive(false);
      }
    }
  }

  IEnumerator DisplayStars(GameObject stars)
  {
    yield return new WaitForEndOfFrame();
    int i = 0;
    foreach (Transform starImage in stars.transform)
    {
      if (i < levelsData.LVStars)
      {
        starImage.gameObject.SetActive(true);
        i += 1;
      }
    }
  }

  public void PauseButtonClicked()
  {
    isPaused.SetValue(true);

    foreach (Transform eachChild in transform)
    {
      if (eachChild.name == "Paused" || eachChild.name == "Timer")
      {
        eachChild.gameObject.SetActive(true);
      }
      else
      {
        eachChild.gameObject.SetActive(false);
      }
    }
  }

  public void ResumeButtonClicked()
  {
    isPaused.SetValue(false);
    foreach (Transform eachChild in transform)
    {
      if (eachChild.name == "PauseButton" || eachChild.name == "Timer")
      {
        eachChild.gameObject.SetActive(true);
      }
      else
      {
        eachChild.gameObject.SetActive(false);
      }
    }
  }

  public void MainMenuButtonClicked()
  {
    SceneManager.LoadScene("Menu");
  }

  public void RetryButtonClicked()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void NextLevelButtonClicked()
  {
    Debug.Log("Hello bij button clockee");

    if (SceneManager.GetActiveScene().name == "Level1")
    {
      Debug.Log("Level 1 complete. Next button clicked!");
      SceneManager.LoadScene("Level2");
    }

    if (SceneManager.GetActiveScene().name == "Level2")
    {
      Debug.Log("Level 2 complete. Next button clicked!");
      SceneManager.LoadScene("Level3");
    }

  }

}
