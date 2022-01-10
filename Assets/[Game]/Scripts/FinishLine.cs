using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class FinishLine : MonoBehaviour
{
  public Transform Pivot;
  [SerializeField] private Animator animatorController;
  [SerializeField] private CinemachineVirtualCamera finishCam;
  [SerializeField] private List<ParticleSystem> confetti;

  private CinemachineVirtualCamera playerCam =>
    GameObject.FindWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();

  private void FinishScene()
  {
    animatorController.SetBool(GameConstValues.RUN, true);
    PlayerManager.Instance.PlayerCanMove = false;
    finishCam.Priority = playerCam.Priority + 1;
  }

  public void AnimationEnd()
  {
    foreach (var con in confetti)
    {
      con.Play();
    }

    PlayerManager.Instance.OnWin();
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      FinishScene();
    }

    if (other.GetComponent<Collectable>())
    {
      StackManager.Instance.OnFinishRemoveCollectable(this);
    }
  }
}
