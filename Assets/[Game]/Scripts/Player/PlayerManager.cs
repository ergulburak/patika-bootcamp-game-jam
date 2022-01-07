using System;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
  public bool PlayerCanMove;
  public event EventHandler OnFinish;
  private MovementController movementController => GetComponent<MovementController>();
  private StackManager stackManager => GetComponent<StackManager>();
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
      movementController.MoveForward();
      if (stackManager.GetCollectablesCount() > 0)
        stackManager.WatchTheFront();
    }
  }

  public void OnWin()
  {
    Debug.Log("aa");
    OnFinish?.Invoke(this, EventArgs.Empty);
  }

  public float GetPlayerZPosition()
  {
    return transform.position.z;
  }

  private void OnSwerve(object sender, Vector2 e)
  {
    if (PlayerCanMove)
      movementController.MoveHorizontal(e);
  }

  private void OnFirstClick(object sender, bool e)
  {
    if (onFirstClick)
    {
      onFirstClick = false;
      //PlayerCanMove = !PlayerCanMove;
    }
  }
}
