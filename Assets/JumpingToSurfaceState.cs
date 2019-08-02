using System;
using UnityEngine;
public class JumpingToSurfaceState : PlayerState
{

    Vector2 direction;
    public JumpingToSurfaceState(Player player) : base(player)
    {
    }

    public override void OnEnter()
    {
        direction = (player.surfaceMousePosition - (Vector2)player.transform.position).normalized;
    }


    public override PlayerState Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(!ReachingSurface())
            {
                return player.idleState;
            }
        }
        else
        {
            return player.idleState;
        }
        return this;
    }

    private bool ReachingSurface()
    {
        if (Vector2.Distance(player.transform.position, player.surfaceMousePosition) > 0.01f)
        {
            player.transform.position = Vector2.MoveTowards(player.transform.position, player.surfaceMousePosition, player.tongueSpeed * Time.deltaTime);
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, -player.tongueSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }

    public override void OnExit()
    {
        player.tongue.transform.localPosition = player.initTonguePosition;
        player.tongue.GetComponent<SpriteRenderer>().size = player.defaultTongueSize;
        player.tongue.SetActive(false);
    }

}