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
        if(Vector2.Distance(player.tongue.transform.position, mousePosition) > player.tongueRange)
        {
            mousePosition = (Vector2)player.tongue.transform.position +
            (mousePosition - (Vector2)player.tongue.transform.position).normalized * player.tongueRange;
        }
        player.tongue.SetActive(true);
    }

    public override PlayerState Update()
    {
        if(!MoveTongue())
        {
            if(Input.GetMouseButton(0) && player.hasContactedSurface)
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
            Vector2 newPosition = Vector2.MoveTowards(player.tongue.transform.position, mousePosition, player.tongueSpeed * Time.deltaTime);

            float distance = Vector2.Distance(newPosition, player.tongue.transform.position);
            player.tongue.transform.position = newPosition;
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, distance);

            Vector2 direction = (mousePosition - ((Vector2)player.transform.position + (Vector2)player.initTonguePosition)).normalized;
            player.tongue.transform.up = direction;
            player.spriteGO.transform.up = direction;

            if (player.hasContactedSurface) //Tongue has found a surface before reaching its destiny
            {
                return false;
            }

            return true;
        }
        return false;
    }
}