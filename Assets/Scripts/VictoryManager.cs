using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{

    public GameObject victoryPanel;

    float time;


    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        victoryPanel.GetComponent<Animator>().Play("VictoryPanel");
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 10)
        {
            SceneManager.LoadScene("Scene1");
        }

    }
}
