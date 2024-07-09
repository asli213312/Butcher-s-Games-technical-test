using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerMover mover;
    [SerializeField] private CameraFollow follower;

    public PlayerMover Mover => mover;
    public CameraFollow CameraFollower => follower;

    private void Awake() 
    {
        mover.Initialize();
        follower.Initialize();

        mover.CurrentInputType.MoveStarted += OnMoveStarted;
    }

    private void OnDestroy() 
    {
        mover.CurrentInputType.MoveStarted -= OnMoveStarted;
    }

    private void FixedUpdate() 
    {
        mover.Handle();
    }

    private void LateUpdate() 
    {
        follower.Handle();
    }

    private void OnMoveStarted() 
    {
        follower.Activate();
    }
}
