using System;
using Sirenix.OdinInspector;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
  [SerializeField] [ReadOnly] private GameStates state = GameStates.Menu;

  private void Start()
  {
    HandleStates();
    UIManager.Instance.OnGameStart += StartGame;
    PlayerManager.Instance.OnFinish += OnWin;
  }

  private void OnWin(object sender, EventArgs e)
  {
    state = GameStates.LevelSuccess;
    HandleStates();
  }

  private void StartGame(object sender, EventArgs e)
  {
    state = GameStates.InGame;
    HandleStates();
  }

  private void HandleStates()
  {
    switch (state)
    {
      case GameStates.Menu:
        UIManager.Instance.SetStartingUI();
        break;
      case GameStates.InGame:
        PlayerManager.Instance.PlayerCanMove = true;
        UIManager.Instance.OpenInGamePanel();
        break;
      case GameStates.LevelSuccess:
        PlayerManager.Instance.PlayerCanMove = false;
        UIManager.Instance.OpenLevelSuccessPanel();
        break;
      case GameStates.LevelFail:
        break;
    }
  }
}
