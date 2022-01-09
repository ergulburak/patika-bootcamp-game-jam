using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
  [FoldoutGroup("Panels")] [SerializeField]
  private GameObject menuPanel;

  [FoldoutGroup("Panels")] [SerializeField]
  private GameObject inGamePanel;

  [FoldoutGroup("Panels")] [SerializeField]
  private GameObject winPanel;

  [FoldoutGroup("Panels")] [SerializeField]
  private GameObject failPanel;


  public event EventHandler OnGameStart;
  private int sliderLevel = 1;
  private int coin;

  public int GetSliderLevel()
  {
    return sliderLevel;
  }

  public void SetStartingUI()
  {
    SetCoinZeroOnStart();

    menuPanel.SetActive(true);
    failPanel.SetActive(false);
    winPanel.SetActive(false);
    inGamePanel.SetActive(false);

  }

  public void StartGame()
  {
    OnGameStart?.Invoke(this, EventArgs.Empty);
  }

  private void SetCoinZeroOnStart()
  {
    coin = PlayerPrefs.GetInt(GameConstValues.TOTAL_COIN, 0);
  }

  private void SetPlayerPrefs()
  {
    if (!PlayerPrefs.HasKey("TotalCoin"))
    {
      PlayerPrefs.SetInt("TotalCoin", coin);
    }

    if (!PlayerPrefs.HasKey("SliderLevel"))
    {
      PlayerPrefs.SetInt("SliderLevel", sliderLevel);
    }

    //sliderLevelText.text = $"{SaveManager.Instance.GetCurrentLevel()}";
  }

  public void RetryButton()
  {
    Scene currenScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currenScene.buildIndex);
  }

  public void NextLevelButton()
  {
    SaveManager.Instance.LevelUp();
    //sliderLevelText.text = $"{SaveManager.Instance.GetCurrentLevel()}";
  }

  public void OpenInGamePanel()
  {
    menuPanel.SetActive(false);
    failPanel.SetActive(false);
    winPanel.SetActive(false);
    inGamePanel.SetActive(true);


  }

  public void OpenLevelSuccessPanel()
  {
    menuPanel.SetActive(false);
    failPanel.SetActive(false);
    winPanel.SetActive(true);
    inGamePanel.SetActive(false);
  }
}
