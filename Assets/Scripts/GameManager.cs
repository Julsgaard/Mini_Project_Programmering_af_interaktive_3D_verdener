using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;


    //static GameManager instance;

    //public static bool GameIsActive;
    //public static bool PlayerDead;

    //UI
    public GameObject CanvasUI;
    public GameObject RenderTextureUI;
    public GameObject menuPanel;
    public GameObject deathPanel;
    public GameObject dollsCollectedUI;
    public GameObject victoryPanel;

    public GameObject Player;

    //Hospital gameobjects
    public GameObject doorHospital;
    public GameObject lightHospital;
    public GameObject firstDoll;
    bool stopFunction = false;

    //Exit gate
    //public GameObject exitBoxCollider;
    public GameObject exitGate;


    public static int dolls = 0;

 


    void Awake()
    {
        //Keeps the GameManager when changing scene and destroys the new GameManager when restarting level
        /*if (instance != null)
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
        }*/

        Time.timeScale = 0f;

    }



    // Start is called before the first frame update
    void Start()
    {

        LoadMenu();

        dolls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OpenFirstDoor();

        WinGame();

    }


    void OpenFirstDoor ()
    {
        if (firstDoll == null && !stopFunction)
        {
            lightHospital.GetComponent<Animator>().Play("LightFlicker");
            doorHospital.GetComponent<Animator>().Play("OpenDoorAnimation");
            AudioSource doorHospitalAudioSource = doorHospital.GetComponent<AudioSource>();
            doorHospitalAudioSource.Play();

            stopFunction = true;
        }
    }

    public void LoadMenu()
    {
        //SceneManager.LoadScene("Menu");
        //GameIsActive = false;
        //PlayerDead = false;

        RenderTextureUI.SetActive(true);
        CanvasUI.SetActive(true);
        deathPanel.SetActive(false);
        dollsCollectedUI.SetActive(false);
        victoryPanel.SetActive(false);

        Time.timeScale = 0f;

        //MenuPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {

        //SceneManager.LoadScene("Scene1");
        //GameIsActive = true;
        //PlayerDead = false;
        Time.timeScale = 1f;

        Player.transform.position = new Vector3(-2.6f, 2.4f, -146.4f);

        //soundManager.PlayAmbientMusic();
        soundManager.PlayFirstClip();

        menuPanel.SetActive(false);
        //CanvasUI.SetActive(false);
        Player.SetActive(true);


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void WinGame()
    {
        if (dolls >= 4)
        {
            //Open exit gate
            exitGate.GetComponent<Animator>().Play("OpenExitGate");

            //Play final clip
            //soundManager.PlayFinalClip();
        }
    }


    //Quits the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }



}
