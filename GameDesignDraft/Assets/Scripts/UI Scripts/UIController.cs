using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
  public BoolVariable FinishCountdown;
  public BoolVariable isPaused;
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
      if (eachChild.name == "LevelComplete")
      {
        eachChild.gameObject.SetActive(true);
      }
      else
      {
        eachChild.gameObject.SetActive(false);
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
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }

    }

}
