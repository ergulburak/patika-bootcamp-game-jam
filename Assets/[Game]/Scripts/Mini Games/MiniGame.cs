using System.Collections;
using Cinemachine;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
  [SerializeField] private bool headsOrTails;

  private CinemachineVirtualCamera playerCam =>
    GameObject.FindWithTag("PlayerCam").GetComponent<CinemachineVirtualCamera>();

  [SerializeField] private Animator animator;

  [SerializeField] private MiniGamesEnum miniGame;

  private MiniGamesManager manager => GetComponentInParent<MiniGamesManager>();
  [SerializeField] private CinemachineVirtualCamera miniGameCam;
  [SerializeField] private MiniGamesManager miniGamesManager;


  private PlayerManager playerManager => PlayerManager.Instance;

  public IEnumerator StartAnimation()
  {
    playerManager.PlayerCanMove = !playerManager.PlayerCanMove;
    miniGameCam.Priority += playerCam.Priority;
    yield return new WaitForSeconds(1f);

    switch (miniGame)
    {
      case MiniGamesEnum.HeadsOrTails:
        animator.SetInteger(GameConstValues.HEADS_OR_TAILS, headsOrTails ? 0 : 1);
        break;
      case MiniGamesEnum.Dart:
        animator.SetInteger(GameConstValues.DART, headsOrTails ? 0 : 1);
        break;
      case MiniGamesEnum.Motorbike:
        animator.SetInteger(GameConstValues.MOTORBIKE, headsOrTails ? 0 : 1);
        break;
    }
  }

  public IEnumerator AnimationEnd()
  {
    miniGamesManager.OnEndAnimation();
    playerCam.Priority += miniGameCam.Priority;
    yield return new WaitForSeconds(1f);
    playerManager.PlayerCanMove = !playerManager.PlayerCanMove;
  }
}
