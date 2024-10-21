using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : MenuState
{

    public MainMenuState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(true);
        menu.getOptionsScreen().SetActive(false);
        menu.getCreditsScreen().SetActive(false);
        menu.getPauseScreen().SetActive(false);
        menu.getPopUp().SetActive(false);
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
                   menu.setState(new PopUpState(this.menu));
                    break;
                case 1:
                    menu.setState(new OptionsMenuState(this.menu));
                    break;
                case 2:
                    menu.setState(new CreditsMenuState(this.menu));
                    break;
                case 3:
                    Application.Quit();
                    break;
            }
        }
    }


}