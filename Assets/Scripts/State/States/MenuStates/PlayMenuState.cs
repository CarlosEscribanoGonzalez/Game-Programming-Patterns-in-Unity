using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuState : MenuState
{

    public PlayMenuState(IMenu menu) : base(menu)
    {
    }

    public override void Enter()
    {
        menu.getMainScreen().SetActive(false);
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

        if (Input.GetKeyDown("escape"))
        {
            menu.setState(new PauseMenuState(this.menu));
        }

    }
}
