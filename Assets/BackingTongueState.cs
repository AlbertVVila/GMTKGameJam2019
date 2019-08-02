using UnityEngine;

public class BackingTongueState : PlayerState
{
    public BackingTongueState(Player player) : base(player)
    {
    }

    public override PlayerState Update()
    {
        if (!BackTongue())
        {
            return player.idleState;
        }
        return player.backingTongue;
    }

    private bool BackTongue()
    {
        if (Vector2.Distance(player.tongue.transform.position, player.initTonguePosition) > 0.01f)
        {
            player.tongue.transform.position = Vector2.MoveTowards(player.tongue.transform.position, player.initTonguePosition, player.tongueSpeed * Time.deltaTime);
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, -player.tongueSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }

    public override void OnExit()
    {
        player.tongue.SetActive(false);
        player.tongue.GetComponent<SpriteRenderer>().size = player.defaultTongueSize;
    }
}