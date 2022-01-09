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

  [SerializeField] private GameObject prepareUI;
  [SerializeField] private GameObject mainGameUI;
  [SerializeField] private GameObject finishGameUI;
  [SerializeField] private GameObject gameOverUI;
  [SerializeField] private GameObject nextLevelUI;
  [SerializeField] private GameObject tryAgainUI;
  [SerializeField] private GameObject completeUI;
  [SerializeField] private GameObject levelUI;
  [SerializeField] private GameObject coinUI;
  [SerializeField] private GameObject distanceFinish;
  [SerializeField] private Slider distanceSlider;
  [SerializeField] private TextMeshProUGUI currentCoinText;
  [SerializeField] private TextMeshProUGUI earnedCoinText;
  [SerializeField] private TextMeshProUGUI totalCoinText;
  [SerializeField] private TextMeshProUGUI sliderLevelText;


  public event EventHandler OnGameStart;
  private int sliderLevel = 1;
  private int coin;

  public int GetSliderLevel()
  {
    return sliderLevel;
  }

  private void Update()
  {
    if(inGamePanel.activeSelf)
      CalculateRoadDistance();
    //EqualCurrentCoin();
    //UpdateCoinInfo();
  }

  private void CalculateRoadDistance()
  {
    if (distanceSlider)
    {
      distanceSlider.maxValue = distanceFinish.gameObject.transform.localPosition.z;
      distanceSlider.value = PlayerManager.Instance.GetPlayerZPosition();
    }
  }

  public void SetStartingUI()
  {
    SetCoinZeroOnStart();
    //SetPlayerPrefs();

    menuPanel.SetActive(true);
    failPanel.SetActive(false);
    winPanel.SetActive(false);
    inGamePanel.SetActive(false);

    //prepareUI.SetActive(true);
    //mainGameUI.SetActive(false);
    //finishGameUI.SetActive(false);
    //gameOverUI.SetActive(false);
    //coinUI.SetActive(false);
  }

  public void StartGame()
  {
    OnGameStart?.Invoke(this, EventArgs.Empty);
  }

  private void SetCoinZeroOnStart()
  {
    coin = PlayerPrefs.GetInt(GameConstValues.TOTAL_COIN, 0);
  }

  public void EarnCoinByCollectables(int value)
  {
    coin = coin + value;
    UpdateCoinInfo();
  }

  private void EqualCurrentCoin()
  {
    currentCoinText.text = coin.ToString();
  }

  public void UpdateCoinInfo()
  {
    earnedCoinText.text = currentCoinText.text;
    totalCoinText.text = $"{SaveManager.Instance.GetCoin()}";
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

    sliderLevelText.text = $"{SaveManager.Instance.GetCurrentLevel()}";
  }

  public void RetryButton()
  {
    Scene currenScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currenScene.buildIndex);
  }

  public void NextLevelButton()
  {
    SaveManager.Instance.LevelUp();
    sliderLevelText.text = $"{SaveManager.Instance.GetCurrentLevel()}";
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
