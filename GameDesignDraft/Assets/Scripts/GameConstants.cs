using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
  //Nezuko's controller constant values
  public float nezukoSpeedX = 140;
  public float nezukoMaxSpeed = 10;
  public float nezukoUpSpeed = 25;

}