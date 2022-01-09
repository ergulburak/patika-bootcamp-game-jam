using System;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
  [SerializeField] private Color goodColor;
  [SerializeField] private Color badColor;
  public bool isGoodGate;
  [SerializeField] private Image gateImage;
  [SerializeField] private Text collectedCollectableText;
  [SerializeField] private Image gateSetImage;
  [SerializeField] private MiniGamesManager miniGamesManager;

  public int collectedCollectable = 0;

  public void Initialize(bool gateWhat, Sprite gateSprite)
  {
    isGoodGate = gateWhat;
    gateImage.color = isGoodGate ? goodColor : badColor;
    gateSetImage.sprite = gateSprite;
  }

  private void Update()
  {
    collectedCollectableText.text = $"{collectedCollectable}";
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.GetComponent<Collectable>())
    {
      collectedCollectable = isGoodGate ? collectedCollectable + 1 : collectedCollectable - 1;
      StackManager.Instance.RemoveCollectable(miniGamesManager);
    }
    else
    {
      miniGamesManager.PlayerDetected();
    }
  }
}
