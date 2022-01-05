using System;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
  public event EventHandler<bool> OnClick;
  public event EventHandler<Vector2> OnSwerve;

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
      PlayerOnClick(true);

    if (Input.GetMouseButtonUp(0))
      PlayerOnClick(false);

    if (Input.GetMouseButton(0))
      PlayerOnSwerve(Input.mousePosition);
  }

  private void PlayerOnClick(bool click)
  {
    OnClick?.Invoke(this, click);
  }

  private void PlayerOnSwerve(Vector2 click)
  {
    OnSwerve?.Invoke(this, click);
  }
}
