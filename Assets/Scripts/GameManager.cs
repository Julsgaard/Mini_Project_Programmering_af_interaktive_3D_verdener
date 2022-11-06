using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //SoundManager script
    public SoundManager soundManager;

    //UI
    public GameObject CanvasUI;
    public GameObject RenderTextureUI;
    public GameObject menuPanel;
    public GameObject deathPanel;
    public GameObject dollsCollectedUI;
    public GameObject victoryPanel;

    //Player
    public GameObject Player;

    //Hospital gameobjects
    public GameObject doorHospital;
    public GameObject lightHospital;
    public GameObject firstDoll;
    bool stopFunction = false;

    //Exit gate
    public GameObject exitGate;

    //Dolls collected by the player
    public static int dolls = 0;

 
    void Awake()
    {
        //Pauses the game
        Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Load Menu when the game is started and collected dolls is set to 0
        LoadMenu();

        dolls = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the first doll has been collected
        OpenFirstDoor();

        //Checks if the player has collected 4 dolls
        WinGame();
    }


    //Checks if the first doll has been collected and makes sure that the function only runs one time
    void OpenFirstDoor ()
    {
        if (firstDoll == null && !stopFunction)
        {
            //Light flicker animation
            lightHospital.GetComponent<Animator>().Play("LightFlicker");
            //Opens the first foor
            doorHospital.GetComponent<Animator>().Play("OpenDoorAnimation");

            //Plays creaking door sound effect
            AudioSource doorHospitalAudioSource = doorHospital.GetComponent<AudioSource>();
            doorHospitalAudioSource.Play();

            //Stops the function from running
            stopFunction = true;
        }
    }

    //LoadMenu when game is started or the level is done
    public void LoadMenu()
    {
        //Displays the correct UI
        RenderTextureUI.SetActive(true);
        CanvasUI.SetActive(true);
        deathPanel.SetActive(false);
        dollsCollectedUI.SetActive(false);
        victoryPanel.SetActive(false);

        //Pauses the game
        Time.timeScale = 0f;

        //Unlocks the cursor and makes in visble
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Starts the game when the button "Play" is pressed
    public void StartGame()
    {
        //Sets the game time to 1 (normal speed)
        Time.timeScale = 1f;

        //Plays the sound ambient music for the Hospital
        soundManager.PlayFirstClip();

        //Deactiavtes the menu
        menuPanel.SetActive(false);

        //Activates the Player
        Player.SetActive(true);

        //Locks the cursor and makes in invisble
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //If 4 dolls are collected then the exit gate is opened 
    void WinGame()
    {
        if (dolls >= 4)
        {
            //Opens the exit gate
            exitGate.GetComponent<Animator>().Play("OpenExitGate");
        }
    }

    //Quits the game if the button "Quit" is pressed
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
