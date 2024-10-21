using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialState : MenuState
{
    public bool goToGame = false;
    public TutorialState(IMenu menu) : base(menu)
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
        //menu.setState(new PlayMenuState(this.menu));
        //SceneManager.LoadScene("GameScene");
    }

    public override void FixedUpdate()
    {
        //throw new System.NotImplementedException();
    }

    public override void Update()
    {
        if (menu.getGoToState())
        {
            menu.setGoToState(false);
            menu.setState(new PlayMenuState(this.menu));
            SceneManager.LoadScene("GameScene");
        }

    }
  
}
