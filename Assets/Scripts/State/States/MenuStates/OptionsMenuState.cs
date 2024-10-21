using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuState : MenuState
{

    public OptionsMenuState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(false);
        menu.getOptionsScreen().SetActive(true);
        menu.getCreditsScreen().SetActive(false);
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
                    if (SceneManager.GetActiveScene().name== "MainMenu")
                    {
                        menu.setState(new MainMenuState(this.menu));
                    }
                    else
                    {
                        menu.setState(new PauseMenuState(this.menu));
                    }
                    break;
            }
        }
    }
}