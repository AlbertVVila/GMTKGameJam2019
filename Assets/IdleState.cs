using UnityEngine;
using System.Collections;

public class IdleState : PlayerState
{
    public IdleState(Player player) : base(player)
    {
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