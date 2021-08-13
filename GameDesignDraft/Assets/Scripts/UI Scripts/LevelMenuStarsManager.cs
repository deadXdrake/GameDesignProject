using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuStarsManager : MonoBehaviour
{
  public List<GameObject> starIcons;
  public LevelsData levelsData;
  // private int levelStars = 1;
  public Material material;

  // Start is called before the first frame update
  void Start()
  {
    // Debug.Log(levelsData.DeveloperDescription);
    for (int i = 0; i < levelsData.LVStars; i++)
    {
      // Debug.Log("Hello stars");
      starIcons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    Debug.Log("START no. of stars " + levelsData.LVStars);
    // for (int i = 0; i < levelStars; i++) {
    //     starIcons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    // }

    if (levelsData.unlockLevel != true)
    {
      this.GetComponent<Image>().material = material;
      this.GetComponent<Button>().interactable = false;
      Debug.Log("levelsData.unlockLevel != true");
    }
    else
    {
      this.GetComponent<Image>().material = null;
      this.GetComponent<Button>().interactable = true;
      Debug.Log("elseee");
    }

    Debug.Log("This is LevelMenuStarsManager Start woop");
  }

  void Update()
  {
  }

}
