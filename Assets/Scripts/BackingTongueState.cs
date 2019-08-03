using UnityEngine;

public class BackingTongueState : PlayerState
{

    Vector2 initialPosition;
    public BackingTongueState(Player player) : base(player)
    {
    }

    public override void OnEnter()
    {
        initialPosition = player.initTonguePosition + player.transform.position; //local initial tongue position + current player position
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
        if (Vector2.Distance(player.tongue.transform.position, initialPosition) > 0.01f)
        {
            
            Vector2 newPosition = Vector2.MoveTowards(player.tongue.transform.position, initialPosition, player.tongueSpeed * Time.deltaTime);

            float distance = Vector2.Distance(newPosition, player.tongue.transform.position);
            player.tongue.transform.position = newPosition;
            player.tongue.GetComponent<SpriteRenderer>().size += new Vector2(0f, -distance);
            return true;
        }
        return false;
    }

    public override void OnExit()
    {
        player.tongue.SetActive(false);
        player.tongue.GetComponent<SpriteRenderer>().size = player.defaultTongueSize;
        player.tongue.transform.localPosition = player.initTonguePosition;
    }
}