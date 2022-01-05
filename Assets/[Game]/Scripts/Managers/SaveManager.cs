using System;
using JetBrains.Annotations;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
  private const string LEVEL_NUMBER = "LEVEL_NUMBER";
  private const string TOTAL_COIN = "TOTAL_COIN";
  private int currentLevel;
  private int totalCoin;

  private void Awake()
  {
    totalCoin = PlayerPrefs.GetInt(TOTAL_COIN, 0);
    currentLevel = PlayerPrefs.GetInt(LEVEL_NUMBER, 1);
  }

  public int GetCoin()
  {
    return totalCoin;
  }

  public int GetCurrentLevel()
  {
    return currentLevel;
  }

  public void AddCoin(int value)
  {
    PlayerPrefs.SetInt(TOTAL_COIN, totalCoin + value);
  }

  public void LevelUp()
  {
    PlayerPrefs.SetInt(LEVEL_NUMBER, currentLevel + 1);
  }
}
