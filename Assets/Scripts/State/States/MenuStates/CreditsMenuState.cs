using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenuState : MenuState
{

    public CreditsMenuState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(false);
        menu.getOptionsScreen().SetActive(false);
        menu.getCreditsScreen().SetActive(true);
        menu.getPauseScreen().SetActive(false);
    }



    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        if (menu.getGoToState())
        {
            menu.setGoToState(false);
            switch (menu.getNewState())
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    menu.setState(new MainMenuState(this.menu));
                    break;
            }
        }
    }
}