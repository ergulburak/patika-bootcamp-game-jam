using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class PlatformCreator : MonoBehaviour
{
  public Transform parent;
  public Transform startPlatformsParent;
  public float frequency = 10f;
  public GameObject platformPrefab;
  public List<GameObject> platforms = new List<GameObject>();

  private bool isStartPlatformsAdded = false;
  private float distance = 0f;

#if UNITY_EDITOR

  [Button, PropertyOrder(-1)]
  public void AddRoad()
  {
    if (!platformPrefab.SafeIsUnityNull())
    {
      distance = frequency * platforms.Count;
      var newPlatform = PrefabUtility.InstantiatePrefab(platformPrefab) as GameObject;

      newPlatform!.transform.position = new Vector3(0, 0, distance);
      newPlatform.transform.parent = parent;

      platforms.Add(newPlatform);
    }
    else
      Debug.LogWarning("platformPrefab is null");
  }

  [Button, PropertyOrder(-1)]
  public void RemoveRoad()
  {
    if (platforms.Count <= 0)
    {
      Debug.LogWarning("All platforms removed");
      return;
    }

    DestroyImmediate(platforms.Last());
    platforms.Remove(platforms.Last());
  }

  [Button, PropertyOrder(-1)]
  public void AddStartRoad()
  {
    if (isStartPlatformsAdded)
    {
      Debug.LogWarning("Start platforms are already added");
      return;
    }

    if (!platformPrefab.SafeIsUnityNull())
    {
      for (int i = 1; i < 3; i++)
      {
        var startDistance = frequency * -i;
        var newPlatform = PrefabUtility.InstantiatePrefab(platformPrefab) as GameObject;

        newPlatform!.transform.position = new Vector3(0, 0, startDistance);
        newPlatform.transform.parent = startPlatformsParent;
      }
    }
    else
      Debug.LogWarning("platformPrefab is null");
  }

#endif
}
