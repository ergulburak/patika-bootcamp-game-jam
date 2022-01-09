using System;
using UnityEngine;


public class FinishLine : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      PlayerManager.Instance.OnWin();
    }
  }
}
