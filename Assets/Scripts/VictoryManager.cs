using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    //UI
    public GameObject victoryPanel;

    //Time in seconds
    float time;


    // Start is called before the first frame update
    void Start()
    {
        //Sets the time to 0
        time = 0;

        //Plays the victory UI animation
        victoryPanel.GetComponent<Animator>().Play("VictoryPanel");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        //Loads "Scene1" when 10 seconds have passed
        if (time > 10)
        {
            SceneManager.LoadScene("Scene1");
        }
    }
}
