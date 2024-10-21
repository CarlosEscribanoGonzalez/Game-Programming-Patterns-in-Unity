using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMenu
{

    public GameObject getMainScreen();
    public GameObject getOptionsScreen();
    public GameObject getCreditsScreen();
    public GameObject getPauseScreen();

    public GameObject getPopUp();

    public void setGoToState(bool setGo);
    public bool getGoToState();

    public void setNewState(int number);
    public int getNewState();

    public void setState(IState state);
    public IState getState();
}