using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*private AudioSource nezukoJump;
    private AudioSource nezukoUnShrink;
    private AudioSource nezukoShrink;
    private AudioSource gameOver;
    private AudioSource gameWin;*/

    public AudioClip buttonClicked;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        /*AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        nezukoJump = allMyAudioSources[0];
        nezukoUnShrink = allMyAudioSources[1];
        nezukoShrink = allMyAudioSources[2];
        gameOver = allMyAudioSources[3];
        gameWin = allMyAudioSources[4];*/
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        audioSource.PlayOneShot(buttonClicked);
    }
}
