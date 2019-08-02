using System;
using UnityEngine;

public class ThrowingTongueState : PlayerState
{

    Vector2 mousePosition; 
    public ThrowingTongueState(Player player) : base(player)
    {
    }

    public override void OnEnter()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player.tongue.SetActive(true);
        Vector2 direction = (mousePosition - (Vector2)player.tongue.transform.position).normalized;
        player.tongue.transform.up = direction;
    }

    public override PlayerState Update()
    {
        if(!MoveTongue())
        {
            if(Input.GetMouseButton(0))
            {
                return player.holdingTongue;
            }
            else
            {
                return player.backingTongue;
            }
        }
        return player.throwingTongue;
    }

    private bool MoveTongue()
    {
        if (Vector2.Distance(player.tongue.transform.position, mousePosition) > 0.01f)
        {
            player.tongue.transform.position = Vector2.MoveTowards(player.tongue.transform.position, mousePosition, player.tongueSpeed * Time.deltaTime);
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, player.tongueSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }
}