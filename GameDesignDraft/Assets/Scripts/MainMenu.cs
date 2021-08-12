using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public LevelsData level1Data;
    // Start is called before the first frame update
    void Start()
    {
        level1Data.unlockLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel1() {
        SceneManager.LoadScene("Level1");
    }
    public void PlayLevel2() {
        SceneManager.LoadScene("Level2");
    }
    public void PlayLevel3() {
        SceneManager.LoadScene("Level3");
    }
}
