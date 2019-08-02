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
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 0f);
        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Surface"))
            {
                canJump = true;
                player.surfaceMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    public override PlayerState Update()
    {
        if(canJump && Input.GetMouseButton(0))
        {
            currentTimer += Time.deltaTime;
            if(currentTimer >= jumpTimer)
            {
                return player.jumpToSurface;
            }
            return this;
        }
        return player.backingTongue;
    }

    public override void OnExit()
    {
        canJump = false;
        currentTimer = 0.0f;
    }
}
