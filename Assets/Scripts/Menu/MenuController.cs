using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : Singleton<MenuController>, IMenu
{

    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject popUpScreen;
    [SerializeField] private GameObject fadeScreen;

    private float alphaFade;
    private bool increaseAlpha;
    private bool topAlpha;

    private bool _goToState;
    private int _newState;
    private IState _state;

    // Start is called before the first frame update
    void Start()
    {
        alphaFade = 0.0f;
        increaseAlpha = false;
        topAlpha = false;

        _goToState = false;
        _newState = 0;
        setState(new MainMenuState(this));

    }


    public void goToStateButton(int number) {

        //SceneManager.LoadScene(0);
        //0 PopUp 1 Options 2 Credits 3 Exit
        //if (getState=)
        if ((SceneManager.GetActiveScene().name!= "MainMenu") && (number == 0))
        {
            setGoToState(true);
            setNewState(number);
        }
        else
        {
            alphaFade = 0.0f;
            increaseAlpha = true;
            setNewState(number);
        }

    }

 
    public void setGoToState(bool setGo)
    {
        _goToState = setGo;
    }

    public bool getGoToState()
    {
        return _goToState;
    }

    public void setNewState(int number)
    {
        _newState = number;
    }
    public int getNewState()
    {
        return _newState;
    }

    public IState getState()
    {
        return _state;
    }
    public void setState(IState state)
    {
        if (_state != null)
        {
            _state.Exit();
        }

        // Set current state and enter
        _state = state;
        _state.Enter();
    }

    private void Update()
    {
        if (increaseAlpha)
        {
            alphaFade += 2f*Time.deltaTime;
            alphaFade = Mathf.Clamp(alphaFade, 0.0f, 1.1f);
            if (alphaFade == 1.1f)
            {
                increaseAlpha = false;
                topAlpha = true;
            }
        }
        else
        {
            alphaFade -= 2f * Time.deltaTime;
            alphaFade = Mathf.Clamp(alphaFade, 0.0f, 1.1f);
        }

        fadeScreen.GetComponent<RawImage>().color = new Color(0f, 0f, 0f, alphaFade);

        if (topAlpha)
        {
            topAlpha = false;
            setGoToState(true);
        }
        _state.Update();
    }

    private void FixedUpdate()
    {
        _state.FixedUpdate();
    }

    public GameObject getMainScreen()
    {
        return mainScreen;
    }
    public GameObject getOptionsScreen()
    {
        return optionsScreen;
    }
    public GameObject getCreditsScreen()
    {
        return creditsScreen;
    }
    public GameObject getPauseScreen()
    {
        return pauseScreen;
    }

    public GameObject getPopUp()
    {
        return popUpScreen;
    }


}
