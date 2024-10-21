using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneChanger : MonoBehaviour
{
    public Animator anim;
    string filePath;

    void Awake()
    {
        anim = GetComponent<Animator>();   
        filePath =  ($"{Application.dataPath}/GameScene");
    }

    public void GameOver()
    {
        anim.SetTrigger("ExitScene");
        SceneManager.LoadScene("GameOver");
    }

    public void ChangeScene()
    {
        if(Time.timeScale != 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ChangeSceneInt(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void StartAgain()
    {
        if (File.Exists(filePath)) { File.Delete(filePath); }
        Time.timeScale = 1;
        MenuController.Instance.setState(new PlayMenuState(MenuController.Instance));
        SceneManager.LoadScene("GameScene");
        
    }

    

    public void EndGame()
    {
        anim.SetTrigger("ExitScene");
        SceneManager.LoadScene("WinScreen");
    }
}
