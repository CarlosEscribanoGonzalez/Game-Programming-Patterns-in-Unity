using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuState : MenuState
{

    public PauseMenuState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(false);
        menu.getOptionsScreen().SetActive(false);
        menu.getCreditsScreen().SetActive(false);
        menu.getPauseScreen().SetActive(true);
        menu.getPopUp().SetActive(false) ;
    }



    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Update()
    {
        Time.timeScale = 0f;

        if (Input.GetKeyDown("escape"))
        {
            menu.setState(new PlayMenuState(this.menu));
            Time.timeScale = 1f;
        }

        if (menu.getGoToState())
        {
            Time.timeScale = 1f;
            menu.setGoToState(false);
            switch (menu.getNewState())
            {
                case 0:
                    menu.setState(new PlayMenuState(this.menu));
                    break;
                case 1:
                    menu.setState(new OptionsMenuState(this.menu));
                    break;
                case 2:
                    break;
                case 3:
                    menu.setState(new MainMenuState(this.menu));
                    SceneManager.LoadScene("MainMenu");
                    break;
            }
        }
    }
}