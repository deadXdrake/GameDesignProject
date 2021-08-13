using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
  //Nezuko's controller constant values
  public float nezukoSpeedX = 140;
  public float nezukoMaxSpeed = 10;
  public float nezukoUpSpeed = 100;
  public float nezukoSmallUpSpeed = 20;
  public float cameraSpeedStandardEasy = 1.5f;
  public float cameraSpeedSlow = 2.0f;
  public float cameraSpeedMid = 2.5f;
  public float cameraSpeedFast = 3.0f;
  public bool stuck = false;

}