using UnityEngine;
using System.Collections;

public class IdleState : PlayerState
{
    public IdleState(Player player) : base(player)
    {
    }

    public override void OnEnter()
    {
        if(!player.isGrabbed)
        {
            player.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }
    public override PlayerState Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            return player.throwingTongue;
        }
        return this;
    }
}