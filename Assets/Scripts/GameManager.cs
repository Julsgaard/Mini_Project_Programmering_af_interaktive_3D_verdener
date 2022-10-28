using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    public static bool GameIsActive;
    public static bool PlayerDead;

    public GameObject CanvasUI;
    public GameObject RenderTextureUI;
    public GameObject MenuPanel;

    public static int dolls = 0;



    void Awake()
    {
        //Keeps the GameManager when changing scene and destroys the new GameManager when restarting level
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(CanvasUI);
            Destroy(RenderTextureUI);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(CanvasUI);
            DontDestroyOnLoad(RenderTextureUI);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //RenderTextureUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        GameIsActive = false;
        PlayerDead = false;
        Time.timeScale = 1f;

        //MenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene1");
        GameIsActive = true;
        PlayerDead = false;
        Time.timeScale = 1f;

        MenuPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }



}
