using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class NextState : MonoBehaviour
{
    [Zenject.Inject] private PlayerController _playerController;

    [SerializeField, SerializeReference, SubclassSelector] private AbstractStateData stateData;
    [SerializeField] private float delay;
    [SerializeField] private StateTrigger[] stateTriggers;
    [SerializeField] private UnityEvent onCompleted;

    public bool IsCompleted { get; private set; }

    private void Awake() 
    {
        foreach (var state in stateTriggers)
        {
            state.Completed += CheckStates;        
        }
    }

    private void OnDestroy() 
    {
        foreach (var state in stateTriggers)
        {
            state.Completed -= CheckStates;        
        }
    }

    public void RotatePlayer(Transform viewPoint, Action onComplete) 
    {
        _playerController.Mover.PlayerObj.RotateTo(viewPoint, delay, onComplete);
    }

    private void CheckStates() 
    {
        if (IsCompleted) return;

        if (stateTriggers.Any(x => x.IsCompleted == false)) return;

        this.InlineWaitForSeconds(delay, () => 
        {
            onCompleted?.Invoke();
            IsCompleted = true;
            HandleCustomData();
        });
    }

    private void HandleCustomData() 
    {
        switch(stateData) 
        {
            case SimpleStateData data: 
                switch(data.playerMoveStrategy) 
                {
                    case MoveStrategy.Linear:
                        _playerController.Mover.PlayerObj.RotateTo(data.playerRotatePoint, data.duration, () => 
                        {
                            _playerController.Mover.SelectMoveStrategy(new PlayerMoveLinear(
                            _playerController.Mover.PlayerObj, 
                            data.moveSpeed, 
                            data.playerDirection

                            ));

                            _playerController.Mover.ChangeDirection(data.playerDirection);
                            _playerController.CameraFollower.ChangePositionOffset(data.cameraDirOffset);
                            //_playerController.CameraFollower.ChangeSpeed(data.cameraSpeed);
                            //_playerController.CameraFollower.ChangeRotationOffset(data.cameraRotationOffset);
                        });
                        break;
                    default: return;
                }
                break;
            default: return;
        }
    }
    

    [Serializable]
    public abstract class AbstractStateData { }

    [Serializable]
    public class SimpleStateData : AbstractStateData 
    {
        public MoveStrategy playerMoveStrategy;
        public float moveSpeed;
        public float cameraSpeed;
        public float duration;
        public Transform playerRotatePoint;
        public Vector3 playerDirection;
        public Vector3 cameraDirOffset;
        public Vector3 cameraRotationOffset;
    }
}