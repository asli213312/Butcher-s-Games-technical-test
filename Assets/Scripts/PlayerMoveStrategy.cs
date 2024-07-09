using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Transform _followObject;

    public PlayerMoveLinear(Transform followObject, float moveSpeed) : base(moveSpeed)
    {
        _followObject = followObject;
    }

    public override void Handle() 
    {
        _followObject.position += new Vector3(0, 0, MoveSpeed * Time.deltaTime);
    }
}
