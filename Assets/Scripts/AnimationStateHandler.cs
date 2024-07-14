using UnityEngine;

public class AnimationStateHandler : MonoBehaviour
{
    [Zenject.Inject] private PlayerController _playerController;
    [Zenject.Inject] private GameStateHandler _gameStateHandler;

    [SerializeField] private Animator playerAnimator;

    private const string ANIM_FAIL = "Fail";

    private void Start()
    {
        _playerController.Mover.CurrentInputType.MoveStarted += OnMoveStarted;
        _gameStateHandler.FailAction += OnFail;
    }

    private void OnDestroy()
    {
        _playerController.Mover.CurrentInputType.MoveStarted -= OnMoveStarted;
        _gameStateHandler.FailAction -= OnFail;
    }

    private void OnMoveStarted() 
    {
        playerAnimator.enabled = true;
    }

    private void OnFail() 
    {
        playerAnimator.SetTrigger(ANIM_FAIL);
    }
}