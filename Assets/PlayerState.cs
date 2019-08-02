using UnityEngine;
using System.Collections;
using System;

public class PlayerState
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }

    virtual public PlayerState Update()
    {
        throw new NotImplementedException();
    }

    virtual public void OnEnter()
    {

    }

    virtual public void OnExit()
    {

    }
}
