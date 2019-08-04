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
        direction = (player.surfaceContactPosition - (Vector2)player.transform.position).normalized;
        player.GetComponent<Rigidbody2D>().gravityScale = 0f;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.isGrabbed = true;
    }


    public override PlayerState Update()
    {
        if(Input.GetMouseButton(0))
        {
            if(!ReachingSurface())
            {
                player.spriteRenderer.sprite = player.spriteBase;
                return player.idleState;
            }
        }
        else
        {
            player.isGrabbed = false;
            return player.idleState;
        }
        return this;
    }

    private bool ReachingSurface()
    {
        if (Vector2.Distance(player.transform.position, player.surfaceContactPosition) > 0.1f) 
        {
            //Update player transform
            player.transform.position = Vector2.MoveTowards(player.transform.position, player.surfaceContactPosition, player.tongueSpeed * Time.deltaTime);

            //Update tongue transform
            Vector2 newPosition = Vector2.MoveTowards(player.tongue.transform.position, (Vector2)player.initTonguePosition + player.surfaceContactPosition, player.tongueSpeed * Time.deltaTime);

            float distance = Vector2.Distance(newPosition, player.tongue.transform.position);
            player.tongue.transform.position = newPosition;
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, -distance);
            return true;
        }
        return false;
    }

    public override void OnExit()
    {
        player.tongue.transform.localPosition = player.initTonguePosition;
        player.tongue.GetComponent<SpriteRenderer>().size = player.defaultTongueSize;
        player.hasContactedSurface = false;

        player.tongue.SetActive(false);
    }

}