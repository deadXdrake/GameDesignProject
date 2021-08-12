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
        for (int i = 0; i < levelsData.LVStars; i++) {
            Debug.Log("Hello stars");
            // starIcons[i].SetActive(true);
        }

        // for (int i = 0; i < levelStars; i++) {
        //     starIcons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        // }

        if (levelsData.isLevelUnlocked != true) {
            this.GetComponent<Image>().material = material;
            this.GetComponent<Button>().interactable = false;
        } else {
            this.GetComponent<Image>().material = null;
            this.GetComponent<Button>().interactable = true;
        }
    }

}
