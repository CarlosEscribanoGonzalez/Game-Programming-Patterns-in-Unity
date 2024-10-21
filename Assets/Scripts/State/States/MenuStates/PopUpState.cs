using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpState : MenuState
{
    public PopUpState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(true);
        menu.getOptionsScreen().SetActive(false);
        menu.getCreditsScreen().SetActive(false);
        menu.getPauseScreen().SetActive(false);
        menu.getPopUp().SetActive(true);
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
            Time.timeScale = 1f;
            menu.setGoToState(false);
            switch (menu.getNewState())
            {
                case 0:
                    menu.setState(new TutorialState(this.menu));
                    SceneManager.LoadScene("GameTutorial");
                    break;
                case 1:
                    menu.setState(new PlayMenuState(this.menu));
                    SceneManager.LoadScene("GameScene");
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
