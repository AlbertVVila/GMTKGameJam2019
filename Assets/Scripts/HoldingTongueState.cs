using UnityEngine;
using System.Collections;

public class HoldingTongueState : PlayerState
{

    bool canJump = false;

    float jumpTimer = 0.5f;
    float currentTimer = 0.0f;

    public HoldingTongueState(Player player) : base(player)
    {
    }

    public override void OnEnter()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override PlayerState Update()
    {
        if(Input.GetMouseButton(0))
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= jumpTimer)
            {
                return player.jumpToSurface;
            }
            return this;
        }
        player.hasContactedSurface = false;
        return player.backingTongue;
    }

    public override void OnExit()
    {
        currentTimer = 0.0f;
    }
}
