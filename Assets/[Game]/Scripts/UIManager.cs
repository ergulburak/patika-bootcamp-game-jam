using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public PlayerController player;
    public GameObject prepareUI;
    public GameObject mainGameUI;
    public GameObject finishGameUI;
    public GameObject gameOverUI;
    public GameObject nextLevelUI;
    public GameObject tryAgainUI;
    public GameObject completeUI;
    public GameObject levelUI;
    public GameObject coinUI;
    public GameObject distanceFinish;
    public Slider distanceSlider;
    public TextMeshProUGUI currentCoinText;
    public TextMeshProUGUI earnedCoinText;
    public TextMeshProUGUI totalCoinText;
    public TextMeshProUGUI sliderLevelText;

    [HideInInspector] public int sliderLevel = 1;
    [HideInInspector] public int coin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
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
            distanceSlider.value = player.gameObject.transform.localPosition.z;
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
