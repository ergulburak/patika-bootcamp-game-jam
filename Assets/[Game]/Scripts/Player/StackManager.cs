using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;


public class StackManager : MovementController
{
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

  protected List<GameObject> collectables = new List<GameObject>();


  private void AddCollectable(GameObject collectable)
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

  protected void WatchTheFront()
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
      yield return new WaitForSeconds(pulseDelay);
      if (collectable.SafeIsUnityNull()) yield break;
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
