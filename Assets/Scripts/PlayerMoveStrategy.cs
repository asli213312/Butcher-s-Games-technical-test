using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveStrategy { Linear }

public abstract class PlayerMoveStrategy
{
    protected float MoveSpeed;

    public PlayerMoveStrategy(float moveSpeed) 
    {
        MoveSpeed = moveSpeed;
    }

	public abstract void Handle();
}

public class PlayerMoveLinear : PlayerMoveStrategy 
{
    private Vector3 _direction;
    private Transform _playerObj;

    public PlayerMoveLinear(Transform playerObj, float moveSpeed, Vector3 direction) : base(moveSpeed)
    {
        _playerObj = playerObj;
        _direction = direction;
    }

    public override void Handle() 
    {
        _playerObj.position += _direction * MoveSpeed * Time.deltaTime;
    }
}
