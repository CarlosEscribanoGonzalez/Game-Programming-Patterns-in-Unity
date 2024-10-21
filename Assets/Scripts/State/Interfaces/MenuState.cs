using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuState : IState
{
    protected IMenu menu;

    public MenuState(IMenu menu)
    {
        this.menu = menu;
    }

    public abstract void Enter();

    public abstract void Exit();


    public abstract void FixedUpdate();

    public abstract void Update();
}
