using Sirenix.OdinInspector;
using UnityEngine;


public class GameManager : MonoBehaviour
{
  [SerializeField] [ReadOnly] private GameStates state = GameStates.Menu;

  private void HandleStates()
  {
    switch (state)
    {
      case GameStates.Menu:
        break;
      case GameStates.InGame:
        break;
      case GameStates.End:
        break;
    }
  }
}
