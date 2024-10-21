using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class TutorialExit : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float visibleDistance = 3f;
    private IState _state;
    public Animator anim;
    string filePath;
    private bool activate;
    // Start is called before the first frame update
    void Start()
    {
        filePath = ($"{Application.dataPath}/GameScene");
        activate = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!(Mathf.Abs(this.transform.position.x - player.transform.position.x) > visibleDistance)&&(activate==false))
        {
            activate = true;
            if (File.Exists(filePath)) { File.Delete(filePath); }
            Time.timeScale = 1;
            MenuController.Instance.GetComponent<MenuController>().goToStateButton(1);
        }
    }
}