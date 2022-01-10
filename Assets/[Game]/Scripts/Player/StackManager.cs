using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;


public class StackManager : Singleton<StackManager>
{
  [FoldoutGroup("Stack Settings")] [SerializeField]
  protected Transform playerPivot;

  [FoldoutGroup("Stack Settings")] [SerializeField] [Range(0f, 2f)]
  private float offsetMultiplier;

  [FoldoutGroup("Stack Settings")] [SerializeField] [Range(0f, 2f)]
  private float stackFollowSpeed;

  [FoldoutGroup("Stack Settings")] [SerializeField] [Range(0f, 1f)]
  private float pulseDuration;

  [FoldoutGroup("Stack Settings")] [SerializeField] [Range(0f, 2f)]
  private float pulseScale;

  [FoldoutGroup("Stack Settings")] [SerializeField] [Range(0f, 2f)]
  private float pulseDelay;

  [SerializeField] private List<GameObject> collectables = new List<GameObject>();

  public int GetCollectablesCount()
  {
    return collectables.Count;
  }

  public void AddCollectable(GameObject collectable)
  {
    if (collectables.Contains(collectable)) return;
    collectables.Add(collectable);
    StartCoroutine(nameof(StackPulse));
    collectable.transform.SetParent(transform);
    collectable.transform.localPosition = collectable.Equals(collectables.First())
      ? new Vector3(playerPivot.localPosition.x, 0, collectables.Count * offsetMultiplier)
      : new Vector3(collectables[collectables.IndexOf(collectable) - 1].transform.localPosition.x, 0,
        collectables.Count * offsetMultiplier);
  }

  public void RemoveCollectable(MiniGamesManager miniGamesManager)
  {
    if (collectables.Count.Equals(0)) return;
    var lastCollectable = collectables.Last();
    lastCollectable.transform.SetParent(miniGamesManager.transform);
    lastCollectable.gameObject.SetActive(false);
    collectables.Remove(lastCollectable);
    miniGamesManager.AddCollectable(lastCollectable);
  }

  public void OnFinishRemoveCollectable(FinishLine finishLine)
  {
    if (collectables.Count.Equals(0)) return;
    var lastCollectable = collectables.Last();
    lastCollectable.transform.SetParent(finishLine.transform.parent);
    lastCollectable.GetComponent<Collider>().enabled = false;
    lastCollectable.transform.DOLocalMove(finishLine.Pivot.localPosition, .8f);
    lastCollectable.transform.DOLocalRotate(finishLine.Pivot.localEulerAngles, .8f)
      .OnComplete(() => { lastCollectable.gameObject.SetActive(false); });

    collectables.Remove(lastCollectable);
  }

  public void WatchTheFront()
  {
    foreach (var collectable in collectables.ToList())
    {
      if (collectable.Equals(null)) continue;
      var previous = collectable.Equals(collectables.First())
        ? playerPivot.gameObject
        : collectables[collectables.IndexOf(collectable) - 1];
      var localPosition = collectable.transform.localPosition;
      var newPos = localPosition;
      newPos = new Vector3(
        previous.transform.localPosition.x,
        newPos.y,
        newPos.z);
      newPos = Vector3.LerpUnclamped(localPosition, newPos,
        stackFollowSpeed);

      collectable.transform.localPosition = newPos;
    }
  }

  IEnumerator StackPulse()
  {
    foreach (var collectable in Enumerable.Reverse(collectables).ToList())
    {
      collectable.transform.DOKill();
      collectable.transform.localScale = Vector3.one;
      yield return new WaitForSeconds(pulseDelay);
      if (collectable.SafeIsUnityNull()) continue;
      collectable.transform.DOPunchScale(new Vector3(pulseScale, pulseScale, pulseScale), pulseDuration, 0, .3f)
        .OnComplete(() => collectable.transform.localScale = Vector3.one);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Collectable>())
    {
      AddCollectable(other.gameObject);
    }
  }
}
