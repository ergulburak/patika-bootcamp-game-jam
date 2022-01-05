using System;
using UnityEngine;

public class PlayerManager : StackManager
{
  public bool PlayerCanMove;
  private bool onFirstClick = true;

  private void Awake()
  {
    InputManager.Instance.OnClick += OnFirstClick;
    InputManager.Instance.OnSwerve += OnSwerve;
  }

  private void Update()
  {
    if (PlayerCanMove)
    {
      MoveForward(transform);
      if (collectables.Count > 0)
        WatchTheFront();
    }
  }

  private void OnSwerve(object sender, Vector2 e)
  {
    if (PlayerCanMove)
      MoveHorizontal(e);
  }

  private void OnFirstClick(object sender, bool e)
  {
    if (onFirstClick)
    {
      onFirstClick = false;
      PlayerCanMove = !PlayerCanMove;
    }
  }
}
