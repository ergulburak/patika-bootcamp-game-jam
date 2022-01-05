using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
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

  private int sliderLevel = 1;
  private int coin;

  public int GetSliderLevel()
  {
    return sliderLevel;
  }

  private void Start()
  {
    SetStartingUI();
    SetCoinZeroOnStart();
    SetPlayerPrefs();
  }

  private void Update()
  {
    CalculateRoadDistance();
    EqualCurrentCoin();
    UpdateCoinInfo();
  }

  private void CalculateRoadDistance()
  {
    if (distanceSlider)
    {
      distanceSlider.maxValue = distanceFinish.gameObject.transform.localPosition.z;
      distanceSlider.value = PlayerManager.Instance.GetPlayerZPosition();
    }
  }

  private void SetStartingUI()
  {
    prepareUI.SetActive(true);
    mainGameUI.SetActive(false);
    finishGameUI.SetActive(false);
    gameOverUI.SetActive(false);
    coinUI.SetActive(false);
  }

  private void SetCoinZeroOnStart()
  {
    coin = 0;
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
    totalCoinText.text = PlayerPrefs.GetInt("TotalCoin").ToString();
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

    sliderLevelText.text = PlayerPrefs.GetInt("SliderLevel").ToString();
  }

  public void RetryButton()
  {
    Scene currenScene = SceneManager.GetActiveScene();
    SceneManager.LoadScene(currenScene.buildIndex);
  }

  public void NextLevelButton()
  {
    PlayerPrefs.SetInt("SliderLevel", PlayerPrefs.GetInt("SliderLevel1") + 1);
    sliderLevelText.text = PlayerPrefs.GetInt("SliderLevel").ToString();
  }
}
