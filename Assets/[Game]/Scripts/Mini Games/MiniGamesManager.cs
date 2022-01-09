using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MiniGamesManager : MonoBehaviour
{
  [SerializeField] private MiniGame miniGame;
  [SerializeField] private Gate leftGate;
  [SerializeField] private Gate rightGate;
  [SerializeField] private Sprite goodSprite;
  [SerializeField] private Sprite badSprite;
  [SerializeField] private GameObject collectablePrefab;

  private List<GameObject> collectables = new List<GameObject>();
  private int totalWin = 0;

  private void Start()
  {
    leftGate.Initialize(true, goodSprite);
    rightGate.Initialize(false, badSprite);
  }

  public void AddCollectable(GameObject collectable)
  {
    collectables.Add(collectable);
  }

  public void PlayerDetected()
  {
    totalWin = leftGate.collectedCollectable + rightGate.collectedCollectable;
    leftGate.GetComponent<Collider>().enabled = false;
    rightGate.GetComponent<Collider>().enabled = false;
    leftGate.gameObject.SetActive(false);
    rightGate.gameObject.SetActive(false);
    StartCoroutine(miniGame.StartAnimation());
  }

  public void OnEndAnimation()
  {
    foreach (var varCollectable in collectables)
    {
      varCollectable.SetActive(true);
    }

    if (totalWin > 0)
    {
      for (int i = 0; i < totalWin; i++)
      {
        var newCollectable = Instantiate(collectablePrefab);
        collectables.Add(newCollectable);
      }
    }

    if (totalWin < 0)
    {
      for (int i = totalWin; i < 0; i++)
      {
        var lastColl = collectables.Last();
        collectables.Remove(lastColl);
        Destroy(lastColl);
      }
    }

    GiveCollectablesToPlayer();
  }

  private void GiveCollectablesToPlayer()
  {
    if (collectables.Count > 0)
    {
      foreach (var collectable in collectables)
      {
        StackManager.Instance.AddCollectable(collectable);
      }
    }
  }
}
